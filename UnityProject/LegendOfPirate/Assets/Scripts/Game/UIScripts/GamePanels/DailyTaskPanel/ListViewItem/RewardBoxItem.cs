
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using Qarth;

namespace GameWish.Game
{
    public partial class RewardBoxItem : MonoBehaviour
    {
        enum ActiveRewardState
        {
            //不可打开
            Lock,
            //可打卡
            CanOpen,
            //已打开 领取完成
            Opened,
        }
        private DailyTaskPanel m_Panel;
        private TDDailyTaskReward m_Conf;
        private TaskData m_TaskData;
        private int m_CurActive;
        private int m_Index;
        public void OnInit(DailyTaskPanel panel, TDDailyTaskReward conf, TaskData taskData, int index)
        {
            m_Panel = panel;
            m_Conf = conf;
            m_TaskData = taskData;
            m_Index = index;
            m_TmpActive.text = m_Conf.activity.ToString();
            m_CurActive = m_Conf.activity;
            m_TmpReward.text = m_Conf.reward;
            m_RewardShow.SetActive(false);
            this.GetComponent<Button>().onClick.AddListener(() =>
            {
                OnClickBtnReward();
            });
            m_BtnRewardMask.onClick.AddListener(() =>
            {
                BtnsControl(false);
            });
        }
        void RefreshUI()
        {
            string imgName = "task_ann01";
            switch (GetTaskState())
            {
                case ActiveRewardState.Lock:
                    imgName = "task_ann01";
                    break;
                case ActiveRewardState.CanOpen:
                    imgName = "task_ann04";
                    break;
                case ActiveRewardState.Opened:
                    imgName = "task_ann04";
                    break;
            }
            // SpriteAtlas atlas = GameplayMgr.S.ResLoader.LoadSync("task_sprite_atlas") as SpriteAtlas;
            // m_ImgBtn.sprite = atlas.GetSprite(imgName);
            // m_ImgBtn.SetNativeSize();
        }
        private ActiveRewardState GetTaskState()
        {
            ActiveRewardState rewardState = ActiveRewardState.Lock;
            if (m_TaskData.GetTaskDailyData().activeNum < m_CurActive)
            {
                rewardState = ActiveRewardState.Lock;
            }
            else if (m_TaskData.GetTaskDailyData().activeNum >= m_CurActive)
            {
                if (m_TaskData.GetTaskDailyData().getRewardList[m_Index] == 1)
                {
                    rewardState = ActiveRewardState.Opened;
                }
                else
                {
                    rewardState = ActiveRewardState.CanOpen;
                }
            }
            return rewardState;
        }
        private void OnClickBtnReward()
        {
            ActiveRewardState rewardState = GetTaskState();
            switch (rewardState)
            {
                case ActiveRewardState.Lock:
                    Debug.Log("this reward is locked");
                    break;
                case ActiveRewardState.CanOpen:
                    Debug.Log("this reward can open");
                    BtnsControl(true);
                    break;
                case ActiveRewardState.Opened:
                    Debug.Log("this reward has opened");
                    m_TaskData.GetTaskDailyData().SetGetRewardState(m_Index, 1);
                    break;
            }
        }
        private void BtnsControl(bool flag)
        {
            m_BtnRewardMask.gameObject.SetActive(flag);
            m_RewardShow.SetActive(flag);
        }
    }
}