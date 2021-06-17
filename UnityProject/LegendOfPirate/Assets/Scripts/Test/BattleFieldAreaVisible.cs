using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace GameWish.Game
{
    public class BattleFieldAreaVisible : MonoBehaviour
    {




        void Start()
        {

        }


        private void OnDrawGizmosSelected()
        {
            float height = 100;
            Vector3 center = gameObject.transform.position;
            // Handle
            float totalWidth = BattleDefine.BATTLE_CELL_WIDTH * BattleDefine.BATTLE_WIDTH;
            Gizmos.color = Color.black;
            Gizmos.DrawWireCube(center, new Vector3(totalWidth, height, BattleDefine.BATTLE_HALF_CENTERHEIGHT * 2));

            Gizmos.color = Color.red;
            Vector3 upStartPos = center + new Vector3(-totalWidth / 2, 0, BattleDefine.BATTLE_HALF_CENTERHEIGHT);
            Vector3 downStartPos = center + new Vector3(-totalWidth / 2, 0, -BattleDefine.BATTLE_HALF_CENTERHEIGHT - BattleDefine.BATTLE_CELL_HEIGHT * BattleDefine.BATTLE_HEIGHT);
            for (int x = 0; x < BattleDefine.BATTLE_WIDTH; x++)
            {
                for (int y = 0; y < BattleDefine.BATTLE_HEIGHT; y++)
                {
                    Vector3 posUp = upStartPos + new Vector3(BattleDefine.BATTLE_CELL_WIDTH * (0.5f + x), 0, BattleDefine.BATTLE_CELL_HEIGHT * (0.5f + y));
                    // Debug.LogError(x + ":" + y + ":" + posUp);
                    Gizmos.DrawWireCube(posUp, new Vector3(BattleDefine.BATTLE_CELL_WIDTH, height, BattleDefine.BATTLE_CELL_HEIGHT));

                    Vector3 posDown = downStartPos + new Vector3(BattleDefine.BATTLE_CELL_WIDTH * (0.5f + x), 0, BattleDefine.BATTLE_CELL_HEIGHT * (0.5f + y));
                    Gizmos.DrawWireCube(posDown, new Vector3(BattleDefine.BATTLE_CELL_WIDTH, height, BattleDefine.BATTLE_CELL_HEIGHT));
                }
            }
        }
    }

}