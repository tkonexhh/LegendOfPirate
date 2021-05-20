using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Qarth;

namespace GameWish.Game
{
	public class DoubleIncomeAdHandler : PersistentlyAdEffectHandler
	{
        public DoubleIncomeAdHandler(AdType adType) : base(adType)
        {
            m_PerAddTime = 1800;
        }

        protected override void StartAdEffect()
        {
            base.StartAdEffect();

            AdEffectParams.isInDoubleIncomeAdEnhance = true;
            // TODO: start special effect
        }

        protected override void EndAdEffect()
        {
            base.EndAdEffect();

            AdEffectParams.isInDoubleIncomeAdEnhance = false;
        }
        public override void OnTick(int count)
        {
            base.OnTick(count);

            //TODO: 发送每次刷新效果
        }

    }
	
}