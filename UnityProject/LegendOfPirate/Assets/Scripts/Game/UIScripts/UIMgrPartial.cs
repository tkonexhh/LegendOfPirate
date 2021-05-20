using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using GameWish.Game;
using UnityEngine;
using UnityEngine.UI;

namespace Qarth
{
    public partial class UIMgr
    {

        public void RefreshTextAndImage(Transform t,Text tex,int count)
        {
            int startNum = int.Parse(tex.text);
            int endNum = count;
            if (endNum - startNum != 0)
            {
                //play image animation
                Sequence sq = DOTween.Sequence();
                sq.Append(t.DOLocalMoveY(15, 0.1f));
                sq.Append(t.DOLocalMoveY(0, 0.1f));
                sq.SetLoops(4);
                //update coin text
                int jumpTimes = Mathf.Min(endNum - startNum, 10);
                StartCoroutine(JumpNumber(startNum, endNum, jumpTimes,tex));
            }
        }

        private IEnumerator JumpNumber(int startNumber, int endNumber, int jumpTIme,Text tex)
        {
            int delta = (endNumber - startNumber) / jumpTIme;
            int result = startNumber;

            for (int i = 0; i < jumpTIme; i++)
            {
                result += delta;
                tex.text = result.ToString();
                yield return new WaitForSeconds(0.05f);
            }

            result = endNumber;
            tex.text = result.ToString();
            StopCoroutine(JumpNumber(startNumber, endNumber, jumpTIme,tex));
        }
    }
}

