using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using Qarth;

namespace GameWish.Game
{
	public class MiddleSelectRole : UListItemView
	{
		#region ÊôÐÔ
		[SerializeField]
		private Button m_MiddleSelectRole;
		[SerializeField]
		private Image m_State;
		#endregion
		#region Data
		private RandomDefenseChooseRoleModule m_RandomDefenseChooseRoleModule;
		#endregion
		#region Method
		public void OnInit(RandomDefenseChooseRoleModule randomDefenseChooseRoleModule, IntReactiveProperty count)
		{
			OnReset();
			m_RandomDefenseChooseRoleModule = randomDefenseChooseRoleModule;
		}

		public void OnReset()
		{
			m_MiddleSelectRole.onClick.RemoveAllListeners();
			m_State.gameObject.SetActive(false);
			m_MiddleSelectRole.OnClickAsObservable().Subscribe(_ =>
			{
				m_RandomDefenseChooseRoleModule.isSelected.Value = !m_RandomDefenseChooseRoleModule.isSelected.Value;
				EventSystem.S.Send(EventID.OnSelectedRole, m_RandomDefenseChooseRoleModule);
				////m_BottomTrainingRoleData.bottomTrainingRoleType.Value = (m_BottomTrainingRoleData.bottomTrainingRoleType.Value. == BottomTrainingRoleType.NotSelected ? BottomTrainingRoleType.Selected : BottomTrainingRoleType.NotSelected);
				//m_IntReactiveIndex.Value += (m_BottomTrainingRoleData.isSelected.Value ? 1 : -1);
				//EventSystem.S.Send(EventID.OnBottomTrainingRole, );
				//OnRefresh();
			});
		}
		public void OnRefresh()
		{
			if (m_RandomDefenseChooseRoleModule.isSelected.Value)
			{
				m_State.gameObject.SetActive(true);
			}
			else
			{
				m_State.gameObject.SetActive(false);
			}
		}

		#endregion
		#region Test4
		#endregion
	}
	
}