using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace GameWish.Game
{
    [ModelAutoRegister]
    public class ShipModel : DbModel
	{
        public IntReactiveProperty shipLevel;
        public ReactiveCollection<ShipUnitModel> shipUnitModelList = null;

        private ShipData m_ShipData = null;

        protected override void LoadDataFromDb()
        {
            m_ShipData = GameDataMgr.S.GetData<ShipData>();

            shipUnitModelList = new ReactiveCollection<ShipUnitModel>();
            for (int i = 0; i < m_ShipData.shipUnitDataList.Count; i++)
            {
                ShipUnitModel unitModel = new ShipUnitModel(m_ShipData.shipUnitDataList[i]);
                shipUnitModelList.Add(unitModel);
            }
        }
    }

}