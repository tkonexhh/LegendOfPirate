using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Qarth;

namespace Qarth.Extension
{
    /// <summary>
    /// ÿ��UIbehaviour��Ӧ��Data
    /// </summary>
    public interface IUIData
    {
    }

    public class UIPanelData : IUIData
    {
        protected AbstractPanel mPanel;
    }

}