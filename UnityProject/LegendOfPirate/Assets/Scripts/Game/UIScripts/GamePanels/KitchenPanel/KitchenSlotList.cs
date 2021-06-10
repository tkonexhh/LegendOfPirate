using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Qarth;

namespace GameWish.Game
{
    public class KitchenSlotList : MonoBehaviour
    {
        [SerializeField] private List<KitchenSlot> m_KitchenSlotList;
        private int m_CookingCount;
        private int m_MaxCookingCount;
        public void SetUnlockSlot(int count)
        {
            m_MaxCookingCount = count;
            for (int i = 0; i < count; i++)
            {
                m_KitchenSlotList[0].UnLockKitchenSlot(true);
            }
        }
        public void UseSlot() 
        {
            if (m_CookingCount < m_MaxCookingCount)
            {
                m_KitchenSlotList[m_CookingCount].UseSlot();
                m_CookingCount++;
            }
            else 
            {
                FloatMessage.S.ShowMsg("There is not enough space for cooking");
            }
        }
    }

}