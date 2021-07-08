using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Qarth;

namespace GameWish.Game
{
    public class BattleShipFleetData : IDataClass
    {
        public List<BattleShipData> fleetDataList = new List<BattleShipData>();

        public override void InitWithEmptyData()
        {
            var mianShipData = GameDataMgr.S.GetData<ShipData>();
            var configs = TDFacilityBattleshipTable.GetDefaultConfig();

            foreach (var config in configs) 
            {
                fleetDataList.Add(
                    new BattleShipData( config.warShipId, 
                                     config.unlockAccountLevel,
                                     mianShipData.shipLevel < config.unlockAccountLevel)
                    );
            }
        }
    }

    [SerializeField]
    public struct BattleShipData 
    {
        public int battleShipId;
        public int battleShipLevel;
        public int mainShipLimLevel;
        public bool isLocked;

        private BattleShipFleetData m_FleetData;

        public BattleShipData(int warShipId,int mainShipLimLevel,bool isLocked) 
        {
            m_FleetData = null;
            this.battleShipId = warShipId;
            battleShipLevel = warShipId%100;
            this.mainShipLimLevel = mainShipLimLevel;
            this.isLocked = isLocked;
        }

        public void OnMainShipLevelUp() 
        {
            isLocked = mainShipLimLevel >= ModelMgr.S.GetModel<ShipModel>().shipLevel.Value;
            
            SetDataDirty();
        }

        public void UnlockBattleShip() 
        {
            isLocked = false;
            SetDataDirty();
        }

        public void OnBattleShipUpdate() 
        {
            battleShipId++;
            battleShipLevel = battleShipId % 100;
            var config = TDFacilityBattleshipTable.GetConfigById(battleShipId);
            this.mainShipLimLevel = config.unlockAccountLevel;
            
            SetDataDirty();
        }
        private void SetDataDirty() 
        {
            if (m_FleetData == null) 
            {
                m_FleetData = GameDataMgr.S.GetData<BattleShipFleetData>();
            }
            m_FleetData.SetDataDirty();
        }
        
    }
}