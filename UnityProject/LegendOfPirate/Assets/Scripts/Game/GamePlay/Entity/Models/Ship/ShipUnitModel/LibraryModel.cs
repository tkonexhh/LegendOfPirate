using Qarth;
using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;


namespace GameWish.Game
{
    public class LibraryModel : ShipUnitModel
    {
        private List<LibrarySlotModel> m_SlotModelList = new List<LibrarySlotModel>();
        private List<LibraryPreparatorRoleModel> m_RoleModelList = new List<LibraryPreparatorRoleModel>();

        //ReactiveCommand
        private LibraryData m_DbData;
        private LibraryUnitConfig m_TableConfig;

        public IntReactiveProperty selectedNumer;
        public IReadOnlyReactiveProperty<bool> IsShowReadBtn;

        public LibraryData DBData { get { return m_DbData; } }
        public LibraryUnitConfig TableConfig { get { return m_TableConfig; } }
        public List<LibrarySlotModel> LibrarySlotModels { get { return m_SlotModelList; } }
        public List<LibraryPreparatorRoleModel> RoleModelList { get { return m_RoleModelList; } }

        public LibraryModel(ShipUnitData shipUnitData) : base(shipUnitData)
        {
            m_DbData = GameDataMgr.S.GetData<LibraryData>();
            m_TableConfig = TDFacilityLibraryTable.GetConfig(level.Value);

            InitLibraryData();

            RefreshCurRoleModels();

            level.Subscribe(val => HandleShipUnitUpgrade());

            PrepareResponseProperties();
        }

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
        public LibraryPreparatorRoleModel GetLibraryRoleByRoleID(int roleID)
        {
            LibraryPreparatorRoleModel libraryPreparatorRoleModel = null;
            foreach (var item in m_RoleModelList)
            {
                if (item.GetRoleID() == roleID)
                {
                    libraryPreparatorRoleModel = item;
                }
            }
            return libraryPreparatorRoleModel;
        }

        /// <summary>
        /// 根据slotID 拿到 roleModel
        /// </summary>
        /// <param name="slotID"></param>
        /// <returns></returns>
        public LibraryPreparatorRoleModel GetLibraryRoleBySlotID(int slotID)
        {
            LibraryPreparatorRoleModel libraryPreparatorRoleModel = null;
            foreach (var item in m_RoleModelList)
            {
                if (item.IsBindSlot.Value && item.LibrarySlotModel.DBItem.slotId == slotID)
                {
                    libraryPreparatorRoleModel = item;
                }
            }
            return libraryPreparatorRoleModel;
        }

        /// <summary>
        /// 根据角色ID获取训练得坑位
        /// </summary>
        /// <param name="roleID"></param>
        public LibrarySlotModel GetSlotModelByRoleID(int roleID)
        {
            LibrarySlotModel librarySlotModel = null;
            foreach (var item in m_SlotModelList)
            {
                if (item.heroID.Value == roleID)
                {
                    librarySlotModel = item;
                }
            }
            return librarySlotModel;
        }

        /// <summary>
        /// 获取一个空闲得坑位
        /// </summary>
        /// <returns></returns>
        public LibrarySlotModel GetFreeSlot()
        {
            LibrarySlotModel trainingSlotModel = null;
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
        public LibrarySlotModel GetFreeAndHeroSelectedSlot()
        {
            LibrarySlotModel trainingSlotModel = null;
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
                m_RoleModelList.Add(new LibraryPreparatorRoleModel(item, this));
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
            int freeNumber = GetNumberBySlotState(LibrarySlotState.Free);
            if (roleModels.Count == 0)
            {
                FloatMessageTMP.S.ShowMsg(LanguageKeyDefine.LIBRARYROOM_CONT_Ⅲ);
                return;
            }
            if (freeNumber == 0)
            {
                FloatMessageTMP.S.ShowMsg(LanguageKeyDefine.LIBRARYROOM_CONT_Ⅰ);
                return;
            }

            int surplus = roleModels.Count > freeNumber ? freeNumber : roleModels.Count;
            for (int i = 0; i < surplus; i++)
            {
                LibrarySlotModel librarySlotModel = GetFreeAndHeroSelectedSlot();
                LibraryPreparatorRoleModel libraryPreparatorRoleModel = GetLibraryRoleByRoleID(roleModels[i].id);

                if (librarySlotModel != null && libraryPreparatorRoleModel != null)
                {
                    libraryPreparatorRoleModel.AutoSelectRole(librarySlotModel);
                    librarySlotModel.AutoSelectAndStart(roleModels[i].id);
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
            Log.e("selectedNumer" + selectedNumer.Value);
        }

        /// <summary>
        /// 减少选择的数量 
        /// </summary>
        public void ReduceSelectedNumer()
        {
            selectedNumer.Value--;
            Log.e("selectedNumer" + selectedNumer.Value);
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
            List<LibraryPreparatorRoleModel> libraryRoleModels = new List<LibraryPreparatorRoleModel>();
            foreach (var item in m_RoleModelList)
            {
                if (item.IsBindSlot.Value && item.LibrarySlotModel.IsReading())
                {
                    libraryRoleModels.Add(item);
                }
            }
            foreach (var item in libraryRoleModels)
                m_RoleModelList.Remove(item);
            m_RoleModelList.AddRange(libraryRoleModels);
            libraryRoleModels.Clear();
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
            m_TableConfig = TDFacilityLibraryTable.GetConfig(level.Value);

            int surplus = m_TableConfig.capacity - number;
            int unlockNumber = 0;

            foreach (var item in m_SlotModelList)
            {
                if (unlockNumber == surplus)
                    return;

                if (item.libraryState.Value == LibrarySlotState.Locked)
                {
                    item.UnlockLibrarySolt();
                    unlockNumber++;
                }
            }
        }

        /// <summary>
        /// 获取各个状态的数量
        /// </summary>
        /// <param name="librarySlotState"></param>
        /// <returns></returns>
        public int GetNumberBySlotState(LibrarySlotState librarySlotState)
        {
            int number = 0;
            foreach (var item in m_SlotModelList)
            {
                if (item.libraryState.Value == librarySlotState)
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
                if (item.libraryState.Value != LibrarySlotState.Locked)
                {
                    number++;
                }
            }
            return number;
        }

        /// <summary>
        /// 初始化数据
        /// </summary>
        private void InitLibraryData()
        {
            if (m_DbData.LibraryItemList.Count == 0)
            {
                foreach (var item in TDFacilityLibraryTable.libraryUnitProperties)
                {
                    LibraryDBData slot = new LibraryDBData(item.baseProperty.level);
                    m_DbData.AddTrainingSlotData(slot);
                    LibrarySlotModel slotModel = new LibrarySlotModel(this, slot);
                    m_SlotModelList.Add(slotModel);
                }
            }
            else
            {
                if (m_DbData.LibraryItemList.Count == TDFacilityLibraryTable.libraryUnitProperties.Length)
                {
                    for (int i = 0; i < m_DbData.LibraryItemList.Count; i++)
                    {
                        LibrarySlotModel slotModel = new LibrarySlotModel(this, m_DbData.LibraryItemList[i]);
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
    }
}