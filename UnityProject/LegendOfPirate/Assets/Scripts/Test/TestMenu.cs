using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameWish.Game
{
    public class TestMenu : MonoBehaviour
    {
        private void OnGUI()
        {
            GUILayout.BeginHorizontal();
            if (GUI.Button(new Rect(0, 0, 200, 200), "BattleStart"))
            {
                BattleMgr.S.BattleInit();
            }
            if (GUI.Button(new Rect(200, 0, 200, 200), "BattleStart"))
            {
                BattleMgr.S.BattleStart();
            }

            if (GUI.Button(new Rect(400, 0, 200, 200), "BattleEnd"))
            {
                BattleMgr.S.BattleEnd();
                BattleMgr.S.BattleClean();
            }

            GUILayout.EndHorizontal();
        }
    }

}