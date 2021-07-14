using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System.Linq;
using Qarth;

namespace GameWish.Game
{
    [ModelAutoRegister]
    public class ShipModel : DbModel
	{
        public IntReactiveProperty shipLevel;
        public ReactiveCollection<ShipUnitModel> shipUnitModelList = null;

        private ShipData m_ShipData = null;

        public override void OnUpdate()
        {
            for (int i = 0; i < shipUnitModelList.Count; i++)
            {
                shipUnitModelList[i].OnUpdate();
            }
        }

        public override void OnDestroyed()
        {
            for (int i = 0; i < shipUnitModelList.Count; i++)
            {
                shipUnitModelList[i].OnDestroyed();
            }
        }

        protected override void LoadDataFromDb()
        {
            m_ShipData = GameDataMgr.S.GetData<ShipData>();

            shipUnitModelList = new ReactiveCollection<ShipUnitModel>();
            for (int i = 0; i < m_ShipData.shipUnitDataList.Count; i++)
            {
                ShipUnitModel unitModel = ShipUnitModelFactory.CreateUnitModel(m_ShipData.shipUnitDataList[i]);
                shipUnitModelList.Add(unitModel);
            }
        }

        public ShipUnitModel GetShipUnitModel(ShipUnitType shipUnitType)
        {
            ShipUnitModel model = shipUnitModelList.FirstOrDefault(i => i.unitType == shipUnitType);
            if (model == null)
            {
                Log.e("Ship Unit Model Not Found: " + shipUnitType.ToString());
            }

            return model;
        }

        public T GetShipUnitModel<T>(ShipUnitType shipUnitType) where T : ShipUnitModel
        {
            ShipUnitModel model = GetShipUnitModel(shipUnitType);
            return model as T;
        }
    }

}