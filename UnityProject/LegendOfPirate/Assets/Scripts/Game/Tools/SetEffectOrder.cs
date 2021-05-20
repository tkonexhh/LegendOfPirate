using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Qarth;

/// <summary>
/// 调整粒子效果的渲染顺序
/// </summary>
public class SetEffectOrder : MonoBehaviour
{
    [SerializeField]
    private Canvas parentCanvas;
    [SerializeField]
    private int offset;
    //void Awake()
    //{
    //    UIMgr.S.uiEventSystem.Register(parentPanel.panelID, SetOrder);
    //}

    //public void SetCanvas(Canvas canvas)
    //{
    //    parentCanvas =canvas;
    //    this.enabled = true;
    //}

    public void RefreshSortingOrder()
    {
        //this.enabled = true;

        SetOrder();
    }

    public void SetOrder()
    {
        if (parentCanvas == null)
        {
            //Log.w("=====canvas is null!!!=====");
            return;
        }
        //第一个参数为触发的事件类型
        Renderer[] renders = GetComponentsInChildren<Renderer>();
        foreach (Renderer r in renders)
        {
            r.sortingOrder = parentCanvas.sortingOrder + offset;
        }
    }

    //void LateUpdate()
    //{
    //    SetOrder();
    //    this.enabled = false;
    //}

}
