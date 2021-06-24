using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Qarth;

namespace GameWish.Game
{
	public class ShipView : ClickableView
	{
        private ShipBody m_ShipBody;

        public ShipBody ShipBody { get => m_ShipBody; }

        private List<Vector3> m_WalkablePosList = null;

        protected override void Awake()
        {
            base.Awake();
        }

        public override int GetSortingLayer()
        {
            return Define.INPUT_SORTING_ORDER_SHIP_UNIT;
        }

        protected override void OnClicked()
        {
            base.OnClicked();

            Log.i("On ShipView Clicked");
        }

        public void Init()
        {
            m_ShipBody = GetComponentInChildren<ShipBody>();
        }

        public List<Vector3> GetWalkablePosList()
        {
            if (m_WalkablePosList == null)
            {
                for (int i = 0; i < m_ShipBody.randomPosList.Count; i++)
                {
                    m_WalkablePosList.Add(m_ShipBody.randomPosList[i].position);
                }
            }

            return m_WalkablePosList;
        }
    }
	
}