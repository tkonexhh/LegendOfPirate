using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameWish.Game
{
	public interface IDebuggerWindow
	{
        /// <summary>
        /// 初始化调试器窗口。
        /// </summary>
        /// <param name="args">初始化调试器窗口参数。</param>
        void Initialize(params object[] args);

        /// <summary>
        /// 关闭调试器窗口。
        /// </summary>
        void Shutdown();

        /// <summary>
        /// 进入调试器窗口。
        /// </summary>
        void OnEnter();

        /// <summary>
        /// 离开调试器窗口。
        /// </summary>
        void OnLeave();

        /// <summary>
        /// 调试器窗口轮询。
        /// </summary>
        void OnUpdate();

        /// <summary>
        /// 调试器窗口绘制。
        /// </summary>
        void OnDraw();
    }
	
}