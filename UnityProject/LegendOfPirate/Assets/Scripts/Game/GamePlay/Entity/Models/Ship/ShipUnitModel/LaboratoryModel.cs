using System.Collections;
using System.Collections.Generic;
using System;
using UniRx;
using UnityEngine;

namespace GameWish.Game
{
    public class LaboratoryModel : ShipUnitModel
    {
        public LaboratoryUnitConfig tableConfig;
        public ReactiveCollection<LaboratorySlotModel> labaratorySlotModelList = new ReactiveCollection<LaboratorySlotModel>();
        public List<PotionSlotModel> potionSlotModelList = new List<PotionSlotModel>();

        private LaboratoryData m_DbData;

        public LaboratoryModel(ShipUnitData shipUnitData) : base(shipUnitData)
        {
            tableConfig = TDFacilityLaboratoryTable.GetConfig(level.Value);
            foreach (var item in TDFacilityLaboratoryTable.dataList)
            {
                potionSlotModelList.Add(new PotionSlotModel(this, item.unlockPotionID, level.Value < item.level, item.level));
            }
            m_DbData = GameDataMgr.S.GetData<LaboratoryData>();
            if (tableConfig.unlockSpaceCount >= Define.LABORATORY_DEFAULT_SLOT_COUNT)
            {
                for (int i = 0; i < tableConfig.unlockSpaceCount; i++)
                {
                    labaratorySlotModelList.Add(new LaboratorySlotModel(this, m_DbData.laboratorySlotItemLst[i], i));
                }
            }
            else
            {
                for (int i = 0; i < Define.LABORATORY_DEFAULT_SLOT_COUNT; i++)
                {
                    labaratorySlotModelList.Add(new LaboratorySlotModel(this, m_DbData.laboratorySlotItemLst[i], i));
                }
            }
        }

        public LaboratorySlotModel GetSelectModel() 
        {

            foreach (var item in labaratorySlotModelList)
            {
                if (item.laboratorySlotState.Value == LaboratorySlotState.Selected)
                    return item;
            }
            return null;
        }

        public LaboratorySlotModel GetAvailableSlot() 
        {

            foreach (var item in labaratorySlotModelList)
            {
                if (item.laboratorySlotState.Value == LaboratorySlotState.Free)
                    return item;
            }
            return null;
        }

        public override void OnLevelUpgrade(int delta)
        {
            base.OnLevelUpgrade(delta);

            tableConfig = TDFacilityLaboratoryTable.GetConfig(level.Value);
            for (int i = 0; i < potionSlotModelList.Count; i++)
            {
                potionSlotModelList[i].OnLaboratoryLevelUp();
            }
            if (tableConfig.unlockSpaceCount > Define.LABORATORY_DEFAULT_SLOT_COUNT && tableConfig.unlockSpaceCount > labaratorySlotModelList.Count)
            {
                labaratorySlotModelList.Add(new LaboratorySlotModel(this, m_DbData.laboratorySlotItemLst[labaratorySlotModelList.Count], labaratorySlotModelList.Count));
            }
            for (int i = 0; i < labaratorySlotModelList.Count; i++)
            {
                labaratorySlotModelList[i].OnProcessingSlotLevelUp();
            }
        }

        private float m_RefreshTime = 0;
        private float m_RefreshInterval = 0.3f;

        public override void OnUpdate()
        {
            m_RefreshTime += Time.deltaTime;
            if (m_RefreshTime >= m_RefreshInterval)
            {
                m_RefreshTime = 0;
                foreach (var item in labaratorySlotModelList)
                {
                    item.RefreshRemainTime();
                }
            }
        }
    }

    public class LaboratorySlotModel
    {
        public int potionId;
        public int slotId;
        public FloatReactiveProperty makingRemainTime = new FloatReactiveProperty();

        public ReactiveProperty<LaboratorySlotState> laboratorySlotState;

        private DateTime m_StartTime = default(DateTime);
        private DateTime m_EndTime = default(DateTime);

        private LaboratoryModel m_LaboratoryModel;
        private LaboratorySlotDataItem m_DbItem;

        #region Public
        public LaboratorySlotModel(LaboratoryModel laboratoryModel, LaboratorySlotDataItem dbItem, int slotId) 
        {
            m_LaboratoryModel = laboratoryModel;
            m_DbItem = dbItem;
            this.slotId = slotId;

            if (slotId + 1 > laboratoryModel.tableConfig.unlockSpaceCount)
            {
                laboratorySlotState = new ReactiveProperty<LaboratorySlotState>(LaboratorySlotState.Locked);
            }
            else
            {
                laboratorySlotState = new ReactiveProperty<LaboratorySlotState>(LaboratorySlotState.Free);
            }
        }

        public void StartMaking(DateTime startTime)
        {
            SetTime(startTime);

            laboratorySlotState.Value = LaboratorySlotState.Making;

            m_DbItem.OnStartProcessing(potionId, startTime);
        }

        public void EndProcessing()
        {
            potionId = TDFacilityLaboratoryTable.dataList[0].unlockPotionID;
            makingRemainTime.Value = -1;
            laboratorySlotState.Value = LaboratorySlotState.Free;
            m_StartTime = default(DateTime);
            m_EndTime = default(DateTime);

            m_DbItem.OnEndProcessing();

        }

        public void OnPartUnSelected()
        {
            potionId = TDFacilityLaboratoryTable.dataList[0].unlockPotionID;
            laboratorySlotState.Value = LaboratorySlotState.Free;
            m_DbItem.OnPartUnSelected();
        }

        public void OnPartSelected(int id)
        {
            potionId = id;
            laboratorySlotState.Value = LaboratorySlotState.Selected;
            m_DbItem.OnPartSelected(id);
        }

        public void RefreshRemainTime()
        {
            if (m_DbItem.laboratorySlotState != LaboratorySlotState.Making)
                return;

            double remainTime = (m_EndTime - DateTime.Now).TotalSeconds;

            makingRemainTime.Value = (float)remainTime;
            if (makingRemainTime.Value <= 0f)
            {
                EndProcessing();
            }
        }

        public void OnProcessingSlotLevelUp()
        {
            if (laboratorySlotState.Value == LaboratorySlotState.Locked && m_LaboratoryModel.tableConfig.unlockSpaceCount >= slotId + 1)
            {
                laboratorySlotState.Value = LaboratorySlotState.Free;

                m_DbItem.OnUnlocked();
            }
        }

        public int GetMakeTime()
        {
            return TDPotionSynthesisConfigTable.GetConfigById(potionId).makeTime;
        }
        #endregion

        private void SetTime(DateTime startTime)
        {
            m_StartTime = startTime;
            m_EndTime = startTime + TimeSpan.FromSeconds(TDPotionSynthesisConfigTable.GetConfigById(potionId).makeTime);
        }
    }

    public class PotionSlotModel 
    {
        public TDPotionSynthesisConfig potionConfig;
        public BoolReactiveProperty isLocked;
        public int unLockLevel;

        private LaboratoryModel m_LaboratoryModel;

        public PotionSlotModel(LaboratoryModel laboratoryModel, int potionId, bool isLocked, int unLockLevel) 
        {
            this.isLocked = new BoolReactiveProperty(true);
            potionConfig = TDPotionSynthesisConfigTable.GetConfigById(potionId);
            this.isLocked.Value = isLocked;
            this.unLockLevel = unLockLevel;
            m_LaboratoryModel = laboratoryModel;
        }

        public void OnLaboratoryLevelUp() 
        {
            if (m_LaboratoryModel.level.Value >= unLockLevel)
            {
                isLocked.Value = false;
            }
            else
            {
                isLocked.Value = true;
            }
        }
        public string GetPotionName() 
        {
            return potionConfig.name;
        }

        public int GetPotionMakeTime() 
        {
            return potionConfig.makeTime;
        }

        public List<ResPair> GetResPairs() 
        {
            return potionConfig.GetMakeResList();
        }


    }

    public enum LaboratorySlotState 
    {
        /// <summary>
        /// 空闲中
        /// </summary>
        Free = 0,
        /// <summary>
        /// 制作中
        /// </summary>
        Making = 1,
        /// <summary>
        /// 未解锁
        /// </summary>
        Locked = 2,
        /// <summary>
        /// 选择但是未开始
        /// </summary>
        Selected = 3,
    }

}