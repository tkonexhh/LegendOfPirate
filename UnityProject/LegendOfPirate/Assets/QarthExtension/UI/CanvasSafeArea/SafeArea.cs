using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameWish.Game
{
	public class SafeArea : MonoBehaviour
	{
	    [SerializeField] private Canvas m_Canvas;
	    private void Awake()
	    {
	        var safeRect = Screen.safeArea;
	        var canvasRect = (m_Canvas.transform as RectTransform).rect;
	        float deltaTop = (Screen.height - safeRect.height) / Screen.height * canvasRect.height;
	        // Debug.LogError(safeRect + "-----" + canvasRect + "------" + Screen.height + "----" + deltaTop);
	
	        var rectRoot = transform as RectTransform;
	
	        Vector2 anchorMin = new Vector2(safeRect.x, safeRect.y);
	        Vector2 anchorMax = anchorMin + new Vector2(safeRect.width, safeRect.height);
	        anchorMin.x /= Screen.width;
	        anchorMin.y /= Screen.height;
	        anchorMax.x /= Screen.width;
	        anchorMax.y /= Screen.height;
	        if (anchorMin.x >= 0 && anchorMin.y >= 0 && anchorMax.x >= 0 && anchorMax.y >= 0)
	        {
	            rectRoot.anchorMin = anchorMin;
	            rectRoot.anchorMax = anchorMax;
	        }
	
	        // rectRoot.offsetMax = new Vector2(rectRoot.offsetMax.x, -deltaTop);
	        // rectRoot.offsetMin = new Vector2(rectRoot.offsetMin.x, 0);
	        // m_TopLeft.anchoredPosition = new Vector2(0, -deltaTop);
	        // m_TopRight.anchoredPosition = new Vector2(0, -deltaTop);
	    }
	}
	
}