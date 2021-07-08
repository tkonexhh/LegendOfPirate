using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;


namespace GameWish.Game
{
    [ModelAutoRegister]
    public class SmuggleModel : DbModel
    {

        public List<SmuggleOrderModel> orderModelList = new List<SmuggleOrderModel>();
        private  SmuggleData smuggleData;
        

        protected override void LoadDataFromDb()
        {
           
            smuggleData = GameDataMgr.S.GetData<SmuggleData>();
            for (int i = 0; i<TDSmuggleTable.dataList.Count; i++) 
            {
                orderModelList.Add(new SmuggleOrderModel(this, TDSmuggleTable.dataList[i].id,smuggleData.orderDataList[i]));
            }
        }
    }

    public class SmuggleOrderModel
    {
        public int smuggingOrderId;
        public BoolReactiveProperty isLocked;
        public int unlockedLevel;
        public SmuggleUnitConfig tableConfig;
        public ReactiveProperty<OrderState> orderState;

        private SmuggleModel m_SmuggingModel;
        private SmuggleOrderData m_OrderData;

  

        public SmuggleOrderModel(SmuggleModel smuggingModel, int smuggingOrderId,SmuggleOrderData orderData)
        {
            this.smuggingOrderId = smuggingOrderId;
            var battleShipFleet = ModelMgr.S.GetModel<BattleShipFleetModel>();
            tableConfig = TDSmuggleTable.GetConfigById(smuggingOrderId);
            unlockedLevel = smuggingOrderId;
            m_OrderData = orderData;
            if (isLocked == null)
            {
                isLocked = new BoolReactiveProperty(battleShipFleet.GetUnlockedShipCount() < smuggingOrderId);
            }
            else 
            {
                isLocked.Value = battleShipFleet.GetUnlockedShipCount() < smuggingOrderId;
            }

        }

        public void OnWarShipLevelChange()
        {

        }
    }

    public enum OrderState
    {
        Locked = 0,
        Free = 1,
        Smugging = 2,
        Complate = 3,
        Done=4,
    }
}