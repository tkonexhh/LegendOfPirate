using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Qarth;

namespace GameWish.Game
{
    public class MainMenuPanel : AbstractAnimPanel
    {

        public UGridListView m_UGridListView;
        public Transform m_Content;

        private List<ItemBase> m_InventroyList;
        protected override void OnUIInit()
        {
            base.OnUIInit();

        }
        private void OnCellRenderer(Transform root, int index)
        {
         
            if (index< m_InventroyList.Count)
            {
                root.GetComponent<ItemUI>().OnInit(index, m_InventroyList[index]);
            }

        }
        protected override void OnOpen()
        {
            base.OnOpen();
            m_InventroyList = InventroyMgr.S.GetAllItem();

            m_UGridListView.SetCellRenderer(OnCellRenderer);
            m_UGridListView.SetDataCount(100);
        }
    }

}