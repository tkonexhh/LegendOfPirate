using UnityEngine;
using UnityEngine.UI;
using Qarth.Extension;
using Qarth;
using UniRx;

namespace GameWish.Game
{
    public class AchievementTaskPanelData : UIPanelData
    {
        public AchievementTaskModel achievementTaskModel;
        public AchievementTaskPanelData()
        {
        }
    }

    public partial class AchievementTaskPanel
    {
        private AchievementTaskPanelData m_PanelData = null;

        private void AllocatePanelData()
        {
            m_PanelData = UIPanelData.Allocate<AchievementTaskPanelData>();
            m_PanelData.achievementTaskModel = new AchievementTaskModel();
            CreatTaskItem();
        }

        private void ReleasePanelData()
        {
            ObjectPool<AchievementTaskPanelData>.S.Recycle(m_PanelData);
        }

        private void BindModelToUI()
        {
        }

        private void BindUIToModel()
        {
        }
        private void OnClickAddListener()
        {
            m_BackBtn.OnClickAsObservable().Subscribe(_ => OnBackClicked()).AddTo(this);
        }

        private void RefreshPanelState()
        {
            UpdateItems(0, 0);
        }
        private void OnBackClicked()
        {
            CloseSelfPanel();
            UIMgr.S.OpenPanel(UIID.MainMenuPanel);
        }

        private void RegisterEvents()
        {
            EventSystem.S.Register(EventID.AchievementTaskRefresh, UpdateItems);
        }
        private void UnregisterEvents()
        {
            EventSystem.S.UnRegister(EventID.AchievementTaskRefresh, UpdateItems);
        }
        private void CreatTaskItem()
        {
            for (int i = 0; i < m_PanelData.achievementTaskModel.GetAchievementTaskCount(); i++)
            {
                AchievementTasklItem item = null;
                if (m_PanelData.achievementTaskModel.achievementItems == null || m_PanelData.achievementTaskModel.achievementItems.Count < m_PanelData.achievementTaskModel.GetAchievementTaskCount())
                {
                    item = Instantiate(m_AchievementTaskItem.gameObject).GetComponent<AchievementTasklItem>();
                    item.transform.SetParent(m_AchievementTaskContent);
                    item.transform.ResetTrans();
                    m_PanelData.achievementTaskModel.achievementItems.Add(item);
                }
                else
                {
                    item = m_PanelData.achievementTaskModel.achievementItems[i];
                }
                item.gameObject.SetActive(true);
                item.OnInit(this, m_PanelData.achievementTaskModel.tdAchievementTaskList[i], m_PanelData.achievementTaskModel.taskData);
            }
            m_AchievementItemScrollView.SetLayoutVertical();
        }

        private void UpdateItems(int key, params object[] args)
        {
            for (int i = 0; i < m_PanelData.achievementTaskModel.achievementItems.Count; i++)
            {
                m_PanelData.achievementTaskModel.achievementItems[i].UpdateItem();
            }
        }
    }
}
