using System;
using System.Collections;
using System.Collections.Generic;
using GameWish.Game;
using Qarth;
using UnityEngine;

public class EventCommand : AbstractGuideCommand
{
    private string m_TypeName;
    private string m_EnumName;

    private List<int> m_EventList=new List<int>();

    //参数按照事件类型成对出现 或
    public override void SetParam(object[] param)
    {
        if (param.Length == 0 || param.Length%2 != 0)
        {
            Log.w("=========event command params erro=========");
            return;
        }

        for (int i = 0; i < param.Length; i=i+2)
        {
            m_TypeName = param[i] as string;
            m_EnumName = param[i+1] as string;
            Log.w("=============" + m_TypeName + "=====" + m_EnumName);
            try
            {
                Type enumType = Type.GetType(m_TypeName);
                int m_EventID = (int)Enum.Parse(enumType, m_EnumName);
                m_EventList.Add(m_EventID);
            }
            catch (Exception e)
            {
                Log.e(e);
            }
        }
    }

    protected override void OnStart()
    {
        for (int i = 0; i < m_EventList.Count; i++)
        {
            EventSystem.S.Register(m_EventList[i], HandleEvent);
        }
       
    }

    protected override void OnFinish(bool forceClean)
    {
        for (int i = 0; i < m_EventList.Count; i++)
        {
            EventSystem.S.UnRegister(m_EventList[i], HandleEvent);
        }
    }
    protected virtual void HandleEvent(int key, params object[] param)
    {
        Log.w("======EventCommandFinish====="+key);
        FinishStep();
    }
}
