using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using Qarth;

namespace GameWish.Game
{
    public class ColorGradientHelper
    {
        public static Color CalcualteColor(Color[] colors, float precent)
        {
            if (precent >= 1.0f)
            {
                return colors[colors.Length - 1];
            }

            float offset = 1.0f / (colors.Length - 1);

            int index = (int)(precent / offset);

            if (index >= (colors.Length - 1))
            {
                return colors[colors.Length - 1];
            }

            float startValue = offset * index;

            float adjustPrecent = (precent - startValue) / offset;

            Color c0 = colors[index];
            Color c1 = colors[index + 1];

            float inverPrecent = 1.0f - adjustPrecent;

            float r = c0.r * inverPrecent + c1.r * adjustPrecent;
            float g = c0.g * inverPrecent + c1.g * adjustPrecent;
            float b = c0.b * inverPrecent + c1.b * adjustPrecent;
            float a = c0.a * inverPrecent + c1.a * adjustPrecent;

            return new Color(r, g, b, a);
        }
    }
}
