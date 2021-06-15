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
		public void OnInit()
		{
			topSelectedRoleModule = new TopSelectedRoleModule();
		}

		//public int GetRoleID()
		//{
  //          if (topSelectedRoleModule.isEmpty.Value)
  //          {

  //          }
		//	//return topSelectedRoleModule.randomDefenseChooseRoleModule.GetRoleID();
		//}
		public void OnRefreshTopSelectedRoleModule(RandomDefenseChooseRoleModule randomDefenseChooseRoleModule)
		{
			topSelectedRoleModule.randomDefenseChooseRoleModule.Value = randomDefenseChooseRoleModule;
			OnRefresh();
		}

        private void OnRefresh()
        {
        }
        #endregion



        #region Test4
        #endregion
    }
	
}