using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using Qarth;


namespace GameWish.Game
{
	public class DailySelectionItem : MonoBehaviour
	{
        #region SerializeField
        [SerializeField] private Button m_DailySelectionItemBtn;
        [SerializeField] private GameObject m_LookAdvState;
        [SerializeField] private GameObject m_PurchaseState;
        [SerializeField] private Toggle m_PurchaseStateTog;
        [SerializeField] private GameObject m_PriceState;
        [SerializeField] private TextMeshProUGUI m_PriceValue;
        #endregion
    }
}