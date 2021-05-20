using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
using Qarth;
using System;

namespace GameWish.Game
{
	public class TaskItem
	{
        private int m_JobId;
        private Type m_ClassType = null;
        private Action<TaskItem> m_StateChangedCallback = null;

        private bool m_IsFinished = false;
        private bool m_IsEventUnregistered = false;

        private List<IConditionChecker> m_ConditionCheckerList = new List<IConditionChecker>();
        private List<IRewardHandler> m_RewardHandlerList = new List<IRewardHandler>();
        private List<EventID> m_EventList = new List<EventID>();

        public int JobId { get => m_JobId; }

        public TaskItem(int id, string tableName, Action<TaskItem> stateChangedCallback)
        {
            m_JobId = id;
            m_ClassType = Type.GetType(Define.NAME_SPACE_PREFIX + tableName);
            m_StateChangedCallback = stateChangedCallback;

            if (m_ClassType != null)
            {
                ParseTaskConditions();
                ParseTaskEvents();
                ParseTaskRewards();

                RegisterEvent();
            }
            else
            {
                Log.e("TaskTableType is null");
            }
        }

        //~TaskItem()
        //{
        //    UnregisterEvent();
        //}

        public void ClaimReward()
        {
            foreach (IRewardHandler rewardHandler in m_RewardHandlerList)
            {
                rewardHandler.OnRewardClaimed();
            }
        }

        public int GetId()
        {
            return m_JobId;
        }

        public bool IsFinished()
        {
            return m_IsFinished;
        }

        public string GetCurValue()
        {
            return m_ConditionCheckerList[0].GetCurrentValue();
        }

        public string GetTargetValue()
        {
            return m_ConditionCheckerList[0].GetTargetValue();
        }

        public float GetProgressPercent()
        {
            return m_ConditionCheckerList[0].GetProgressPercent();
        }

        public void Release()
        {
            UnregisterEvent();
        }

        private void RegisterEvent()
        {
            foreach (EventID eventID in m_EventList)
            {
                EventSystem.S.Register(eventID, HandleEvent);
            }
        }

        private void UnregisterEvent()
        {
            if (m_IsEventUnregistered == false)
            {
                foreach (EventID eventID in m_EventList)
                {
                    EventSystem.S.UnRegister(eventID, HandleEvent);
                }

                m_IsEventUnregistered = true;
            }
        }

        private void HandleEvent(int key, params object[] param)
        {
            //m_IsDirty = true;
            if (m_IsFinished == false)
            {
                m_IsFinished = CheckFinish();
                Log.i("Task:" + m_JobId + " is finished: " + m_IsFinished);

                if (m_IsFinished)
                {
                    UnregisterEvent();
                }

                m_StateChangedCallback.Invoke(this);
            }
        }

        private bool CheckFinish()
        {
            //if (m_IsDirty)
            //{
            foreach (IConditionChecker taskCondition in m_ConditionCheckerList)
            {
                if (taskCondition.IsFinished() == false)
                {
                    //m_IsDirty = false;

                    return false;
                }
            }
            //}
            //else
            //{
            //    return false;
            //}

            return true;
        }

        private void ParseTaskConditions()
        {
            try
            {
                string conditionTypeStr = GetTableValue(TaskDefine.GET_CONDITION_TYPE);
                string[] conditionTypes = conditionTypeStr.Split('|');

                string conditionValueStr = GetTableValue(TaskDefine.GET_CONDITION_VALUE);
                string[] values = conditionValueStr.Split('|');

                for (int i =0; i < conditionTypes.Length; i++)
                {
                    string checkerClassName = Define.NAME_SPACE_PREFIX + conditionTypes[i] + "Checker";
                    string valueStr = values[i];

                    Type type = Type.GetType(checkerClassName);
                    if (type == null)
                    {
                        Log.e("ParseTaskConditions but class not found: " + checkerClassName);
                        continue;
                    }

                    object obj = Activator.CreateInstance(type, true);
                    MethodInfo methodInfo = type.GetMethod("Init");
                    object[] param = BuildParams(valueStr);
                    methodInfo.Invoke(obj, param);
                    m_ConditionCheckerList.Add((IConditionChecker)obj);  
                }
            }
            catch (Exception e)
            {
                Log.e(e);
            }
        }

        private void ParseTaskEvents()
        {
            try
            {
                string eventStr = GetTableValue(TaskDefine.GET_EVENT);
                string[] events = eventStr.Split('|');

                for (int i = 0; i < events.Length; i++)
                {
                    EventID eventId = StringHelper.ParseStringToEnum<EventID>(events[i]);
                    m_EventList.Add(eventId);
                }
            }
            catch (Exception e)
            {
               Log.e(e);
            }
        }

        private void ParseTaskRewards()
        {
            try
            {
                string rewardTypeStr = GetTableValue(TaskDefine.GET_REWARD_TYPE);
                string[] rewardTypes = rewardTypeStr.Split('|');

                string rewardValueStr = GetTableValue(TaskDefine.GET_REWARD_VALUE);
                string[] values = rewardValueStr.Split('|');

                for (int i = 0; i < rewardTypes.Length; i++)
                {
                    string rewardClassName = Define.NAME_SPACE_PREFIX + rewardTypes[i] + "RewardHandler";
                    string valueStr = values[i];

                    Type type = Type.GetType(rewardClassName);
                    if (type == null)
                    {
                        Log.e("ParseRewards but class not found: " + rewardClassName);
                        continue;
                    }

                    object obj = Activator.CreateInstance(type, true);
                    MethodInfo methodInfo = type.GetMethod("Init");
                    object[] param = BuildParams(valueStr);
                    methodInfo.Invoke(obj, param);
                    m_RewardHandlerList.Add((IRewardHandler)obj);
                }
            }
            catch (Exception e)
            {
                Log.e(e);
            }
        }

        private string GetTableValue(string colName)
        {
            object[] param = new object[] { m_JobId };

            MethodInfo methodInfo = m_ClassType.GetMethod(colName);
            string value = (string)methodInfo.Invoke(null, param);
            return value;
        }

        private object[] BuildParams(string value)
        {
            object[] objs;

            if (value.Contains("_"))
            {
                objs = new object[] { value };
            }
            else if (value.Contains("E+"))
            {
                objs = new object[] { double.Parse(value) };
            }
            else if (value.Contains("."))
            {
                objs = new object[] { float.Parse(value) };
            }
            else
            {
                objs = new object[] { int.Parse(value) };
            }

            return objs;
        }
    }

}