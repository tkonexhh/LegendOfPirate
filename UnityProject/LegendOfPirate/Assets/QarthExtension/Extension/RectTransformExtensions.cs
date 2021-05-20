// Copyright (c) 2017 Doozy Entertainment / Marlink Trading SRL and Ez Entertainment / Ez Entertainment SRL. All Rights Reserved.
// This code is a collaboration between Doozy Entertainment and Ez Entertainment and is not to be used in any other assets other then the ones created by their respective companies.
// This code can only be used under the standard Unity Asset Store End User License Agreement
// A Copy of the EULA APPENDIX 1 is available at http://unity3d.com/company/legal/as_terms

using UnityEngine;

namespace Qarth
{
    public enum AnchorPresets
    {
        TopLeft,
        TopCenter,
        TopRight,

        MiddleLeft,
        MiddleCenter,
        MiddleRight,

        BottomLeft,
        BottonCenter,
        BottomRight,
        BottomStretch,

        VertStretchLeft,
        VertStretchRight,
        VertStretchCenter,

        HorStretchTop,
        HorStretchMiddle,
        HorStretchBottom,

        StretchAll
    }

    public enum PivotPresets
    {
        TopLeft,
        TopCenter,
        TopRight,

        MiddleLeft,
        MiddleCenter,
        MiddleRight,

        BottomLeft,
        BottomCenter,
        BottomRight,
    }


    public static class RectTransformExtensions
    {
        public static void SetDefaultScale(this RectTransform trans)
        {
            trans.localScale = new Vector3(1, 1, 1);
        }

        public static void SetPivotAndAnchors(this RectTransform trans, Vector2 aVec)
        {
            trans.pivot = aVec;
            trans.anchorMin = aVec;
            trans.anchorMax = aVec;
        }

        public static void SetAnchor(this RectTransform trans, Vector2 aVec)
        {
            trans.anchorMin = aVec;
            trans.anchorMax = aVec;
        }

        public static void SetAnchor(this RectTransform trans, Vector2 aVecMin, Vector2 aVecMax)
        {
            trans.anchorMin = aVecMin;
            trans.anchorMax = aVecMax;
        }

        public static void SetAnchor(this RectTransform source, AnchorPresets allign, int offsetX = 0, int offsetY = 0)
        {
            source.anchoredPosition = new Vector3(offsetX, offsetY, 0);

            switch (allign)
            {
                case (AnchorPresets.TopLeft):
                    source.SetAnchor(new Vector2(0, 1));
                    break;
                case (AnchorPresets.TopCenter):
                    source.SetAnchor(new Vector2(0.5f, 1));
                    break;
                case (AnchorPresets.TopRight):
                    source.SetAnchor(new Vector2(1, 1));
                    break;
                case (AnchorPresets.MiddleLeft):
                    source.SetAnchor(new Vector2(0, 0.5f));
                    break;
                case (AnchorPresets.MiddleCenter):
                    source.SetAnchor(new Vector2(0.5f, 0.5f));
                    break;
                case (AnchorPresets.MiddleRight):
                    source.SetAnchor(new Vector2(1, 0.5f));
                    break;
                case (AnchorPresets.BottomLeft):
                    source.SetAnchor(new Vector2(0, 0));
                    break;
                case (AnchorPresets.BottonCenter):
                    source.SetAnchor(new Vector2(0.5f, 0));
                    break;
                case (AnchorPresets.BottomRight):
                    source.SetAnchor(new Vector2(1, 0));
                    break;
                case (AnchorPresets.HorStretchTop):
                    source.SetAnchor(new Vector2(0, 1), new Vector2(1, 1));
                    break;
                case (AnchorPresets.HorStretchMiddle):
                    source.SetAnchor(new Vector2(0, 0.5f), new Vector2(1, 0.5f));
                    break;
                case (AnchorPresets.HorStretchBottom):
                    source.SetAnchor(new Vector2(0, 0), new Vector2(1, 0));
                    break;
                case (AnchorPresets.VertStretchLeft):
                    source.SetAnchor(new Vector2(0, 0), new Vector2(0, 1));
                    break;
                case (AnchorPresets.VertStretchCenter):
                    source.SetAnchor(new Vector2(0.5f, 0), new Vector2(0.5f, 1));
                    break;
                case (AnchorPresets.VertStretchRight):
                    source.SetAnchor(new Vector2(1, 0), new Vector2(1, 1));
                    break;
                case (AnchorPresets.StretchAll):
                    source.SetAnchor(new Vector2(0, 0), new Vector2(1, 1));
                    break;
            }

        }

        public static void SetPivot(this RectTransform source, PivotPresets preset)
        {
            switch (preset)
            {
                case (PivotPresets.TopLeft):
                    source.pivot = new Vector2(0, 1);
                    break;
                case (PivotPresets.TopCenter):
                    source.pivot = new Vector2(0.5f, 1);
                    break;
                case (PivotPresets.TopRight):
                    source.pivot = new Vector2(1, 1);
                    break;
                case (PivotPresets.MiddleLeft):
                    source.pivot = new Vector2(0, 0.5f);
                    break;
                case (PivotPresets.MiddleCenter):
                    source.pivot = new Vector2(0.5f, 0.5f);
                    break;
                case (PivotPresets.MiddleRight):
                    source.pivot = new Vector2(1, 0.5f);
                    break;
                case (PivotPresets.BottomLeft):
                    source.pivot = new Vector2(0, 0);
                    break;
                case (PivotPresets.BottomCenter):
                    source.pivot = new Vector2(0.5f, 0);
                    break;
                case (PivotPresets.BottomRight):
                    source.pivot = new Vector2(1, 0);
                    source.SetLeftTopPosition(new Vector3(0, 0));
                    break;
            }
        }


        public static Vector2 GetSize(this RectTransform trans)
        {
            return trans.rect.size;
        }

        public static float GetWidth(this RectTransform trans)
        {
            return trans.rect.width;
        }

        public static float GetHeight(this RectTransform trans)
        {
            return trans.rect.height;
        }

        public static void SetPositionOfPivot(this RectTransform trans, Vector2 newPos)
        {
            trans.localPosition = new Vector3(newPos.x, newPos.y, trans.localPosition.z);
        }

        public static void SetLeftBottomPosition(this RectTransform trans, Vector2 newPos)
        {
            trans.localPosition = new Vector3(newPos.x + (trans.pivot.x * trans.rect.width), newPos.y + (trans.pivot.y * trans.rect.height), trans.localPosition.z);
        }

        public static void SetLeftTopPosition(this RectTransform trans, Vector2 newPos)
        {
            trans.localPosition = new Vector3(newPos.x + (trans.pivot.x * trans.rect.width), newPos.y - ((1f - trans.pivot.y) * trans.rect.height), trans.localPosition.z);
        }

        public static void SetRightBottomPosition(this RectTransform trans, Vector2 newPos)
        {
            trans.localPosition = new Vector3(newPos.x - ((1f - trans.pivot.x) * trans.rect.width), newPos.y + (trans.pivot.y * trans.rect.height), trans.localPosition.z);
        }

        public static void SetRightTopPosition(this RectTransform trans, Vector2 newPos)
        {
            trans.localPosition = new Vector3(newPos.x - ((1f - trans.pivot.x) * trans.rect.width), newPos.y - ((1f - trans.pivot.y) * trans.rect.height), trans.localPosition.z);
        }

        public static void SetSize(this RectTransform trans, Vector2 newSize)
        {
            Vector2 oldSize = trans.rect.size;
            Vector2 deltaSize = newSize - oldSize;
            trans.offsetMin = trans.offsetMin - new Vector2(deltaSize.x * trans.pivot.x, deltaSize.y * trans.pivot.y);
            trans.offsetMax = trans.offsetMax + new Vector2(deltaSize.x * (1f - trans.pivot.x), deltaSize.y * (1f - trans.pivot.y));
        }

        public static void SetWidth(this RectTransform trans, float newSize)
        {
            SetSize(trans, new Vector2(newSize, trans.rect.size.y));
        }

        public static void SetHeight(this RectTransform trans, float newSize)
        {
            SetSize(trans, new Vector2(trans.rect.size.x, newSize));
        }
    }
}
