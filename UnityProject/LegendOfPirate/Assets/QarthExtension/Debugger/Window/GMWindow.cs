using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameWish.Game
{
    public class GMWindow : IDebuggerWindow
    {
        /// <summary>
        /// 初始化调试器窗口。
        /// </summary>
        /// <param name="args">初始化调试器窗口参数。</param>
        public void Initialize(params object[] args) { }

        /// <summary>
        /// 关闭调试器窗口。
        /// </summary>
        public void Shutdown() { }

        /// <summary>
        /// 进入调试器窗口。
        /// </summary>
        public void OnEnter() { }

        /// <summary>
        /// 离开调试器窗口。
        /// </summary>
        public void OnLeave() { }

        /// <summary>
        /// 调试器窗口轮询。
        /// </summary>
        public void OnUpdate() { }

        /// <summary>
        /// 调试器窗口绘制。
        /// </summary>
        public void OnDraw()
        {
            GUILayout.BeginVertical();
            GUILayout.BeginHorizontal();
            if (GUILayout.Button("BattleInit", GUILayout.Width(100f)))
            {
                BattleMgr.S.BattleInit(BattleMgr.S.DemoEnemyFieldConfigSO);
            }

            if (GUILayout.Button("BattleStart", GUILayout.Width(100f)))
            {
                BattleMgr.S.BattleStart();
            }

            if (GUILayout.Button("BattleClean", GUILayout.Width(100f)))
            {
                BattleMgr.S.BattleEnd(true);
                BattleMgr.S.BattleClean();
            }

            if (GUILayout.Button("AddSpiritRole1001", GUILayout.Width(150f)))
            {
                RoleGroupModel roleGroupModel = ModelMgr.S.GetModel<RoleGroupModel>();
                roleGroupModel.AddSpiritRoleModel(1001, 100);
            }

            if (GUILayout.Button("AddSkillForRole1001", GUILayout.Width(170f)))
            {
                RoleModel roleModel = ModelMgr.S.GetModel<RoleGroupModel>().GetRoleModel(1001);
                roleModel.AddSkill(10011);
            }


            GUILayout.EndHorizontal();

            GUILayout.EndVertical();
        }
    }

}