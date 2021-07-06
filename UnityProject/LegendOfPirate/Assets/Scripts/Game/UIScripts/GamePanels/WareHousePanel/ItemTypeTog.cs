using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using Qarth;
using System;

namespace GameWish.Game
{
    public class ItemTypeTog : MonoBehaviour
    {
        #region SerializeField
        [SerializeField] private TextMeshProUGUI m_ItemType;
        [SerializeField] private Toggle m_ItemTypeTog;
        [SerializeField] private GameObject m_SmallIcons;
        #endregion
        #region Data
        private InventoryItemType m_InventoryItemType;
        private WareHousePanel m_WareHousePanel;
        private IDisposable m_ItemTogIDis;

        public InventoryItemType InventoryItemType { get { return m_InventoryItemType; } }
        #endregion

        #region Public
        public void OnInit(InventoryItemType inventoryItemType, WareHousePanel wareHousePanel)
        {
            OnReset();

            m_ItemTypeTog.group = GetComponentInParent<ToggleGroup>();

            m_InventoryItemType = inventoryItemType;
            m_WareHousePanel = wareHousePanel;

            BindUIToModel();

            OnRefresh();
        }

        #endregion

        #region Private
        private void OnReset()
        {
            m_ItemTogIDis?.Dispose();
            //SetIconObj(false);
        }

        private void BindUIToModel()
        {
            m_ItemTogIDis = m_ItemTypeTog.OnValueChangedAsObservable()
                 .Subscribe(on => m_WareHousePanel.OnRefreshItemListByType(m_InventoryItemType))
                 .AddTo(this);
        }

        private void OnRefresh()
        {
            switch (m_InventoryItemType)
            {
                case InventoryItemType.HeroChip:
                    m_ItemType.text = "HeroChip";
                    break;
                case InventoryItemType.Equip:
                    m_ItemType.text = "Equip";
                    break;
                case InventoryItemType.Material:
                    m_ItemType.text = "Material";
                    break;
                case InventoryItemType.Food:
                    m_ItemType.text = "Food";
                    break;
                default:
                    break;
            }
        }
        #endregion
    }
}