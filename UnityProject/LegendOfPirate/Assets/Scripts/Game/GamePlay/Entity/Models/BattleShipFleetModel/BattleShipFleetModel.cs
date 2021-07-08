using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System.Linq;
namespace GameWish.Game
{
    [ModelAutoRegister]
    public class BattleShipFleetModel : DbModel
    {
        public List<BattleShipModel> battleShipModels = new List<BattleShipModel>();
        private BattleShipFleetData dbData;

        protected override void LoadDataFromDb()
        {
            dbData = GameDataMgr.S.GetData<BattleShipFleetData>();
            foreach (var item in dbData.fleetDataList)
            {
                battleShipModels.Add(new BattleShipModel(item));
            }
        }

        public BattleShipModel GetBattleShipModel(int ShipId) 
        {
            return battleShipModels.FirstOrDefault(ship => ship.GetId() == ShipId);
        }

        public int GetBattleShipIndexById(int ShipId) 
        {

            for (int index = 0; index < battleShipModels.Count; index++) 
            {
               if(battleShipModels[index].GetId() == ShipId) 
                {
                    return index;
                }
            }
            return -1;
        }

        public int GetUnlockedShipCount() 
        {
            int ret = 0;
            for (int index = 0; index < battleShipModels.Count; index++)
            {
                if (!battleShipModels[index].isLocked.Value)
                {
                    ret++;
                }
            }
            return ret;
        }
    }

    public class BattleShipModel
    {
        public IntReactiveProperty shipLevel;
        public BoolReactiveProperty isLocked;
        private BattleShipData shipData;
        private BattleShipUnitConfig config;

        public BattleShipModel(BattleShipData shipData)
        {
            this.shipLevel = new IntReactiveProperty(shipData.battleShipLevel);
            this.shipData = shipData;
            this.isLocked = new BoolReactiveProperty(shipData.isLocked);
            config = TDFacilityBattleshipTable.GetConfigById(shipData.battleShipId);

            ModelSubscribe();
        }

        #region Public Get
        public BattleShipUnitConfig GetShipConfig()
        {
            return config;
        }

        public string GetShipName()
        {
            return config.battleShipName;
        }

        public float GetAtk()
        {
            return config.ATKCount;
        }

        public float GetArmor()
        {
            return config.ArmorCount;
        }

        public float GetHp()
        {
            return config.HPCount;
        }

        public int GetId() 
        {
            return config.warShipId;
        }
        #endregion

        #region Public Set
        public void AddShipLevel(int delta = 1)
        {
            this.shipLevel.Value++;

        }

        public void UnlockShip() 
        {
            this.isLocked.Value = false;
        }
        #endregion

        #region Private
        //注册战船模型事件
        private void ModelSubscribe()
        {
            isLocked.Subscribe(islock =>
            {
                if (!islock) shipData.UnlockBattleShip();
            });

            var shipModel = ModelMgr.S.GetModel<ShipModel>();  
        }

        #endregion
    }

}