using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Qarth;

namespace Qarth.Extension
{
    /// <summary>
    /// 每个UIbehaviour对应的Data
    /// </summary>
    public interface IUIData
    {
    }

    public class UIPanelData : IUIData
    {
        protected AbstractPanel mPanel;
    }

}