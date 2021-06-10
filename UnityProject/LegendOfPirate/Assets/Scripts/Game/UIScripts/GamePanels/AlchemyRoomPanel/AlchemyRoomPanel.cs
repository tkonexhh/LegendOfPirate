using UnityEngine;
using UnityEngine.UI;
using Qarth.Extension;
using Qarth;
using UniRx;

namespace GameWish.Game
{
	public partial class AlchemyRoomPanel : AbstractAnimPanel
	{
		[SerializeField]
		private USimpleListView m_BottomLibarayRoleUList;
		protected override void OnUIInit()
		{
			base.OnUIInit();
		}
		
		protected override void OnPanelOpen(params object[] args)
		{
			base.OnPanelOpen(args);
			RegisterEvents();

			AllocatePanelData(args);
			
			BindModelToUI();
			BindUIToModel();

			OnClickAddListener();

			InitData();
		}
		private void InitData()
		{
			m_BottomLibarayRoleUList.SetCellRenderer(OnBottomCellRenderer);
			m_BottomLibarayRoleUList.SetDataCount(10);

		}
		private void OnBottomCellRenderer(Transform root, int index)
		{
			Debug.LogError("Index = " + index);
            root.GetComponent<BottomAlchemyPotion>().OnInit(index);
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
			UnregisterEvents();
		}
		
	}
}
