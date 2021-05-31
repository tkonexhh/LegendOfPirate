using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameWish.Game
{
    public abstract class InventoryItem<T> : IInventoryItem where T : System.IConvertible
    {
        protected int m_Count;
        protected T m_Id;

        public InventoryItem(T id, int count)
        {
            m_Id = id;
            m_Count = count;
        }

        #region IInventoryItem

        public void AddCount(int deltaCount)
        {
            m_Count += deltaCount;
            m_Count = Mathf.Max(0, m_Count);
        }

        public void SubCount(int deltaCount)
        {
            m_Count -= deltaCount;
            m_Count = Mathf.Max(0, m_Count);
        }

        public int GetCount()
        {
            return m_Count;
        }

        public int GetId()
        {
            return m_Id.ToInt32(null);
        }

        public abstract string GetDesc();

        public abstract string GetName();

        #endregion

    }

}