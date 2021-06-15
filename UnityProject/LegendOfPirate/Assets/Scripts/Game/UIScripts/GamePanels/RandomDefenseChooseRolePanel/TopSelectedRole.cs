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
	public class TopSelectedRole : MonoBehaviour
	{
		#region  Ù–‘
		[SerializeField]
		private Button m_TopSelectedRole;
		[SerializeField]
		private Image m_State;
		#endregion
		#region Data
		public TopSelectedRoleModule topSelectedRoleModule;
		#endregion

		#region Method

		public void OnReset()
		{
			m_State.gameObject.SetActive(false);
		}

		public void OnInit()
		{
			OnReset();
			topSelectedRoleModule = new TopSelectedRoleModule();
		}

		public void OnRefreshTopSelectedRoleModule(RandomDefenseChooseRoleModule randomDefenseChooseRoleModule)
		{
			topSelectedRoleModule.randomDefenseChooseRoleModule.Value = randomDefenseChooseRoleModule;
			OnRefresh();
		}
		public void ClearSelf()
		{
			topSelectedRoleModule.randomDefenseChooseRoleModule.Value = null;
			OnRefresh();
		}

		private void OnRefresh()
        {
            if (topSelectedRoleModule.isEmpty.Value)
            {
				m_State.gameObject.SetActive(false);
			}
            else
            {
				m_State.gameObject.SetActive(true);
			}
		}
        #endregion



        #region Test4
        #endregion
    }
	
}