using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameWish.Game
{
    //动画下推状态机
    public class BattleRoleAnimationPDA : BattleRoleComponent
    {
        public BattleRoleAnimationPDA(BattleRoleController controller) : base(controller) { }

        Stack<string> m_AnimationStack = new Stack<string>();


        public void PushAnimation(string animationName)
        {
            m_AnimationStack.Push(animationName);
        }

        public void PopAnimation()
        {

        }
    }

}