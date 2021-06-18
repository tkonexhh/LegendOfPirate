using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace GameWish.Game
{
    public class BattleField : MonoBehaviour
    {
        public Vector3 Position => transform.position;
        public BattleRoleController Controller { get; private set; }

        public void SetBattleRoleController(BattleRoleController controller, bool Recycle = true)
        {
            if (Controller != null && Recycle)
            {
                BattleRoleControllerFactory.RecycleBattleRole(Controller);
            }
            this.Controller = controller;
            if (Controller != null)
            {
                this.Controller.transform.position = this.Position;
            }
        }
    }
}