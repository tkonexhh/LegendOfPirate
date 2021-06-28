using UnityEngine;
using UnityEngine.UI;
using Qarth.Extension;
using Qarth;
using UniRx;

namespace GameWish.Game
{
    public class LevelPeviewPanelData : UIPanelData
    {
        public MainSeaLevelModel mainSeaLevelModel;
        public LevelPeviewPanelData()
        {
        }
    }

    public partial class LevelPeviewPanel
    {
        private LevelPeviewPanelData m_PanelData = null;

        private void AllocatePanelData()
        {
            m_PanelData = UIPanelData.Allocate<LevelPeviewPanelData>();
            m_PanelData.mainSeaLevelModel = new MainSeaLevelModel();
        }

        private void ReleasePanelData()
        {
            ObjectPool<LevelPeviewPanelData>.S.Recycle(m_PanelData);
        }

        private void BindModelToUI()
        {
            m_PanelData.mainSeaLevelModel.curLevelID.AsObservable().SubscribeToTextMeshPro(m_TmpTitleText, "Level {0}");
            m_PanelData.mainSeaLevelModel.curLevelType.AsObservable().SubscribeToTextMeshPro(m_TmpSourceText);
        }

        private void BindUIToModel()
        {
        }

        private void OnClickAddListener()
        {
            m_BtnAttack.OnClickAsObservable().Subscribe(_ => { OnAttackClicked(); }).AddTo(this);
            m_BtnClose.OnClickAsObservable().Subscribe(_ => { OnBackClicked(); }).AddTo(this);
        }
        private void OnBackClicked()
        {
            CloseSelfPanel();
            UIMgr.S.OpenPanel(UIID.MainSeaLevelPanel);
        }

        private void OnAttackClicked()
        {
            CloseSelfPanel();
            UIMgr.S.ClosePanelAsUIID(UIID.MainSeaLevelPanel);
            UIMgr.S.OpenPanel(UIID.BattlePreparePanel, null, m_PanelData.mainSeaLevelModel.curLevelID.Value);
        }

        private void SetCurLevelID(int levelID)
        {
            m_PanelData.mainSeaLevelModel.curLevelID.Value = levelID;
            m_PanelData.mainSeaLevelModel.InitContent();
        }
    }
}
