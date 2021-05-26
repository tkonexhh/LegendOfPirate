using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace GameWish.Game
{
    [ModelAutoRegister]
	public class RoleModel : Model
	{
        public IntReactiveProperty hp;
        //public int MaxHp;
        //public int Dog;//闪避率
        //public int Critical;//暴击率
        //public int Atk;//物理攻击
        //public int Def;//物理防御
        //public int Mag;//法术攻击
        //public int Wil;//法术防御
        //public float attackSpeed;//攻击速率
        //public float moveSpeed;//移动速度

        protected override void LoadDataFromDb()
        {
            base.LoadDataFromDb();
        }
    }

}