
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using Qarth;

namespace GameWish.Game
{
    public partial class DailyTasklItem : MonoBehaviour
    {
        enum TaskState
        {
            Complete,
            UnComplete,
            End,
        }
        private DailyTaskPanel m_Panel;
        private TaskItemData m_TaskItemData;
        private TDDailyTask m_Conf;
        private Image m_ImgBtn;
        private TaskData m_TaskData;
        private int m_ActiveNum;
        private int m_PlayerLevel;
        public void OnInit(DailyTaskPanel panel, TDDailyTask conf, TaskData taskData)
        {
            m_Panel = panel;
            m_Conf = conf;
            m_TaskData = taskData;
            m_TaskItemData = m_TaskData.GetTaskDailyData().GetTaskItemData(m_Conf.taskID);
            m_ImgBtn = m_BtnGet.transform.GetComponent<GImage>();
            m_TmpTitle.text = string.Format(m_Conf.taskDescription, m_Conf.taskCount);
            m_TmpActiveCount.text = string.Format("{0}", m_Conf.reward);
            m_PlayerLevel = conf.playerLevel;
            m_ActiveNum = int.Parse(m_Conf.reward.Split('|')[2]);
            m_BtnGet.onClick.AddListener(() =>
            {
                OnClickBtnOk();
            });
            RefreshUI();
        }

        public void UpdateItem()
        {
            RefreshUI();
        }

        void RefreshUI()
        {
            int target = m_Conf.taskCount;
            int cur = m_TaskItemData.curCompleteTimes > target ? target : m_TaskItemData.curCompleteTimes;
            m_TmpProgress.text = string.Format("{0}/{1}", cur, target);
            m_Slider.fillAmount = (float)cur / (float)target;
            string imgName = "";
            switch (GetTaskState())
            {
                case TaskState.Complete:
                    imgName = "task_ann01";
                    m_TmpState.text = "Receive";
                    m_BtnGet.enabled = true;
                    this.transform.SetAsFirstSibling();
                    break;
                case TaskState.UnComplete:
                    imgName = "task_ann04";
                    m_TmpState.text = "Unfinished";
                    m_BtnGet.enabled = false;
                    break;
                default:
                    imgName = "task_ann03";
                    m_TmpState.text = "Already Received";
                    m_BtnGet.enabled = false;
                    this.transform.SetAsLastSibling();
                    break;
            }
            //TODO 添加人物等级判断
            if (m_PlayerLevel >= 5)
            {
                SetBtnsState(false);
            }
            else
            {
                SetBtnsState(true);
            }
            // SpriteAtlas atlas = GameplayMgr.S.ResLoader.LoadSync("task_sprite_atlas") as SpriteAtlas;
            // m_ImgBtn.sprite = atlas.GetSprite(imgName);
            // m_ImgBtn.SetNativeSize();
        }

        private void SetBtnsState(bool flag)
        {
            m_TmpLimit.gameObject.SetActive(!flag);
            m_ItemBg.gameObject.SetActive(flag);
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
                    FloatMessage.S.ShowMsg("The task has been completed today～～～");
                    break;
                case TaskState.Complete:
                    // DataAnalysisMgr.S.CustomEvent(DefineTaskDot.Task_get_daily_reward, type);
                    Debug.Log("m_Conf.taskID==" + m_Conf.taskID + "has finish");
                    m_TaskData.GetTaskDailyData().AddActiveNum(m_ActiveNum);
                    m_TaskData.GetTaskDailyData().SetTaskComplete(m_Conf.taskID);
                    break;
            }
        }

        TaskState GetTaskState()
        {
            TaskState taskState = TaskState.Complete;
            if (m_TaskItemData.isGetRewardToday)
            {
                taskState = TaskState.End;
            }
            else if (m_TaskItemData.curCompleteTimes >= m_Conf.taskCount)
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