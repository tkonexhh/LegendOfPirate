using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;
using Qarth;
using static GameWish.Game.TrainingData;

namespace GameWish.Game
{
    public class TrainingRoomModel : ShipUnitModel
    {
        private List<TrainingSlotModel> m_SlotModelList = new List<TrainingSlotModel>();
        private List<TrainingPreparatorRoleModel> m_RoleModelList = new List<TrainingPreparatorRoleModel>();

        private TrainingData m_DbData;
        private TrainingRoomUnitConfig m_TableConfig;

        public IntReactiveProperty selectedNumer;
        public IReadOnlyReactiveProperty<bool> IsShowReadBtn;

        public TrainingData DBData { get { return m_DbData; } }
        public TrainingRoomUnitConfig TableConfig { get { return m_TableConfig; } }
        public List<TrainingSlotModel> TrainingSlotModels { get { return m_SlotModelList; } }
        public List<TrainingPreparatorRoleModel> RoleModelList { get { return m_RoleModelList; } }

        #region Model
        public override void OnDestroyed()
        {
            base.OnDestroyed();
            foreach (var item in m_SlotModelList)
            {
                item.OnDestroyed();
            }
        }

        public override void OnUpdate()
        {
            foreach (var item in m_SlotModelList)
            {
                item.OnUpdate();
            }
        }

        public TrainingRoomModel(ShipUnitData shipUnitData) : base(shipUnitData)
        {
            m_DbData = GameDataMgr.S.GetData<TrainingData>();
            m_TableConfig = TDFacilityTrainingRoomTable.GetConfig(level.Value);

            InitTrainingData();

            RefreshCurRoleModels();

            level.Subscribe(val => HandleShipUnitUpgrade());

            PrepareResponseProperties();
        }
        #endregion

        #region Public
        /// <summary>
        /// 清除缓存数据
        /// </summary>
        public void ClearCacheData()
        {
            ResetSelectedNumber();
            foreach (var item in RoleModelList)
            {
                item.ClearRoleSelect();
            }
        }

        /// <summary>
        /// 根据角色ID 拿到RoleModel
        /// </summary>
        /// <param name="roleID"></param>
        /// <returns></returns>
        public TrainingPreparatorRoleModel GetTrainingRoleByRoleID(int roleID)
        {
            TrainingPreparatorRoleModel TrainingPreparatorRoleModel = null;
            foreach (var item in m_RoleModelList)
            {
                if (item.GetRoleID() == roleID)
                {
                    TrainingPreparatorRoleModel = item;
                }
            }
            return TrainingPreparatorRoleModel;
        }

        /// <summary>
        /// 根据slotID 拿到 roleModel
        /// </summary>
        /// <param name="slotID"></param>
        /// <returns></returns>
        public TrainingPreparatorRoleModel GetTrainingRoleBySlotID(int slotID)
        {
            TrainingPreparatorRoleModel TrainingPreparatorRoleModel = null;
            foreach (var item in m_RoleModelList)
            {
                if (item.IsBindSlot.Value && item.TrainingSlotModel.DBItem.slotId == slotID)
                {
                    TrainingPreparatorRoleModel = item;
                }
            }
            return TrainingPreparatorRoleModel;
        }

        /// <summary>
        /// 根据角色ID获取训练得坑位
        /// </summary>
        /// <param name="roleID"></param>
        public TrainingSlotModel GetSlotModelByRoleID(int roleID)
        {
            TrainingSlotModel TrainingSlotModel = null;
            foreach (var item in m_SlotModelList)
            {
                if (item.heroID.Value == roleID)
                {
                    TrainingSlotModel = item;
                }
            }
            return TrainingSlotModel;
        }

        /// <summary>
        /// 获取一个空闲得坑位
        /// </summary>
        /// <returns></returns>
        public TrainingSlotModel GetFreeSlot()
        {
            TrainingSlotModel trainingSlotModel = null;
            foreach (var item in m_SlotModelList)
            {
                if (item.IsFree())
                {
                    trainingSlotModel = item;
                    break;
                }
            }
            return trainingSlotModel;
        }

        /// <summary>
        /// 获得空闲和选中的空位
        /// </summary>
        /// <returns></returns>
        public TrainingSlotModel GetFreeAndHeroSelectedSlot()
        {
            TrainingSlotModel trainingSlotModel = null;
            foreach (var item in m_SlotModelList)
            {
                if (item.IsFree() || item.IsHeroSelected())
                {
                    trainingSlotModel = item;
                    break;
                }
            }
            return trainingSlotModel;
        }

        /// <summary>
        /// 刷新当前的角色列表
        /// </summary>
        public void RefreshCurRoleModels()
        {
            if (m_RoleModelList.Count > 0)
                m_RoleModelList.Clear();

            foreach (var item in ModelMgr.S.GetModel<RoleGroupModel>().RoleUnlockedItemList)
            {
                m_RoleModelList.Add(new TrainingPreparatorRoleModel(item, this));
            }
            SortRefreshList();
        }

        /// <summary>
        /// 自动选择
        /// </summary>
        public void AutoSelectRole()
        {
            foreach (var item in m_RoleModelList)
            {
                if (item.IsBindSlot.Value)
                {
                    item.ClearRoleSelect();
                }
            }

            List<RoleModel> roleModels = ModelMgr.S.GetModel<RoleGroupModel>().GetRolesByManagementState();
            int freeNumber = GetNumberBySlotState(TrainingSlotState.Free);
            if (roleModels.Count == 0)
            {
                FloatMessageTMP.S.ShowMsg(LanguageKeyDefine.TRAININGROOM_CONT_Ⅲ);
                return;
            }
            if (freeNumber == 0)
            {
                FloatMessageTMP.S.ShowMsg(LanguageKeyDefine.TRAININGROOM_CONT_Ⅰ);
                return;
            }

            int surplus = roleModels.Count > freeNumber ? freeNumber : roleModels.Count;
            for (int i = 0; i < surplus; i++)
            {
                TrainingSlotModel TrainingSlotModel = GetFreeAndHeroSelectedSlot();
                TrainingPreparatorRoleModel TrainingPreparatorRoleModel = GetTrainingRoleByRoleID(roleModels[i].id);

                if (TrainingSlotModel != null && TrainingPreparatorRoleModel != null)
                {
                    TrainingPreparatorRoleModel.AutoSelectRole(TrainingSlotModel);
                    TrainingSlotModel.AutoSelectAndStart(roleModels[i].id);
                }
                else
                    Log.e("Error : Auto select");
            }

            SortRefreshList();
        }

        /// <summary>
        /// 开始训练
        /// </summary>
        public void StartLearn()
        {
            foreach (var item in m_SlotModelList)
            {
                item.StartRead();
            }

            SortRefreshList();
        }

        /// <summary>
        /// 增加选择的数量
        /// </summary>
        public void AddSelectedNumer()
        {
            selectedNumer.Value++;
        }

        /// <summary>
        /// 减少选择的数量 
        /// </summary>
        public void ReduceSelectedNumer()
        {
            selectedNumer.Value = Mathf.Max(Define.DEFAULT_DIAMOND_NUM, selectedNumer.Value - 1);
        }

        /// <summary>
        /// 重置选择数据
        /// </summary>
        public void ResetSelectedNumber()
        {
            selectedNumer.Value = 0;
        }

        /// <summary>
        /// 获得选择了角色的数量
        /// </summary>
        /// <returns></returns>
        public int GetSelectedNumber()
        {
            int number = 0;
            foreach (var item in RoleModelList)
            {
                if (item.IsHeroSelect())
                {
                    number++;
                }
            }
            return number;
        }

        /// <summary>
        /// 排序
        /// </summary>
        public void SortRefreshList()
        {
            List<TrainingPreparatorRoleModel> TrainingRoleModels = new List<TrainingPreparatorRoleModel>();
            foreach (var item in m_RoleModelList)
            {
                if (item.IsBindSlot.Value && item.TrainingSlotModel.IsReading())
                {
                    TrainingRoleModels.Add(item);
                }
            }
            foreach (var item in TrainingRoleModels)
                m_RoleModelList.Remove(item);
            m_RoleModelList.AddRange(TrainingRoleModels);
            TrainingRoleModels.Clear();
        }
        #endregion

        #region Private
        /// <summary>
        /// 准备响应式属性
        /// </summary>
        private void PrepareResponseProperties()
        {
            selectedNumer = new IntReactiveProperty(GetSelectedNumber());

            IsShowReadBtn = selectedNumer.Select(val => val > 0).ToReactiveProperty();
        }

        /// <summary>
        /// 处理升级事件
        /// </summary>
        private void HandleShipUnitUpgrade()
        {
            int number = GetUnlockSlotNumber();
            m_TableConfig = TDFacilityTrainingRoomTable.GetConfig(level.Value);

            int surplus = m_TableConfig.capacity - number;
            int unlockNumber = 0;

            foreach (var item in m_SlotModelList)
            {
                if (unlockNumber == surplus)
                    return;

                if (item.trainingState.Value == TrainingSlotState.Locked)
                {
                    item.UnlockTrainingSolt();
                    unlockNumber++;
                }
            }
        }

        /// <summary>
        /// 获取各个状态的数量
        /// </summary>
        /// <param name="TrainingSlotState"></param>
        /// <returns></returns>
        public int GetNumberBySlotState(TrainingSlotState TrainingSlotState)
        {
            int number = 0;
            foreach (var item in m_SlotModelList)
            {
                if (item.trainingState.Value == TrainingSlotState)
                {
                    number++;
                }
            }
            return number;
        }

        /// <summary>
        /// 已解锁的坑位数量
        /// </summary>
        /// <returns></returns>
        private int GetUnlockSlotNumber()
        {
            int number = 0;
            foreach (var item in m_SlotModelList)
            {
                if (item.trainingState.Value != TrainingSlotState.Locked)
                {
                    number++;
                }
            }
            return number;
        }

        /// <summary>
        /// 初始化数据
        /// </summary>
        private void InitTrainingData()
        {
            if (m_DbData.TrainingItemList.Count == 0)
            {
                foreach (var item in TDFacilityTrainingRoomTable.trainingRoomUnitProperties)
                {
                    TrainingDBData slot = new TrainingDBData(item.baseProperty.level);
                    m_DbData.AddTrainingSlotData(slot);
                    TrainingSlotModel slotModel = new TrainingSlotModel(this, slot);
                    m_SlotModelList.Add(slotModel);
                }
            }
            else
            {
                if (m_DbData.TrainingItemList.Count == TDFacilityTrainingRoomTable.trainingRoomUnitProperties.Length)
                {
                    for (int i = 0; i < m_DbData.TrainingItemList.Count; i++)
                    {
                        TrainingSlotModel slotModel = new TrainingSlotModel(this, m_DbData.TrainingItemList[i]);
                        m_SlotModelList.Add(slotModel);
                    }
                }
                else
                {
                    Log.e("Error : Count Different ! ");
                }
            }
        }
        #endregion
    }
}