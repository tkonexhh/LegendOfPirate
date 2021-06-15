using UnityEngine;
using UnityEngine.UI;
using Qarth.Extension;
using Qarth;
using UniRx;
using System.Linq;

namespace GameWish.Game
{
    #region Other Data Class
    public class BottomLibraryRoleModule
    {
        public IntReactiveProperty index;
        public BoolReactiveProperty isSelected;

        public BottomLibraryRoleModule(int index, bool selected)
        {
            this.index = new IntReactiveProperty(index);
            this.isSelected = new BoolReactiveProperty(selected);
        }
    }

    public class MiddleLibraryRoleModule
    {
        public IntReactiveProperty index;

        public MiddleLibraryRoleModule(int index)
        {
            this.index = new IntReactiveProperty(index);
        }
    }
    #endregion

    public partial class LibraryRoomPanel : AbstractAnimPanel
    {
        [SerializeField]
        private ScrollRectAutoAdjustPosition m_ScrollRectAutoAdjustPosition;
        [SerializeField]
        private UGridListView m_MiddleLibarayRoleUGridList;
        [SerializeField]
        private USimpleListView m_BottomLibarayRoleUList;
        #region Data
        private IntReactiveProperty m_SelectedCount = new IntReactiveProperty(0);
        private ReactiveCollection<BottomLibraryRoleModule> bottomLibraryRoleDatas = new ReactiveCollection<BottomLibraryRoleModule>();
        private ReactiveCollection<MiddleLibraryRoleModule> middleLibraryRoleDatas = new ReactiveCollection<MiddleLibraryRoleModule>();

        #endregion
        #region AbstractAnimPanel
        protected override void OnUIInit()
        {
            base.OnUIInit();
        }

        protected override void OnPanelOpen(params object[] args)
        {
            base.OnPanelOpen(args);
            RegisterEvents();

            OpenDependPanel(EngineUI.MaskPanel, -1, null);

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

            CloseDependPanel(EngineUI.MaskPanel);
        }
        #endregion
        #region ButtonEvent
        public void LibraryUpgradeBtnEvent()
        {

        }
        public void TrainBtnEvent()
        {

        }
        public void AutoTrainBtnEvent()
        {
        }
        public void BgBtnEvent()
        {
            HideSelfWithAnim();
        }
        public void LeftArrowBtnEvent()
        {
            m_ScrollRectAutoAdjustPosition.Move2Pre();
        }
        public void RightArrowBtnEvent()
        {
            m_ScrollRectAutoAdjustPosition.Move2Next();
        }
        #endregion
        #region Other Method
        private void InitData()
        {
            BindUniRxUI();
            //TitleIcon.sprite = SpriteHandler.S.GetSprite(AtlasDefine.TestAtlas, iconName);

            m_MiddleLibarayRoleUGridList.SetCellRenderer(OnMiddleCellRenderer);
            m_BottomLibarayRoleUList.SetCellRenderer(OnBottomCellRenderer);

            m_ScrollRectAutoAdjustPosition.EnableAutoAdjust(10);

            m_MiddleLibarayRoleUGridList.SetDataCount(10);
            m_BottomLibarayRoleUList.SetDataCount(10);
        }
        private void OnBottomCellRenderer(Transform root, int index)
        {
            BottomLibraryRoleModule bottomLibraryRoleModule = bottomLibraryRoleDatas.FirstOrDefault(item => item.index.Value == index);
            if (bottomLibraryRoleModule != null)
            {
                root.GetComponent<BottomLibraryRole>().OnInit(bottomLibraryRoleModule, m_SelectedCount);
            }
            else
            {
                BottomLibraryRoleModule newBottomLibraryRoleModule = new BottomLibraryRoleModule(index, false);
                bottomLibraryRoleDatas.Add(newBottomLibraryRoleModule);
                root.GetComponent<BottomLibraryRole>().OnInit(newBottomLibraryRoleModule, m_SelectedCount);
            }
        }

        private void OnMiddleCellRenderer(Transform root, int index)
        {
            MiddleLibraryRoleModule middleLibraryRoleModule = middleLibraryRoleDatas.FirstOrDefault(item => item.index.Value == index);
            if (middleLibraryRoleModule != null)
            {
                root.GetComponent<MiddleLibraryRole>().OnInit(middleLibraryRoleModule);
            }
            else
            {
                MiddleLibraryRoleModule newMiddleLibraryRoleModule = new MiddleLibraryRoleModule(index);
                middleLibraryRoleDatas.Add(newMiddleLibraryRoleModule);
                root.GetComponent<MiddleLibraryRole>().OnInit(newMiddleLibraryRoleModule);
            }
        }
        protected override void OnClose()
        {
            base.OnClose();

            ReleasePanelData();
            UnregisterEvents();
        }
        private void BindUniRxUI()
        {
            m_SelectedCount.Select(count => count + "/" + 10).SubscribeToTextMeshPro(RoleSelectNumberTMP);
        }
        #endregion
    }
}
