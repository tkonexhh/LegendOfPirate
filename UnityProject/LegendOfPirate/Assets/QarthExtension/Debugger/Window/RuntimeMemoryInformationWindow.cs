﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Profiling;

namespace GameWish.Game
{
    public class RuntimeMemoryInformationWindow<T> : ScrollableDebuggerWindowBase where T : UnityEngine.Object
    {
        private const int ShowSampleCount = 300;

        private readonly List<Sample> m_Samples = new List<Sample>();
        private DateTime m_SampleTime = DateTime.MinValue;
        private long m_SampleSize = 0L;
        private long m_DuplicateSampleSize = 0L;
        private int m_DuplicateSimpleCount = 0;

        protected override void OnDrawScrollableWindow()
        {
            string typeName = typeof(T).Name;
            GUILayout.Label(Utility.Text.Format("<b>{0} Runtime Memory Information</b>", typeName));
            GUILayout.BeginVertical("box");
            {
                if (GUILayout.Button(Utility.Text.Format("Take Sample for {0}", typeName), GUILayout.Height(30f)))
                {
                    TakeSample();
                }

                if (m_SampleTime <= DateTime.MinValue)
                {
                    GUILayout.Label(Utility.Text.Format("<b>Please take sample for {0} first.</b>", typeName));
                }
                else
                {
                    if (m_DuplicateSimpleCount > 0)
                    {
                        GUILayout.Label(Utility.Text.Format("<b>{0} {1}s ({2}) obtained at {3}, while {4} {1}s ({5}) might be duplicated.</b>", m_Samples.Count.ToString(), typeName, GetByteLengthString(m_SampleSize), m_SampleTime.ToLocalTime().ToString("yyyy-MM-dd HH:mm:ss"), m_DuplicateSimpleCount.ToString(), GetByteLengthString(m_DuplicateSampleSize)));
                    }
                    else
                    {
                        GUILayout.Label(Utility.Text.Format("<b>{0} {1}s ({2}) obtained at {3}.</b>", m_Samples.Count.ToString(), typeName, GetByteLengthString(m_SampleSize), m_SampleTime.ToLocalTime().ToString("yyyy-MM-dd HH:mm:ss")));
                    }

                    if (m_Samples.Count > 0)
                    {
                        GUILayout.BeginHorizontal();
                        {
                            GUILayout.Label(Utility.Text.Format("<b>{0} Name</b>", typeName));
                            GUILayout.Label("<b>Type</b>", GUILayout.Width(240f));
                            GUILayout.Label("<b>Size</b>", GUILayout.Width(80f));
                        }
                        GUILayout.EndHorizontal();
                    }

                    int count = 0;
                    for (int i = 0; i < m_Samples.Count; i++)
                    {
                        GUILayout.BeginHorizontal();
                        {
                            GUILayout.Label(m_Samples[i].Highlight ? Utility.Text.Format("<color=yellow>{0}</color>", m_Samples[i].Name) : m_Samples[i].Name);
                            GUILayout.Label(m_Samples[i].Highlight ? Utility.Text.Format("<color=yellow>{0}</color>", m_Samples[i].Type) : m_Samples[i].Type, GUILayout.Width(240f));
                            GUILayout.Label(m_Samples[i].Highlight ? Utility.Text.Format("<color=yellow>{0}</color>", GetByteLengthString(m_Samples[i].Size)) : GetByteLengthString(m_Samples[i].Size), GUILayout.Width(80f));
                        }
                        GUILayout.EndHorizontal();

                        count++;
                        if (count >= ShowSampleCount)
                        {
                            break;
                        }
                    }
                }
            }
            GUILayout.EndVertical();
        }

        private void TakeSample()
        {
            m_SampleTime = DateTime.UtcNow;
            m_SampleSize = 0L;
            m_DuplicateSampleSize = 0L;
            m_DuplicateSimpleCount = 0;
            m_Samples.Clear();

            T[] samples = Resources.FindObjectsOfTypeAll<T>();
            for (int i = 0; i < samples.Length; i++)
            {
                long sampleSize = 0L;
#if UNITY_5_6_OR_NEWER
                sampleSize = Profiler.GetRuntimeMemorySizeLong(samples[i]);
#else
                    sampleSize = Profiler.GetRuntimeMemorySize(samples[i]);
#endif
                m_SampleSize += sampleSize;
                m_Samples.Add(new Sample(samples[i].name, samples[i].GetType().Name, sampleSize));
            }

            m_Samples.Sort(SampleComparer);

            for (int i = 1; i < m_Samples.Count; i++)
            {
                if (m_Samples[i].Name == m_Samples[i - 1].Name && m_Samples[i].Type == m_Samples[i - 1].Type && m_Samples[i].Size == m_Samples[i - 1].Size)
                {
                    m_Samples[i].Highlight = true;
                    m_DuplicateSampleSize += m_Samples[i].Size;
                    m_DuplicateSimpleCount++;
                }
            }
        }

        private int SampleComparer(Sample a, Sample b)
        {
            int result = b.Size.CompareTo(a.Size);
            if (result != 0)
            {
                return result;
            }

            result = a.Type.CompareTo(b.Type);
            if (result != 0)
            {
                return result;
            }

            return a.Name.CompareTo(b.Name);
        }


        private sealed class Sample
        {
            private readonly string m_Name;
            private readonly string m_Type;
            private readonly long m_Size;
            private bool m_Highlight;

            public Sample(string name, string type, long size)
            {
                m_Name = name;
                m_Type = type;
                m_Size = size;
                m_Highlight = false;
            }

            public string Name
            {
                get
                {
                    return m_Name;
                }
            }

            public string Type
            {
                get
                {
                    return m_Type;
                }
            }

            public long Size
            {
                get
                {
                    return m_Size;
                }
            }

            public bool Highlight
            {
                get
                {
                    return m_Highlight;
                }
                set
                {
                    m_Highlight = value;
                }
            }
        }
    }

}