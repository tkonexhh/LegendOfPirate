using System;
using System.Collections;
using System.Collections.Generic;
using GameWish.Game;
using Qarth;
using UnityEngine;

public class GameDateCountTimer
{
    private int m_IntervalSecs;//计时间隔
    private int m_Count;//计时次数
    private int m_CurrentCount;
    private DateTime m_DeadLineTime;
    private int m_TimerId = -1;
    private string m_CurCountCachStr = "curcount";
    private string m_SaveTimeCachStr = "savetime";

    public Action<int> ticktockAction;//参数：秒
    public Action timerReachAction;

    public GameDateCountTimer(int count, int interval)
    {
        this.m_Count = count;
        this.m_IntervalSecs = interval;

        if (PlayerPrefs.GetString(m_SaveTimeCachStr, "") == "")
        {
            m_DeadLineTime = DateTime.Now.AddSeconds(m_IntervalSecs);
            PlayerPrefs.SetString(m_SaveTimeCachStr, DateTime.Now.AddSeconds(m_IntervalSecs).ToString());
        }
        else
        {
            m_DeadLineTime = DateTime.Parse(PlayerPrefs.GetString(m_SaveTimeCachStr));
        }

        m_CurrentCount = PlayerPrefs.GetInt(m_CurCountCachStr, 0);
    }

    public int CurrentCount
    {
        get { return m_CurrentCount; }
    }

    public void StartTimer()
    {
        double secs = (m_DeadLineTime - DateTime.Now).TotalSeconds;
        if (secs > 0)
        {
            StartCountDown();
        }
        else
        {
            OnTimerReach();
        }
    }
    public void MinusCount(int count)
    {
        m_CurrentCount -= count;
        m_CurrentCount = Mathf.Max(0, m_CurrentCount);
        PlayerPrefs.SetInt(m_CurCountCachStr, m_CurrentCount);

        if (m_DeadLineTime <= DateTime.Now && m_TimerId == -1)

            SetNextDeadLineTime(0);
    }

    public void OnDisable()
    {
        Timer.S.Cancel(m_TimerId);
    }

    private void StartCountDown()
    {
        m_TimerId = Timer.S.Post2Really((i) =>
        {
            //如果线程暂停，回来后超出的时间要算
            ticktockAction(Mathf.RoundToInt((float)(m_DeadLineTime - DateTime.Now).TotalSeconds));
            if (m_DeadLineTime <= DateTime.Now)
            {
                OnTimerReach();
            }
        }, 1, -1);
    }

    private void OnTimerReach()
    {
        Timer.S.Cancel(m_TimerId);
        m_TimerId = -1;
        m_CurrentCount++;

        CheckOfflineTime();

        if (timerReachAction != null)
        {
            timerReachAction();
        }
    }

    private void CheckOfflineTime()
    {
        double secs = (m_DeadLineTime - DateTime.Now).TotalSeconds;
        int add = Mathf.FloorToInt((float)-secs / m_IntervalSecs);
        m_CurrentCount += add;
        m_CurrentCount = Mathf.Min(m_CurrentCount, m_Count);
        PlayerPrefs.SetInt(m_CurCountCachStr, m_CurrentCount);

        if (m_CurrentCount < m_Count)
        {
            SetNextDeadLineTime(secs + add * m_IntervalSecs);
        }
    }

    private void SetNextDeadLineTime(double delta)
    {
        m_DeadLineTime = DateTime.Now.AddSeconds(m_IntervalSecs + delta);
        PlayerPrefs.SetString(m_SaveTimeCachStr, m_DeadLineTime.ToString());
        StartCountDown();
    }
}


public class CustomTimer
{
    private float m_TotalSeconds;//计时秒数
    private float m_CurrentSeconds;
    private int m_TimerId = -1;
    private float m_Tick;

    public Action timerStartAction;
    public Action<float> timerTickAction;
    public Action timerReachAction;

    public float CurrentSeconds
    {
        get
        {
            return m_CurrentSeconds;
        }
    }

    public float TotalSeconds
    {
        get
        {
            return m_TotalSeconds;
        }
    }

    public CustomTimer(float toltalSecs, float tick)
    {
        m_TotalSeconds = toltalSecs;        
        m_Tick = tick;
    }

    public void StartTimer()
    {
        m_CurrentSeconds = m_TotalSeconds;
        if (timerStartAction != null)
        {
            timerStartAction.Invoke();
        }
        m_TimerId = Timer.S.Post2Really((i) =>
        {
            m_CurrentSeconds -= m_Tick;
            if (m_CurrentSeconds <= 0)
            {
                OnTimerReached();
            }
            if (timerTickAction != null)
            {
                timerTickAction.Invoke(Mathf.RoundToInt((float)CurrentSeconds));
            }    
        }, m_Tick, -1);
    }

    //加快或缩短时间，修改总时间
    public void UpdateTimer(float delta,float total=0)
    {
        if (total > 0)
            m_TotalSeconds = total;
        m_CurrentSeconds += delta;

        //Debug.Log("update timer"+m_CurrentSeconds);

        m_CurrentSeconds = Mathf.Max(m_CurrentSeconds, 0);
        m_CurrentSeconds = Mathf.Min(m_CurrentSeconds, m_TotalSeconds);
        if (m_TimerId == -1)
        {
            StartTimer();
        }
    }

    private void OnTimerReached()
    {
        Timer.S.Cancel(m_TimerId);
        ResetTimer();

        Log.i("timer reached");

        if (timerReachAction != null)
        {
            timerReachAction.Invoke();
        }
    }

    public void ResetTimer()
    {
        m_TimerId = -1;
        m_CurrentSeconds = 0;
    }

    public void PauseTimer()
    {
        Timer.S.Cancel(m_TimerId);
        m_TimerId = -1;
    }

    public void ResumeTimer()
    {
        m_TimerId = Timer.S.Post2Really((i) =>
        {
            m_CurrentSeconds -= m_Tick;
            if (m_CurrentSeconds <= 0)
            {
                OnTimerReached();
            }
            if (timerTickAction != null)
            {
                timerTickAction.Invoke(Mathf.RoundToInt((float)CurrentSeconds));
            }    
        }, m_Tick, -1);
    }

    public void ForceStop()
    {
        Timer.S.Cancel(m_TimerId);
        ResetTimer();
    }

    public void OnDisable()
    {
        Timer.S.Cancel(m_TimerId);
    }
}
