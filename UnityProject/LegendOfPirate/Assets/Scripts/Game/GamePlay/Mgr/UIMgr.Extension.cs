using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Qarth;
using System;

namespace Qarth
{
    /// <summary>
    /// 扩展UIMgr，增强对向导的支持
    /// </summary>
	public partial class UIMgr
	{
        /// <summary>
        /// 判读某个界面是否处于激活状态
        /// </summary>
        public bool IsPanelActive<T>(T uiID) where T : IConvertible
        {
            var panel = GetPanelFromActive(uiID.ToInt32(null));

            return panel != null;
        }

        public int GetTopPanelID()
        {
            int id = UIMgr.S.FindTopPanel<int>();

            return id;
        }
    }

}