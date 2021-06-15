using UnityEngine;
using UnityEngine.UI;
using Qarth.Extension;
using Qarth;
using UniRx;

namespace GameWish.Game
{
	public partial class RandomDefensePanel : AbstractAnimPanel
	{
		[SerializeField]
		private GameObject m_MiddleDispatchRole;
		[SerializeField]
		private GameObject m_MiddleDownAwardRole;

		#region Data
		#endregion

		#region AbstractAnimPanel
		protected override void OnUIInit()
		{
			base.OnUIInit();
		}

		protected override void OnPanelOpen(params object[] args)
		{
			base.OnPanelOpen(args);

			AllocatePanelData(args);

			BindModelToUI();
			BindUIToModel();
			OnClickAddListener();

			InitData();
		}

		protected override void OnPanelHideComplete()
		{
			base.OnPanelHideComplete();

			CloseSelfPanel();
		}

		protected override void OnClose()
		{
			base.OnClose();

			ReleasePanelData();
		}
        #endregion
        #region ButtonEvent
        public void BlackExitBgEvent()
        {
			HideSelfWithAnim();
        }
        #endregion

        #region EventSystem
        private void HandlerEvent(int key, object[] param)
		{
			
		}
		#endregion

		#region Other Method
		private void InitData()
		{

            for (int i = 0; i < 3; i++)
            {
				CeateMiddleDispatchRole();
				CeateMiddleDownAwardRole();
			}
		}


		public void CeateMiddleDispatchRole()
		{
			Instantiate(m_MiddleDispatchRole, DispatchTra.transform).SetActive(true);
		}
		public void CeateMiddleDownAwardRole()
		{
			Instantiate(m_MiddleDownAwardRole, AwardTra.transform).SetActive(true);
		}
		#endregion

	}
}
