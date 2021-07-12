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
        private float m_RefreshTime = 0;
        private float m_RefreshInterval = 0.3f;

        public TrainingRoomUnitConfig tableConfig;
        public List<TrainingSlotModel> slotModelList = new List<TrainingSlotModel>();

        private TrainingData m_DbData;

        #region Model
        public override void OnUpdate()
        {
            m_RefreshTime += Time.deltaTime;
            if (m_RefreshTime >= m_RefreshInterval)
            {
                m_RefreshTime = 0;

                for (int i = 0; i < slotModelList.Count; i++)
                {
                    slotModelList[i].RefreshRemainTime();
                }
            }
        }
        #endregion

        #region public

        /// <summary>
        /// 训练一组人
        /// </summary>
        /// <param name="roles"></param>
        public void TrainingRoleGroup(List<RoleModel> roles)
        {
            List<TrainingSlotModel> freeSlots = GetAllFreeSlot();
            if (freeSlots.Count >= roles.Count)
            {
                foreach (var item in roles)
                {
                    TrainingSlotModel trainingSlotModel = GetFreeSlot();
                    if (trainingSlotModel != null)
                        trainingSlotModel.StartTraining(item.id);
                    else
                        FloatMessageTMP.S.ShowMsg(LanguageKeyDefine.TRAININGROOM_CONT_1);
                }
            }
            else
            {
                for (int i = 0; i < freeSlots.Count; i++)
                {
                    freeSlots[i].StartTraining(roles[i].id);
                }
            }
        }

        /// <summary>
        /// 开始训练
        /// </summary>
        public void StartTraining()
        {
            foreach (var item in slotModelList)
            {
                if (item.IsReadyTraining())
                {
                    item.StartTraining();
                }
            }
        }

        /// <summary>
        /// 根据角色ID获取训练得坑位
        /// </summary>
        /// <param name="roleID"></param>
        public TrainingSlotModel GetSlotModelByRoleID(int roleID)
        {
            TrainingSlotModel trainingSlotModel = null;
            foreach (var item in slotModelList)
            {
                if (item.heroId.Value == roleID)
                {
                    trainingSlotModel = item;
                }
            }
            return trainingSlotModel;
        }

        /// <summary>
        /// 获取一个空闲得坑位
        /// </summary>
        /// <returns></returns>
        public TrainingSlotModel GetFreeSlot()
        {
            TrainingSlotModel trainingSlotModel = null;
            foreach (var item in slotModelList)
            {
                if (item.trainState.Value == TrainingSlotState.Free && item.heroId.Value == -1)
                {
                    trainingSlotModel = item;
                    break;
                }
            }
            return trainingSlotModel;
        }

        public TrainingRoomModel(ShipUnitData shipUnitData) : base(shipUnitData)
        {
            m_DbData = GameDataMgr.S.GetData<TrainingData>();
            tableConfig = TDFacilityTrainingRoomTable.GetConfig(level.Value);

            InitTrainData();

            level.Subscribe(val =>
            {
                int number = GetUnlockSlotNumber();
                tableConfig = TDFacilityTrainingRoomTable.GetConfig(level.Value);

                int surplus = tableConfig.capacity - number;
                int unlockNumber = 0;

                foreach (var item in slotModelList)
                {
                    if (unlockNumber == surplus)
                        return;

                    if (item.trainState.Value == TrainingSlotState.Locked)
                    {
                        item.OnTrainingRoomLevelUp();
                        unlockNumber++;
                    }
                }
            });
        }
        #endregion

        #region Private

        private List<TrainingSlotModel> GetAllFreeSlot()
        {
            List<TrainingSlotModel> freeSlots = new List<TrainingSlotModel>();
            foreach (var item in slotModelList)
            {
                if (item.IsFree())
                {
                    freeSlots.Add(item);
                }
            }
            return freeSlots;
        }

        private void InitTrainData()
        {
            if (m_DbData.trainingItemList.Count == 0)
            {
                foreach (var item in TDFacilityTrainingRoomTable.trainingRoomUnitProperties)
                {
                    TrainingSlotData slot = new TrainingSlotData(item.baseProperty.level);
                    m_DbData.AddTrainingSlotData(slot);
                    TrainingSlotModel slotModel = new TrainingSlotModel(this, slot);
                    slotModelList.Add(slotModel);
                }
            }
            else
            {
                if (m_DbData.trainingItemList.Count == TDFacilityTrainingRoomTable.trainingRoomUnitProperties.Length)
                {
                    for (int i = 0; i < m_DbData.trainingItemList.Count; i++)
                    {
                        TrainingSlotModel slotModel = new TrainingSlotModel(this, m_DbData.trainingItemList[i]);
                        slotModelList.Add(slotModel);
                    }
                }
                else
                {
                    Log.e("Error : Count Different ! ");
                }
            }
        }

        private int GetUnlockSlotNumber()
        {
            int number = 0;
            foreach (var item in slotModelList)
            {
                if (item.trainState.Value != TrainingSlotState.Locked)
                {
                    number++;
                }
            }
            return number;
        }
        #endregion
    }

    public class TrainingSlotModel : Model
    {
        public int slotIDAndUnlockLevel;

        public IntReactiveProperty heroId = new IntReactiveProperty(-1);
        public FloatReactiveProperty trainRemainTime = new FloatReactiveProperty(0);

        public ReactiveProperty<TrainingSlotState> trainState;

        private DateTime m_StartTime = default(DateTime);
        private DateTime m_EndTime = default(DateTime);

        private TrainingRoomModel m_TrainingRoomMode;
        private TrainingData.TrainingSlotData m_DbItem;
        private RoleGroupModel m_RoleGroupModel;

        public TrainingSlotModel(TrainingRoomModel trainingRoomModel, TrainingData.TrainingSlotData dbItem)
        {
            m_TrainingRoomMode = trainingRoomModel;
            m_DbItem = dbItem;

            this.slotIDAndUnlockLevel = dbItem.slotId;
            this.heroId.Value = dbItem.heroId;
            this.trainState = new ReactiveProperty<TrainingSlotState>(dbItem.trainState);

            switch (trainState.Value)
            {
                case TrainingSlotState.Free:
                    break;
                case TrainingSlotState.Training:
                    SetTime(dbItem.trainingStartTime);
                    RefreshRemainTime();
                    break;
                case TrainingSlotState.Locked:
                    break;
            }
        }

        #region Public Public

        /// <summary>
        /// 是否空闲
        /// </summary>
        /// <returns></returns>
        public bool IsFree()
        {
            return trainState.Value == TrainingSlotState.Free;
        }

        /// <summary>
        /// 是否准好训练
        /// </summary>
        /// <returns></returns>
        public bool IsReadyTraining()
        {
            if (heroId.Value!=-1 && IsFree())
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 临时设置角色
        /// </summary>
        /// <param name="id"></param>
        public void SetTemporaryRoleID(int id)
        {
            if (IsFree())
            {
                heroId.Value = id;
            }
        }

        /// <summary>
        /// 清除临时角色
        /// </summary>
        public void ClearTemporaryRoleID()
        {
            if (IsFree())
            {
                heroId.Value = - 1;
            }
        }

        /// <summary>
        /// 开始训练
        /// </summary>
        /// <param name="startTime"></param>
        public void StartTraining()
        {
            if (heroId.Value != -1)
            {
                DateTime dateTime = DateTime.Now;

                SetTime(dateTime);

                trainState.Value = TrainingSlotState.Training;

                ModelMgr.S.GetModel<RoleGroupModel>().SetRoleManagementState(heroId.Value, ManagementRoleState.TrainingRoom);

                m_DbItem.OnStartTraining(heroId.Value, dateTime);
            }
            else
                Log.e("Error : id = -1");
        }

        public void StartTraining(int id)
        {
            heroId.Value = id;
            StartTraining();
        }

        /// <summary>
        /// 结束训练
        /// </summary>
        public void EndTraining()
        {
            ModelMgr.S.GetModel<RoleGroupModel>().SetRoleManagementState(heroId.Value, ManagementRoleState.None);

            heroId.Value = -1;
            trainRemainTime.Value = 0f;
            trainState.Value = TrainingSlotState.Free;
            m_StartTime = default(DateTime);
            m_EndTime = default(DateTime);

            m_DbItem.OnEndTraining();
            //TODO...   增加人物经验
            //ModelMgr.S.GetModel<RoleGroupModel>().GetRoleModel(heroId).AddCurExp(m_TrainingRoomMode.tableConfig.experience);
        }

        /// <summary>
        /// 刷新训练时间
        /// </summary>
        public void RefreshRemainTime()
        {
            if (m_DbItem.trainState != TrainingSlotState.Training)
                return;

            double remainTime = (m_EndTime - DateTime.Now).TotalSeconds;

            trainRemainTime.Value = (float)remainTime;
            if (trainRemainTime.Value <= 0f)
            {
                EndTraining();
            }
        }

        /// <summary>
        /// 训练室升级
        /// </summary>
        public void OnTrainingRoomLevelUp()
        {
            trainState.Value = TrainingSlotState.Free;

            m_DbItem.OnUnlocked();
        }
        #endregion

        #region Private
        private void SetTime(DateTime startTime)
        {
            m_StartTime = startTime;
            int totalTime = m_TrainingRoomMode.tableConfig.trainingTime;
            m_EndTime = m_StartTime + TimeSpan.FromSeconds(totalTime);
        }
        #endregion
    }
}