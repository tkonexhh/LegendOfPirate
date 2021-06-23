using System.Collections.Generic;
using Qarth.Extension;
using UnityEngine.UI;
using Qarth;
using UniRx;

namespace GameWish.Game
{
    public class DailyTaskPanelData : UIPanelData
    {
        public DailyTaskModel dailyTaskModel;
        public DailyTaskAwardModel dailyTaskAwardModel;
        public IntReactiveProperty curActiveNum;
        public DailyTaskPanelData()
        {
        }
    }

    public partial class DailyTaskPanel
    {
        private DailyTaskPanelData m_PanelData = null;

        private void AllocatePanelData()
        {
            m_PanelData = UIPanelData.Allocate<DailyTaskPanelData>();
            m_PanelData.dailyTaskModel = new DailyTaskModel();
            m_PanelData.dailyTaskAwardModel = new DailyTaskAwardModel();
            CreatRwardBoxItem();
            CreatTaskItem();
        }

        private void ReleasePanelData()
        {
            ObjectPool<DailyTaskPanelData>.S.Recycle(m_PanelData);
        }

        private void BindModelToUI()
        {
            m_PanelData.curActiveNum = new IntReactiveProperty(m_PanelData.dailyTaskModel.GetActiveNum());
            m_PanelData.curActiveNum.AsObservable().Subscribe(count =>
            {
                SetSliderState(count);
            }).AddTo(this);
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
            SetSliderState(m_PanelData.curActiveNum.Value);
            UpdateItems(0, 0);
        }
        private void OnBackClicked()
        {
            CloseSelfPanel();
            UIMgr.S.OpenPanel(UIID.MainMenuPanel);
        }

        private void SetSliderState(int acitveNum)
        {
            m_Slider.fillAmount = (float)acitveNum / (float)m_PanelData.dailyTaskAwardModel.targetActive;
        }
        private void RegisterEvents()
        {
            EventSystem.S.Register(EventID.DailyTaskRefresh, UpdateItems);
        }
        private void UnregisterEvents()
        {
            EventSystem.S.UnRegister(EventID.DailyTaskRefresh, UpdateItems);
        }
        private void CreatRwardBoxItem()
        {
            for (int i = 0; i < m_PanelData.dailyTaskAwardModel.GetDailyTaskCount(); i++)
            {
                RewardBoxItem item = null;
                if (m_PanelData.dailyTaskAwardModel.rewardBoxItems == null || m_PanelData.dailyTaskAwardModel.rewardBoxItems.Count < m_PanelData.dailyTaskAwardModel.GetDailyTaskCount())
                {
                    item = Instantiate(m_RewardBoxItem.gameObject).GetComponent<RewardBoxItem>();
                    m_PanelData.dailyTaskAwardModel.rewardBoxItems.Add(item);
                }
                else
                {
                    item = m_PanelData.dailyTaskAwardModel.rewardBoxItems[i];
                }
                item.transform.SetParent(m_RewardContent);
                item.transform.ResetTrans();
                item.gameObject.SetActive(true);
                item.OnInit(this, m_PanelData.dailyTaskAwardModel.tdDailyTaskAwardList[i], m_PanelData.dailyTaskModel.taskData, i);
            }
        }

        private void CreatTaskItem()
        {
            for (int i = 0; i < m_PanelData.dailyTaskModel.GetDailyTaskCount(); i++)
            {
                DailyTasklItem item = null;
                if (m_PanelData.dailyTaskModel.dailyItems == null || m_PanelData.dailyTaskModel.dailyItems.Count < m_PanelData.dailyTaskModel.GetDailyTaskCount())
                {
                    item = Instantiate(m_DailyTasklItem.gameObject).GetComponent<DailyTasklItem>();
                    m_PanelData.dailyTaskModel.dailyItems.Add(item);
                }
                else
                {
                    item = m_PanelData.dailyTaskModel.dailyItems[i];
                }
                item.transform.SetParent(m_DailyTaskContent);
                item.transform.ResetTrans();
                item.gameObject.SetActive(true);
                item.OnInit(this, m_PanelData.dailyTaskModel.tdDailyTaskList[i], m_PanelData.dailyTaskModel.taskData);
            }
            m_DailyItemScrollView.SetLayoutVertical();
        }

        private void UpdateItems(int key, params object[] args)
        {
            m_PanelData.curActiveNum.Value = m_PanelData.dailyTaskModel.GetActiveNum();
            for (int i = 0; i < m_PanelData.dailyTaskModel.dailyItems.Count; i++)
            {
                m_PanelData.dailyTaskModel.dailyItems[i].UpdateItem();
            }
        }
    }
}
