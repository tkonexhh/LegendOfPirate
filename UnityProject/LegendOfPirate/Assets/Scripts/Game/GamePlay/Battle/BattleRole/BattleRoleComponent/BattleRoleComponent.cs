using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameWish.Game
{
    public abstract class BattleRoleComponent
    {
        public BattleRoleController controller { get; private set; }
        public bool battleStarted { get; private set; }
        public BattleRoleComponent(BattleRoleController controller)
        {
            this.controller = controller;
        }

        public virtual void OnBattleStart()
        {
            battleStarted = true;
        }

        public virtual void OnUpdate() { }
        public virtual void OnDestroy()
        {
            battleStarted = false;
        }

    }

}