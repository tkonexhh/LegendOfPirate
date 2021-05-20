using System.Collections;
using System.Collections.Generic;
using Qarth;
using QuickEngine.Extensions;
using UnityEngine;
[ExecuteInEditMode]
public class GrideLayout : MonoBehaviour
{

    [SerializeField] private float m_Pading;

    [SerializeField] public float m_Spacing;

    [SerializeField] private float m_RebuildSmooth = 0.3f;

    [SerializeField] private int m_Row;//hang

    [SerializeField] private int m_Col;

    [SerializeField] private Vector2 m_CellSize;



    //void Start()
    //{
    //    Init();
    //}

    public void BuildChild()
    {
        Vector2 m_Pos = Vector2.zero;
        transform.rectTransform().pivot = new Vector2(0.5f, 1);

        if (m_Col == 0 || m_Row == 0)
        {
            Log.w("row or col cant be 0");
            m_Col = 1;
            m_Row = transform.childCount;
        }

        float w = m_Col * m_CellSize.x + m_Spacing * (m_Col - 1);
        Vector2 startPos = new Vector2(-(w - m_CellSize.x) / 2, -m_CellSize.y / 2);

        //针对隐藏物体的处理
        int index = 0;
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).gameObject.activeSelf == false)
                continue;
            transform.GetChild(i).rectTransform().pivot = new Vector2(0.5f, 0.5f);
            transform.GetChild(i).rectTransform().anchorMin = new Vector2(0.5f, 1);
            transform.GetChild(i).rectTransform().anchorMax = new Vector2(0.5f, 1);

            transform.GetChild(i).rectTransform().SetHeight(m_CellSize.y);
            m_Pos = startPos + new Vector2(index % 3 * (m_CellSize.x + m_Spacing), -index / 3 * (m_Spacing + m_CellSize.y));
            transform.GetChild(i).rectTransform().anchoredPosition = m_Pos;
            index++;
        }
    }
}
