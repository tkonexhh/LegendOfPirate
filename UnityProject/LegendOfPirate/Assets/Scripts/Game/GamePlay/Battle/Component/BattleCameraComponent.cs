using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameWish.Game
{
    public class BattleCameraComponent : AbstractBattleComponent
    {
        public override void OnBattleInit(BattleFieldConfigSO enemyConfigSO)
        {
            GameCameraMgr.S.ToBattle();
            ToBattle();
        }

        public void ToPrepare()
        {
            BattleMgr.CameraField.Priority = 10;
            BattleMgr.CameraBattle.Priority = 0;
        }

        public void ToBattle()
        {
            BattleMgr.CameraField.Priority = 0;
            BattleMgr.CameraBattle.Priority = 10;
        }
    }

}