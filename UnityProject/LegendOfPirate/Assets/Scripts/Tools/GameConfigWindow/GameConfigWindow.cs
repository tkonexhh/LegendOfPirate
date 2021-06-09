using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using System.Linq;
using Sirenix.Utilities;
using Sirenix.Utilities.Editor;
using Sirenix.OdinInspector.Editor;
using UnityEditor;

namespace GameWish.Game
{
    public class GameConfigWindow : OdinMenuEditorWindow
    {
        private const string SkillConfigSOPath = "Assets/Res/FolderMode/Skill";
        private const string BuffConfigSOPath = "Assets/Res/FolderMode/Buff";
        private const string RoleConfigSOPath = "Assets/Res/FolderMode/Character";
        private const string BattleFieldConfigSOPath = "Assets/Res/FolderMode/Config";

        private const string Name_Role = "角色";
        private const string Name_Skill = "技能";
        private const string Name_BattleConfig = "战场配置";
        private const string Name_Buff = "Buff";

        [MenuItem("Tools/Game/GameConfigWindow")]
        private static void OpenWindow()
        {
            var window = GetWindow<GameConfigWindow>();
            window.position = GUIHelper.GetEditorWindowRect().AlignCenter(800, 600);
        }

        protected override OdinMenuTree BuildMenuTree()
        {
            var tree = new OdinMenuTree(true);
            tree.DefaultMenuStyle.IconSize = 28.00f;
            tree.Config.DrawSearchToolbar = true;

            tree.AddAllAssetsAtPath(Name_Skill, SkillConfigSOPath, typeof(SkillConfigSO), true, true);
            tree.AddAllAssetsAtPath(Name_Buff, BuffConfigSOPath, typeof(BuffConfigSO), true, true);
            tree.AddAllAssetsAtPath(Name_Role, RoleConfigSOPath, typeof(RoleConfigSO), true, true);
            tree.AddAllAssetsAtPath(Name_BattleConfig, BattleFieldConfigSOPath, typeof(BattleFieldConfigSO), true, true);

            return tree;
        }

        protected override void OnBeginDrawEditors()
        {
            var selected = this.MenuTree.Selection.FirstOrDefault();
            var toolbarHeight = this.MenuTree.Config.SearchToolbarHeight;


            SirenixEditorGUI.BeginHorizontalToolbar(toolbarHeight);
            {
                if (selected != null)
                {
                    GUILayout.Label(selected.Name);

                    switch (selected.Name)
                    {
                        case Name_Role: CreateRole(); break;
                        case Name_Skill: CreateSkill(); break;
                        case Name_Buff: CreateBuff(); break;
                        case Name_BattleConfig: CreateBattleConfig(); break;
                    }
                }

            }

            SirenixEditorGUI.EndHorizontalToolbar();
        }


        private void CreateRole()
        {
            if (SirenixEditorGUI.ToolbarButton(new GUIContent("Create Role")))
            {
                ScriptableObjectCreator.ShowDialog<RoleConfigSO>(RoleConfigSOPath, obj =>
                {
                    obj.RoleName = obj.name;
                    base.TrySelectMenuItemWithObject(obj); // Selects the newly created item in the editor
                });
            }
        }

        private void CreateSkill()
        {
            if (SirenixEditorGUI.ToolbarButton(new GUIContent("Create Skill")))
            {
                ScriptableObjectCreator.ShowDialog<SkillConfigSO>(SkillConfigSOPath, obj =>
                {
                    obj.SkillName = obj.name;
                    base.TrySelectMenuItemWithObject(obj); // Selects the newly created item in the editor
                });
            }
        }

        private void CreateBattleConfig()
        {
            if (SirenixEditorGUI.ToolbarButton(new GUIContent("Create BattleConfig")))
            {
                ScriptableObjectCreator.ShowDialog<BattleFieldConfigSO>(BattleFieldConfigSOPath, obj =>
                {
                    base.TrySelectMenuItemWithObject(obj); // Selects the newly created item in the editor
                });
            }
        }

        private void CreateBuff()
        {
            if (SirenixEditorGUI.ToolbarButton(new GUIContent("Create Buff")))
            {
                ScriptableObjectCreator.ShowDialog<BuffConfigSO>(BuffConfigSOPath, obj =>
                {
                    obj.Name = obj.name;
                    base.TrySelectMenuItemWithObject(obj); // Selects the newly created item in the editor
                });
            }
        }
    }

}