using UnityEngine;
using UnityEngine.UI;
using Qarth.Extension;
using Qarth;
using UniRx;

namespace GameWish.Game
{
	public partial class RoleGroupPanel : AbstractAnimPanel
	{
		protected override void OnUIInit()
		{
			base.OnUIInit();
            AllocatePanelData();

            BindModelToUI();
            BindUIToModel();
			InitUIListener();
		}
		
		protected override void OnPanelOpen(params object[] args)
		{
			base.OnPanelOpen(args);

			OnOpenInit(args);
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

        private void InitUIListener()
        {

            m_CloseBtn.OnClickAsObservable().Subscribe(_ => HideSelfWithAnim()).AddTo(this);
            var toggles = m_ToggleGroup.GetComponentsInChildren<Toggle>();
          
            toggles[0].OnValueChangedAsObservable().Where(ison=>ison).Subscribe(_ => SetGroupList(0)).AddTo(this);
			toggles[1].OnValueChangedAsObservable().Where(ison => ison).Subscribe(_ => SetGroupList(1)).AddTo(this);
			toggles[2].OnValueChangedAsObservable().Where(ison => ison).Subscribe(_ => SetGroupList(2)).AddTo(this);
			toggles[3].OnValueChangedAsObservable().Where(ison => ison).Subscribe(_ => SetGroupList(3)).AddTo(this);
		}
    }
}
