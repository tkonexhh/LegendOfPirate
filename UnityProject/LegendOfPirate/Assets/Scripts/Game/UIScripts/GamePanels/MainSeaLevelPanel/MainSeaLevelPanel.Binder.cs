using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using Qarth.Extension;
using Qarth;
using UniRx;
using System;

namespace GameWish.Game
{
    public class MainSeaLevelPanelData : UIPanelData
    {
        public MainSeaLevelModel mainSeaLevelModel;
        public IntReactiveProperty currentLevelId;
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
            //test tmp
            m_PanelData.currentLevelId = new IntReactiveProperty(101);
            m_PanelData.currentLevelId.AsObservable().SubscribeToTextMeshPro(m_CurrentLevel, "LEVEL {0}").AddTo(this);

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
            for (int i = 0; i < m_PanelData.mainSeaLevelModel.GetBattleLevelCount(); i++)
            {
                GameObject levelObj = null;
                if (m_PanelData.mainSeaLevelModel.allLevelItems == null || m_PanelData.mainSeaLevelModel.allLevelItems.Count < m_PanelData.mainSeaLevelModel.GetBattleLevelCount())
                {
                    levelObj = Instantiate(m_LevelItem.gameObject);
                    levelObj.transform.SetParent(m_Content);
                    levelObj.transform.ResetTrans();
                    levelObj.transform.localEulerAngles = new Vector3(0, 0, 180);
                    levelObj.GetComponent<Button>().OnClickAsObservable().Subscribe(_ => OnChooseLevel(levelObj.GetComponent<LevelItem>())).AddTo(this);
                    m_PanelData.mainSeaLevelModel.allLevelItems.Add(levelObj);
                }
                else
                {
                    levelObj = m_PanelData.mainSeaLevelModel.allLevelItems[i];
                }
                levelObj.GetComponent<LevelItem>().OnUIInit(this, m_PanelData.mainSeaLevelModel.GetBattleLevelData(i + 101));
                levelObj.SetActive(true);
            }
            m_LevelSelectScrollView.SetLayoutVertical();
            m_Content.SetLocalY(150);
        }
        private void OnChooseLevel(LevelItem levelObj)
        {
            m_PanelData.currentLevelId.Value = levelObj.curLevelId;
            UIMgr.S.OpenPanel(UIID.LevelPeviewPanel, null, m_PanelData.currentLevelId.Value);
        }

    }
}
