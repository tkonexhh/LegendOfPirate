using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace GameWish.Game
{
    public abstract class InventoryItemModel<T> : Model, IInventoryItemModel where T : System.IConvertible
    {
        protected IntReactiveProperty m_Count;
        protected T m_Id;

        public InventoryItemModel(T id, IntReactiveProperty count)
        {
            m_Id = id;
            m_Count = count;
        }

        #region IInventoryItemModel

        public void AddCount(int deltaCount)
        {
            m_Count.Value += deltaCount;
            m_Count.Value = Mathf.Max(0, m_Count.Value);
        }

        public void SubCount(int deltaCount)
        {
            m_Count.Value -= deltaCount;
            m_Count.Value = Mathf.Max(0, m_Count.Value);
        }

        public int GetCount()
        {
            return m_Count.Value;
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