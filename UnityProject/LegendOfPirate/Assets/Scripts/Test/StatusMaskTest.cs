using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameWish.Game
{
    public class StatusMaskTest : MonoBehaviour
    {
        StatusMask statusMask;
        void Start()
        {
            statusMask = new StatusMask();
            Debug.LogError("1:" + statusMask.ToString());
            statusMask.AddStatus(StatusControlType.MoveForbid);
            Debug.LogError(statusMask.HasStatus(StatusControlType.MoveForbid));
            Debug.LogError("2:" + statusMask.ToString());
            statusMask.RemoveStatus(StatusControlType.MoveForbid);
            Debug.LogError(statusMask.HasStatus(StatusControlType.MoveForbid));
            Debug.LogError("3:" + statusMask.ToString());


            Debug.LogError(statusMask.HasStatus(StatusControlType.AttackForbid));
        }


    }

}