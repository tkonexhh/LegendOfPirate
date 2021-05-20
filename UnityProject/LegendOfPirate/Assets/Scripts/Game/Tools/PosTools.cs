using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Qarth;
namespace GameWish.Game
{  
    public class PosTools : TSingleton<PosTools> 
    {
        public static Vector2 ConvertWorldToUIPos(Vector3 worldPos, Canvas canvas)
        {
            Vector2 pos;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform,
                Camera.main.WorldToScreenPoint(worldPos),
                canvas.worldCamera,
                out pos);

            return pos;
        }
    }
}