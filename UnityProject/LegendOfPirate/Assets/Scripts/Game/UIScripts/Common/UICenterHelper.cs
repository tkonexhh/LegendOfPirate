using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using Qarth;

namespace GameWish.Game
{
    public class UICenterHelper
    {

        public static void CenterImageWithText(Image img, Text text, float offset)
        {
            float texWidth = text.preferredWidth;
            float imgWidth = img.preferredWidth;
            float totalWidth = texWidth + imgWidth + offset;

            float start = -totalWidth * 0.5f;
            Vector3 imgPos = img.transform.localPosition;
            imgPos.x = start + imgWidth * 0.5f;
            img.transform.localPosition = imgPos;

            Vector3 texPos = text.transform.localPosition;
            texPos.x = start + imgWidth + offset;// + texWidth;// * 0.5f;

            text.transform.localPosition = texPos;
        }

        public static void CenterTextWithText(Text textOne, Text textTwo, float offset)
        {
            float tex1Width = textOne.preferredWidth;
            float tex2Width = textTwo.preferredWidth;
            float totalWidth = tex1Width + tex2Width + offset;

            float start = -totalWidth * 0.5f;
            Vector3 textOnePos = textOne.transform.localPosition;
            textOnePos.x = start + tex1Width * 0.5f;
            textOne.transform.localPosition = textOnePos;

            Vector3 textTwoPos = textTwo.transform.localPosition;
            textTwoPos.x = start + tex1Width + offset;

            textTwo.transform.localPosition = textTwoPos;
        }
    }
}
