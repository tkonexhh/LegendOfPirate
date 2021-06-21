using System.Collections;
using System.Collections.Generic;
using System;
using UniRx;

namespace GameWish.Game
{
    public class ProcessingRoomModel : ShipUnitModel
    {
        public ProcessingRoomUnitConfig tableConfig;
        public List<ProcessingSlotModel> slotModelList = new List<ProcessingSlotModel>();

        private ProcessingData m_DbData;
        public ProcessingRoomModel(ShipUnitData shipUnitData) : base(shipUnitData)
        {
            tableConfig = TDFacilityProcessingRoomTable.GetConfig(level.Value);
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
        private ProcessingData m_DbData;

        public ProcessingSlotModel(ProcessingRoomModel processingRoomModel, ProcessingData dbItem)
        {
            m_ProcessingRoomModel = processingRoomModel;
            m_DbData = dbItem;
        }

        private void SetTime(DateTime startTime)
        {
            m_StartTime = startTime;
            m_EndTime = startTime + TimeSpan.FromSeconds(TDPartSynthesisConfigTable.GetConfigById(partId).makeTime);
        }

    }
}