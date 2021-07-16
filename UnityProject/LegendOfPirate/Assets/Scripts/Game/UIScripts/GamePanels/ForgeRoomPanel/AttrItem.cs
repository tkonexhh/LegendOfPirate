using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GameWish.Game
{
	public class AttrItem : MonoBehaviour
	{
        #region SerilizeField
        [SerializeField] private Image m_AttrItem;
        [SerializeField] private TextMeshProUGUI m_AttrTitle;
        [SerializeField] private TextMeshProUGUI m_AttrValue;
        #endregion
        public void OnInit()
        {
            HideSelf();
        }

        public void HideSelf()
        {
            m_AttrItem.gameObject.SetActive(false);
        }

        public void ShowAttr(EquipAttributeValue equipAttributeValue)
        {
            m_AttrTitle.text = equipAttributeValue.equipAttrType.ToString();
            m_AttrValue.text = equipAttributeValue.percentage.ToString();
            m_AttrItem.gameObject.SetActive(true);
        }
    }
}