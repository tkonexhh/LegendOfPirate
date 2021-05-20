using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using Qarth;

public class DoTweenAnimExtern
{

    public static Sequence PlayScaleAnimQuence(Transform target,float delay, float duration, params float[] allScale)
    {
        
        var sequence = DOTween.Sequence();
        if (delay > 0)
        {
            sequence.AppendInterval(delay);
        }
        for (int i = 0; i < allScale.Length; i++)
        {
            Vector3 scale = new Vector3(allScale[i],allScale[i],allScale[i]);
            sequence = sequence.Append(target.DOScale(scale, duration).SetEase(Ease.Linear));
        }
        return sequence;
    }

    public static Tweener DoScale(Transform target,Vector3 targetScale, float duration)
    {
        return target.DOScale(targetScale, duration);
    }

    public static Sequence DoScaleBreathe(Transform target,float scaleFactor,float duration,bool loop=false)
    {
        Sequence s = DOTween.Sequence().Append(DoScale(target, (1 + scaleFactor) * Vector3.one, duration))
            .Append(DoScale(target, (1 - scaleFactor) * Vector3.one, duration)).SetLoops(100);
        return s;
    }
}


