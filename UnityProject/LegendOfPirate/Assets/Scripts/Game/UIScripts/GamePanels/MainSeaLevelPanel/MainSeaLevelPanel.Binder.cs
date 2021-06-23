using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using Qarth.Extension;
using Qarth;
using UniRx;

namespace GameWish.Game
{
    public class MainSeaLevelPanelData : UIPanelData
    {
        public MainSeaLevelModel mainSeaLevelModel;
        public IntReactiveProperty currentLevelId;
        public List<GameObject> allLevelItems;
        public MainSeaLevelPanelData()
        {
        }
    }

    public partial class MainSeaLevelPanel
    {
        private MainSeaLevelPanelData m_PanelData = null;

        private void AllocatePanelData()
        {
            m_PanelData = UIPanelData.Allocate<MainSeaLevelPanelData>();
            m_PanelData.mainSeaLevelModel = new MainSeaLevelModel();
        }

        private void ReleasePanelData()
        {
            ObjectPool<MainSeaLevelPanelData>.S.Recycle(m_PanelData);
        }

        private void BindModelToUI()
        {
            m_PanelData.currentLevelId = new IntReactiveProperty();
            m_PanelData.currentLevelId.Value = 101;
            m_PanelData.currentLevelId.AsObservable().SubscribeToTextMeshPro(m_CurrentLevel, "LEVEL {0}").AddTo(this);
            m_PanelData.allLevelItems = new List<GameObject>();
        }

        private void BindUIToModel()
        {
        }

        private void OnClickAddListener()
        {
            m_BackBtn.OnClickAsObservable().Subscribe(_ => OnBackClicked()).AddTo(this);
            m_AttackBtn.OnClickAsObservable().Subscribe(_ => OnAttackClicked()).AddTo(this);
        }

        private void OnBackClicked()
        {
            CloseSelfPanel();
            UIMgr.S.OpenPanel(UIID.MainMenuPanel);
        }

        private void OnAttackClicked()
        {
            CloseSelfPanel();
            UIMgr.S.OpenPanel(UIID.BattlePreparePanel, null, m_PanelData.currentLevelId.Value);
        }
        private void CreateLevel()
        {
            //m_Content.DestroyChildren();
            for (int i = 0; i < m_PanelData.mainSeaLevelModel.GetBattleLevelCount(); i++)
            {
                GameObject levelObj = null;
                if (m_PanelData.allLevelItems == null || m_PanelData.allLevelItems.Count < m_PanelData.mainSeaLevelModel.GetBattleLevelCount())
                {
                    levelObj = Instantiate(m_LevelItem.gameObject);
                    m_PanelData.allLevelItems.Add(levelObj);
                }
                else
                {
                    levelObj = m_PanelData.allLevelItems[i];
                }

                levelObj.GetComponent<RectTransform>().SetParent(m_Content);
                levelObj.transform.SetParent(m_Content);
                levelObj.transform.ResetTrans();
                levelObj.GetComponent<LevelItem>().OnUIInit(this, m_PanelData.mainSeaLevelModel.GetBattleLevelData(i + 101));
                levelObj.GetComponent<Button>().OnClickAsObservable().Subscribe(_ => OnChooseLevel(levelObj.GetComponent<LevelItem>())).AddTo(this);
                levelObj.SetActive(true);
            }
            m_LevelSelectScrollView.SetLayoutVertical();
        }
        private void OnChooseLevel(LevelItem levelObj)
        {
            m_PanelData.currentLevelId.Value = levelObj.curLevelId;
        }

    }
}
