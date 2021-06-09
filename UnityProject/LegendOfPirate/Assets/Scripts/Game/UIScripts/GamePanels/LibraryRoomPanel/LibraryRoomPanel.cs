using UnityEngine;
using UnityEngine.UI;
using Qarth.Extension;
using Qarth;
using UniRx;

namespace GameWish.Game
{
	public partial class LibraryRoomPanel : AbstractAnimPanel
	{
		[SerializeField]
		private UGridListView m_MiddleLibarayRoleUGridList;
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
		
		protected override void OnPanelHideComplete()
		{
			base.OnPanelHideComplete();
			
			CloseSelfPanel();
		}
		private void InitData()
		{

			//TitleIcon.sprite = SpriteHandler.S.GetSprite(AtlasDefine.TestAtlas, iconName);

			m_MiddleLibarayRoleUGridList.SetCellRenderer(OnMiddleCellRenderer);
			m_BottomLibarayRoleUList.SetCellRenderer(OnBottomCellRenderer);

			m_MiddleLibarayRoleUGridList.SetDataCount(10);
			m_BottomLibarayRoleUList.SetDataCount(10);
        }
		private void OnBottomCellRenderer(Transform root, int index)
		{
			Debug.LogError("Index = " + index);
			root.GetComponent<BottomLibraryRole>().OnInit(index);
		}

		private void OnMiddleCellRenderer(Transform root, int index)
		{
			Debug.LogError("Index = " + index);
			root.GetComponent<MiddleLibraryRole>().OnInit(index);
		}
		protected override void OnClose()
		{
			base.OnClose();
			
			ReleasePanelData();
			UnregisterEvents();
		}
		
	}
}
