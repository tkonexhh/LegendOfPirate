using System.Collections;
using System.Collections.Generic;
using System;
using UniRx;
using UnityEngine;

namespace GameWish.Game
{
    public class ProcessingRoomModel : ShipUnitModel
    {
        public ProcessingRoomUnitConfig tableConfig;
        public ReactiveCollection<ProcessingSlotModel> processingSlotModelList = new ReactiveCollection<ProcessingSlotModel>();
        public List<PartSlotModel> ProcessingPartModelList = new List<PartSlotModel>();

        private ProcessingData m_DbData;
        public ProcessingRoomModel(ShipUnitData shipUnitData) : base(shipUnitData)
        {
            tableConfig = TDFacilityProcessingRoomTable.GetConfig(level.Value);
            foreach (var item in TDFacilityProcessingRoomTable.dataList) 
            {
                var UnlockParts = item.GetUnlockPartId();
                foreach (var unLockPart in UnlockParts) 
                {
                    ProcessingPartModelList.Add(new PartSlotModel(this,unLockPart, level.Value < item.level,item.level));
                }
            }
            m_DbData = GameDataMgr.S.GetData<ProcessingData>();
            if (tableConfig.unlockPartSpace >= Define.PROCESSING_ROOM_DEFAULT_SLOT_COUNT)
            {
                for (int i = 0; i < tableConfig.unlockPartSpace; i++) 
                {
                    processingSlotModelList.Add(new ProcessingSlotModel(this, m_DbData.processingItemList[i], i));
                }
            }
            else 
            {
                for (int i = 0; i < Define.PROCESSING_ROOM_DEFAULT_SLOT_COUNT; i++)
                {
                    processingSlotModelList.Add(new ProcessingSlotModel(this, m_DbData.processingItemList[i], i));
                }
            }
        }

        public ProcessingSlotModel GetSelectModel() 
        {
         
            foreach (var item in processingSlotModelList)
            {
                if (item.processState.Value == ProcessSlotState.Selected)
                    return item;
            }
            return null;
        }

        public ProcessingSlotModel GetAvailableSlot() 
        {
            foreach (var item in processingSlotModelList) 
            {
                if (item.processState.Value == ProcessSlotState.Free)
                    return item;
            }
            return null;
        }

        public override void OnLevelUpgrade(int delta)
        {
            base.OnLevelUpgrade(delta);

            tableConfig = TDFacilityProcessingRoomTable.GetConfig(level.Value);
            for (int i = 0; i < ProcessingPartModelList.Count; i++) 
            {
                ProcessingPartModelList[i].OnProcessingRoomLevelUp();
            }
            if (tableConfig.unlockPartSpace > Define.PROCESSING_ROOM_DEFAULT_SLOT_COUNT&&tableConfig.unlockPartSpace>processingSlotModelList.Count) 
            {
                processingSlotModelList.Add(new ProcessingSlotModel(this, m_DbData.processingItemList[processingSlotModelList.Count], processingSlotModelList.Count));
            }
            for (int i = 0; i < processingSlotModelList.Count; i++)
            {
                processingSlotModelList[i].OnProcessingSlotLevelUp();
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
                foreach (var item in processingSlotModelList) 
                {
                    item.RefreshRemainTime();               
                }
            }
        }
    }

    public class ProcessingSlotModel : Model
    {
        public int partId;
        public int slotId;
        public FloatReactiveProperty ProcessingRemainTime = new FloatReactiveProperty();

        public ReactiveProperty<ProcessSlotState> processState;

        private DateTime m_StartTime = default(DateTime);
        private DateTime m_EndTime = default(DateTime);

        private ProcessingRoomModel m_ProcessingRoomModel;
        private ProcessingSlotDataItem m_DbItem;

        #region public
        public ProcessingSlotModel(ProcessingRoomModel processingRoomModel, ProcessingSlotDataItem dbItem,int slotid)
        {
            m_ProcessingRoomModel = processingRoomModel;
            m_DbItem = dbItem;
            this.slotId = slotid;

            if (slotId+1 > processingRoomModel.tableConfig.unlockPartSpace) 
            {
                processState = new ReactiveProperty<ProcessSlotState>(ProcessSlotState.Locked);
            }
            else 
            {
                processState = new ReactiveProperty<ProcessSlotState>(ProcessSlotState.Free);
            }

        }

        public void StartProcessing(DateTime startTime) 
        {
            SetTime(startTime);

            processState.Value = ProcessSlotState.Processing;

            m_DbItem.OnStartProcessing(partId, startTime);
        }

        public void EndProcessing() 
        {
            partId = TDFacilityProcessingRoomTable.dataList[0].GetUnlockPartId()[0];
            ProcessingRemainTime.Value = -1;
            processState.Value = ProcessSlotState.Free;
            m_StartTime = default(DateTime);
            m_EndTime = default(DateTime);

            m_DbItem.OnEndProcessing();
            
        }

        public void OnPartUnSelected()
        {
            partId= TDFacilityProcessingRoomTable.dataList[0].GetUnlockPartId()[0];
            processState.Value = ProcessSlotState.Free;
            m_DbItem.OnPartUnSelected();
        }

        public void OnPartSelected(int id) 
        {
            partId = id;
            processState.Value = ProcessSlotState.Selected;
            m_DbItem.OnPartSelected(id);
        }

        public void RefreshRemainTime() 
        {
            if (m_DbItem.progressSlotState != ProcessSlotState.Processing)
                return;

            double remainTime = (m_EndTime - DateTime.Now).TotalSeconds;

            ProcessingRemainTime.Value = (float)remainTime;
            if (ProcessingRemainTime.Value <= 0f)
            {
                EndProcessing();
            }
        }

        public void OnProcessingSlotLevelUp() 
        {
            if (processState.Value == ProcessSlotState.Locked && m_ProcessingRoomModel.tableConfig.unlockPartSpace >= slotId+1)
            {
                processState.Value = ProcessSlotState.Free;

                m_DbItem.OnUnlocked();
            }
        }

        public int GetMakeTime() 
        {
            return TDPartSynthesisConfigTable.GetConfigById(partId).makeTime;
        }
        #endregion
        
        private void SetTime(DateTime startTime)
        {
            m_StartTime = startTime;
            m_EndTime = startTime + TimeSpan.FromSeconds(TDPartSynthesisConfigTable.GetConfigById(partId).makeTime);
        }

    }

    public class PartSlotModel : Model 
    {
        public TDPartSynthesisConfig partConfig;
        public BoolReactiveProperty isLocked;
        public int unLockLevel;

        private ProcessingRoomModel m_ProcessingRoomModel;

        public PartSlotModel(ProcessingRoomModel processingRoomModel ,int partId,bool isLocked,int unLockLevel) 
        {
            this.isLocked = new BoolReactiveProperty(true);
            partConfig = TDPartSynthesisConfigTable.GetConfigById(partId);
            this.isLocked.Value = isLocked;
            this.unLockLevel = unLockLevel;
            m_ProcessingRoomModel = processingRoomModel;
        }

        public void OnProcessingRoomLevelUp() 
        {
            if (m_ProcessingRoomModel.level.Value >= unLockLevel)
            {
                isLocked.Value = false;
            }
            else 
            {
                isLocked.Value = true;
            }
        }

        public string GetPartName() 
        {
            return TDMaterialConfigTable.GetConfigById(partConfig.id).materialName;
               
        }

        public int GetPartMakeTime() 
        {
            return partConfig.makeTime;
        }

        public List<ResPair> GetResPairs() 
        {
            return partConfig.GetMakeResList();
        }
    }

    public enum ProcessSlotState
    {
        /// <summary>
        /// 空闲中
        /// </summary>
        Free = 0,
        /// <summary>
        /// 加工中
        /// </summary>
        Processing = 1,
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