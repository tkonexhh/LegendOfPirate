using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayCheckHelper
{
    private string m_LastSignDayKey = "lastPlayDay";

    public bool newDay;

    public DayCheckHelper()
    {

    }

    private void Save()
    {
        PlayerPrefs.SetString(m_LastSignDayKey, DateTime.Today.ToShortDateString());
    }

    public void Check()
    {
        Load();
        Save();
    }

    private void Load()
    {
        string day = PlayerPrefs.GetString(m_LastSignDayKey, "");
        DateTime lastSignDate;
        if (!string.IsNullOrEmpty(day))
        {
            if (DateTime.TryParse(day, out lastSignDate))
            {
                DateTime today = DateTime.Today;
                TimeSpan pass = today - lastSignDate;

                if (pass.Days < 1)
                {
                    newDay = false;
                }
                else
                {
                    newDay = true;
                }

            }
            else
            {
                newDay = true;
            }
        }
        else
        {
            newDay = true;
        }
    }
}
