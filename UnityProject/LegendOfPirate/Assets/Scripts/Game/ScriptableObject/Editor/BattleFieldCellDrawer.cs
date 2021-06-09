using UnityEngine;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using Sirenix.OdinInspector.Editor.Drawers;
using Sirenix.Utilities.Editor;
using Sirenix.Utilities;
using UnityEditor;

namespace GameWish.Game
{
    internal sealed class BattleFieldCellDrawer<TArray> : TwoDimensionalArrayDrawer<TArray, BattleFieldCell> where TArray : System.Collections.IList
    {

        protected override TableMatrixAttribute GetDefaultTableMatrixAttributeSettings()
        {
            return new TableMatrixAttribute()
            {
                SquareCells = true,
                // HideColumnIndices = true,
                // HideRowIndices = true,
                // ResizableColumns = false
            };
        }


        protected override BattleFieldCell DrawElement(Rect rect, BattleFieldCell value)
        {
            var id = DragAndDropUtilities.GetDragAndDropId(rect);
            DragAndDropUtilities.DrawDropZone(rect, value, null, id); // Draws the drop-zone using the items icon.
            // Debug.LogError(value);
            if (value != null)
            {
                // Item count
                var countRect = rect.Padding(2).AlignBottom(16);
                // value.ItemCount = EditorGUI.IntField(countRect, Mathf.Max(1, value.ItemCount));
                // GUI.Label(countRect, "/ " + value.Item.ItemStackSize, SirenixGUIStyles.RightAlignedGreyMiniLabel);
            }

            value = DragAndDropUtilities.DropZone(rect, value);                                     // Drop zone for ItemSlot structs.
            // value.roleConfig = DragAndDropUtilities.DropZone<RoleConfigSO>(rect, value.roleConfig);                     // Drop zone for Item types.
            value = DragAndDropUtilities.DragZone(rect, value, true, true);                         // Enables dragging of the ItemSlot
            return value;
        }

        protected override void DrawPropertyLayout(GUIContent label)
        {
            base.DrawPropertyLayout(label);

            // Draws a drop-zone where we can destroy items.
            var rect = GUILayoutUtility.GetRect(0, 40).Padding(2);
            var id = DragAndDropUtilities.GetDragAndDropId(rect);
            DragAndDropUtilities.DrawDropZone(rect, null as UnityEngine.Object, null, id);
            DragAndDropUtilities.DropZone<BattleFieldCell>(rect, new BattleFieldCell(), false, id);
        }
    }
}