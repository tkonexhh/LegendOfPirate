using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
// using Sirenix.Utilities;
// using Sirenix.Utilities.Editor;

namespace GameWish.Game
{
    [CreateAssetMenu(menuName = "Game/BattleFieldConfigSO", fileName = "new_BattleFieldConfigSO")]
    public class BattleFieldConfigSO : SerializedScriptableObject
    {
        [LabelText("战场ID")]
        public int ID;
        [LabelText("敌人等级")]
        public int Level;

        [BoxGroup("敌人阵容配置")]
        [TableMatrix(SquareCells = true, HideColumnIndices = true, HideRowIndices = true, ResizableColumns = false), OnValueChanged("OnEnemysChange")]//DrawElementMethod ="DrawBattleFieldCell"
        public RoleConfigSO[,] Enemys = new RoleConfigSO[BattleDefine.BATTLE_WIDTH, BattleDefine.BATTLE_HEIGHT];

        // static RoleConfigSO DrawBattleFieldCell(Rect rect, RoleConfigSO value)
        // {
        //     var id = DragAndDropUtilities.GetDragAndDropId(rect);
        //     // DragAndDropUtilities.DrawDropZone(rect, value ? value.Icon : null, null, id);
        //     Texture2D texture = null;
        //     if (value)
        //     {
        //         texture = GUIHelper.GetAssetThumbnail(value.Icon, typeof(Texture2D), true);
        //     }
        //     SirenixEditorFields.UnityPreviewObjectField(rect.Padding(2), value, texture, typeof(RoleConfigSO), false);
        //     SirenixEditorFields.UnityObjectField(rect, value, typeof(RoleConfigSO), false);
        //     // value = DragAndDropUtilities.DropZone(rect, value);                                     // Drop zone for ItemSlot structs.
        //     // value.Item = DragAndDropUtilities.DropZone<Item>(rect, value.Item);                     // Drop zone for Item types.
        //     // value = DragAndDropUtilities.DragZone(rect, value, true, true);
        //     return value;
        // }


        [BoxGroup("头像预览")]
        [ReadOnly]
        [TableMatrix(SquareCells = true, HideColumnIndices = true, HideRowIndices = true, ResizableColumns = false)]//
        public Sprite[,] EnemyIcons = new Sprite[BattleDefine.BATTLE_WIDTH, BattleDefine.BATTLE_HEIGHT];

        private void OnEnemysChange()
        {
            for (int x = 0; x < EnemyIcons.GetLength(0); x++)
                for (int y = 0; y < EnemyIcons.GetLength(1); y++)
                {
                    if (Enemys[x, y] != null)
                        EnemyIcons[x, y] = Enemys[x, y].Icon;
                    else
                        EnemyIcons[x, y] = null;
                }
        }


        [Button("Save")]
        public void Save()
        {
#if UNITY_EDIROT
            UnityEditor.AssetDatabase.SaveAssets();
#endif
        }

    }

    [SerializeField]
    public class BattleFieldCell
    {
        public RoleConfigSO roleConfig;
    }

}