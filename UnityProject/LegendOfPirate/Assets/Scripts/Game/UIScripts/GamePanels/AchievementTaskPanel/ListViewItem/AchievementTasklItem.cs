
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Qarth;

namespace GameWish.Game
{
    public partial class AchievementTasklItem : MonoBehaviour
    {
        enum TaskState
        {
            UnComplete,
            Complete,
            End,
        }
        private AchievementTaskPanel m_Panel;
        private TaskItemData m_TaskItemData;
        private TDAchievementTask m_Conf;
        private Image m_ImgBtn;
        private TaskData m_TaskData;
        private int[] m_TargetCountList;
        private string[] m_TargetRewardList;
        private int targetIndex;

        public void OnInit(AchievementTaskPanel panel, TDAchievementTask conf, TaskData taskData)
        {
            m_Panel = panel;
            m_Conf = conf;
            m_TaskData = taskData;
            m_TaskItemData = m_TaskData.GetTaskAchievementData().GetTaskItemData(m_Conf.achievementID);
            m_DescTex.text = string.Format(m_Conf.achievementDescription, m_Conf.taskCount);
            m_ImgBtn = m_GetBtn.GetComponent<GImage>();
            m_GetBtn.onClick.AddListener(() =>
            {
                OnClickBtnOk();
            });
            DealTarget();
            RefreshUI();
        }

        public void UpdateItem()
        {
            RefreshUI();
        }
        private void DealTarget()
        {
            string[] targetCount = m_Conf.taskCount.Split(';');
            string[] targetReward = m_Conf.reward.Split(';');
            m_TargetCountList = new int[targetCount.Length];
            m_TargetRewardList = new string[targetReward.Length];
            for (int i = 0; i < targetCount.Length; i++)
            {
                m_TargetCountList[i] = int.Parse(targetCount[i]);
                m_TargetRewardList[i] = targetReward[i];
            }
        }
        void RefreshUI()
        {
            m_TaskItemData.curAchievementTimes = m_TaskItemData.curAchievementTimes <= m_TargetCountList[m_TargetCountList.Length - 1] ? m_TaskItemData.curAchievementTimes : m_TargetCountList[m_TargetCountList.Length - 1];
            targetIndex = m_TaskItemData.curTargetIndex == m_TargetCountList.Length ? m_TaskItemData.curTargetIndex - 1 : m_TaskItemData.curTargetIndex;
            int target = m_TargetCountList[targetIndex];
            int cur = m_TaskItemData.curAchievementTimes;
            m_SliderTex.text = string.Format("{0}/{1}", cur, target);
            m_DescTex.text = string.Format(m_Conf.achievementDescription, target);
            m_SliderImg.fillAmount = (float)cur / (float)target;
            string imgName = "";
            switch (GetTaskState())
            {
                case TaskState.Complete:
                    imgName = "task_ann01";
                    m_TmpState.text = "Get";
                    m_GetBtn.enabled = true;
                    this.transform.SetAsFirstSibling();
                    break;
                case TaskState.UnComplete:
                    imgName = "task_ann04";
                    m_TmpState.text = "Unfinished";
                    m_GetBtn.enabled = false;
                    break;
                default:
                    imgName = "task_ann03";
                    m_TmpState.text = "Finished";
                    m_GetBtn.enabled = false;
                    this.transform.SetAsLastSibling();
                    break;
            }
            CreatRewardItem();
            // SpriteAtlas atlas = GameplayMgr.S.ResLoader.LoadSync("task_sprite_atlas") as SpriteAtlas;
            // m_ImgBtn.sprite = atlas.GetSprite(imgName);
            // m_ImgBtn.SetNativeSize();
        }
        private void CreatRewardItem()
        {
            m_AchievementRewardContent.RemoveAllChild();
            string[] rewardList = m_TargetRewardList[targetIndex].Split('&');
            for (int i = 0; i < rewardList.Length; i++)
            {
                GameObject item = Instantiate(m_AchievementRewardItem);
                item.transform.SetParent(m_AchievementRewardContent);
                item.transform.ResetTrans();
                item.gameObject.SetActive(true);
                item.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = rewardList[i].Split('|')[2];
            }
        }
        private void OnClickBtnOk()
        {
            TaskState taskState = GetTaskState();
            switch (taskState)
            {
                case TaskState.UnComplete:
                    GoComplete();
                    break;
                case TaskState.End:
                    FloatMessageTMP.S.ShowMsg("The task has been completed today～～～");
                    break;
                case TaskState.Complete:
                    // DataAnalysisMgr.S.CustomEvent(DefineTaskDot.Task_get_daily_reward, type);
                    Debug.Log("m_Conf.achievementID==" + m_Conf.achievementID + "has finish");
                    m_TaskData.GetTaskAchievementData().SetTaskComplete(m_Conf.achievementID);
                    break;
            }
        }

        TaskState GetTaskState()
        {
            TaskState taskState = TaskState.Complete;
            int achievementTimes = m_TaskItemData.curAchievementTimes;
            if (m_TaskItemData.isGetRewardToday && m_TaskItemData.curTargetIndex == m_TargetCountList.Length)
            {
                taskState = TaskState.End;
            }
            else if (achievementTimes >= m_TargetCountList[m_TaskItemData.curTargetIndex])
            {
                taskState = TaskState.Complete;
            }
            else
            {
                taskState = TaskState.UnComplete;
            }
            return taskState;
        }

        void GoComplete()
        {
            m_Panel.CloseSelfPanel();
        }
    }
}