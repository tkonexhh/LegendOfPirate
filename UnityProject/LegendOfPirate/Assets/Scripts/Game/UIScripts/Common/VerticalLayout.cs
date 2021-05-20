using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using QuickEngine.Extensions;
using UnityEngine;

[Serializable]
public class VerticalLayout : MonoBehaviour
{

    [SerializeField] private float pading;

    [SerializeField] public float spacing;

    [SerializeField] private float rebuildSmooth = 0.3f;


    public void BuildChild()
    {
        transform.rectTransform().pivot=new Vector2(0.5f,1);
        Vector2 lastPos = Vector2.zero;
        Vector2 target = Vector2.zero;

        for (int i = 0; i < transform.childCount; i++)
        {
            if(transform.GetChild(i).gameObject.activeSelf==false)
                continue;

            transform.GetChild(i).rectTransform().anchorMax=new Vector2(0.5f,1);
            transform.GetChild(i).rectTransform().anchorMin=new Vector2(0.5f,1);
            transform.GetChild(i).rectTransform().pivot = new Vector2(0.5f, 0.5f);

            //针对空物体的处理
            target = lastPos - new Vector2(0, 0.5f * transform.GetChild(i).rectTransform().rect.height);
            target.y -= (Mathf.Abs(target.y - lastPos.y)<=0.001f||i==0 ? 0 : spacing);

            transform.GetChild(i).rectTransform().anchoredPosition = target;
            lastPos = target - new Vector2(0, 0.5f * transform.GetChild(i).rectTransform().rect.height);
        }

        //transform.rectTransform().SetHeight(Mathf.Abs(lastPos.y));
        //SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical,Mathf.Abs(lastPos.y));
    }

    //public void RebuildChild()
    //{
    //    lastPos = Vector3.zero;

    //    for (int i = 0; i < transform.childCount; i++)
    //    {
    //        transform.GetChild(i).rectTransform().anchorMin=new Vector2(0.5f,1);
    //        transform.GetChild(i).rectTransform().anchorMax=new Vector2(0.5f,1);

    //        Vector2 target =
    //            lastPos - new Vector2(0, 0.5f * transform.GetChild(i).rectTransform().rect.height + spacing);
    //        transform.GetChild(i).DOLocalMove(target, rebuildSmooth);
    //        lastPos = target - new Vector3(0, 0.5f * transform.GetChild(i).rectTransform().rect.height + spacing, 0);
    //    }

    //    transform.rectTransform().SetHeight(Mathf.Abs(lastPos.y));
    //    //SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical,Mathf.Abs(lastPos.y));
    //}
}
