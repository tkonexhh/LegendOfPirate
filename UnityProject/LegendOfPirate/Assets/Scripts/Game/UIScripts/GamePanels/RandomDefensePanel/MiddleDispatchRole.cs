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
	public class MiddleDispatchRole : MonoBehaviour
	{
        #region ÊôÐÔ
        [SerializeField]
        private Button m_MiddleDispatchRole;
        [SerializeField]
        private Image m_Bg;
        #endregion

        private void Start()
        {
            m_MiddleDispatchRole.OnClickAsObservable().Subscribe(_ => {
                UIMgr.S.OpenTopPanel(UIID.RandomDefenseChooseRolePanel,Callback);
            });
        }

        private void Callback(AbstractPanel obj)
        {

        }

        public void OnInit()
        {
            
        }
    }
}