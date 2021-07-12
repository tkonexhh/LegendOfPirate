using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System.Linq;

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

        public SmuggleOrderModel GetSmuggleOrderModel(int id) 
        {
            return orderModelList.FirstOrDefault(order => order.smuggingOrderId == id);

        }

    }

    public class SmuggleOrderModel
    {
        public int smuggingOrderId;
        public BoolReactiveProperty isLocked;
        public int unlockedLevel;
        public SmuggleUnitConfig tableConfig;
        public ReactiveProperty<OrderState> orderState;
        public List<RoleModel> roleList;
        public BoolReactiveProperty needRefresh;

        private SmuggleModel m_SmuggingModel;
        private SmuggleOrderData m_OrderData;

  

        public SmuggleOrderModel(SmuggleModel smuggingModel, int smuggingOrderId,SmuggleOrderData orderData)
        {
            orderState = new ReactiveProperty<OrderState>(orderData.orderState);
            m_SmuggingModel = smuggingModel;
            m_OrderData = orderData;
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
            roleList = new List<RoleModel>();
            needRefresh = new BoolReactiveProperty(false);

            ModelSubscribe();
        }

        public void OnWarShipLevelChange()
        {
            var shipCount = ModelMgr.S.GetModel<BattleShipFleetModel>().GetUnlockedShipCount();
            isLocked.Value = shipCount > unlockedLevel;
        }

        public void AddRoleModel(int roleId) 
        {
            if (roleList.Count >= 5) return;
            if (roleList.Any(r => r.id == roleId)) return;
            var role = ModelMgr.S.GetModel<RoleGroupModel>().GetRoleModel(roleId);
            roleList.Add(role);
            needRefresh.Value = true;
        }

        public void RemoveRoleModel(int roleId) 
        {
            if (!roleList.Any(role => role.id == roleId)) return;
            var findedRole = roleList.First(role => role.id == roleId);
            roleList.Remove(findedRole);
        }

        private void ModelSubscribe()
        {
            var battleShip = ModelMgr.S.GetModel<BattleShipFleetModel>();
            battleShip.UnlockedShipCount.Subscribe(shipCount =>OnShipCountChange(shipCount));
        }

        private void OnShipCountChange(int shipCount) 
        {
           isLocked.Value= shipCount < smuggingOrderId;

        }
    }

    public enum OrderState
    {
        Free = 1,
        Smugging = 2,
        Complate = 3,
        Done=4,
    }
}