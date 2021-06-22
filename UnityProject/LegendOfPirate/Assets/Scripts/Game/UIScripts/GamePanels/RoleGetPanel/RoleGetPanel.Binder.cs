using UnityEngine;
using UnityEngine.UI;
using Qarth.Extension;
using Qarth;
using UniRx;

namespace GameWish.Game
{
    public class RoleGetPanelData : UIPanelData
    {
        public RoleGetPanelData()
        {
        }
    }

    public partial class RoleGetPanel
    {
        private RoleGetPanelData m_PanelData = null;

        private void AllocatePanelData()
        {
            m_PanelData = UIPanelData.Allocate<RoleGetPanelData>();
        }

        private void ReleasePanelData()
        {
            ObjectPool<RoleGetPanelData>.S.Recycle(m_PanelData);
        }

        private void BindModelToUI()
        {
        }

        private void BindUIToModel()
        {
        }
        private void OnClickAddListener()
        {
            CloseBtn.onClick.AddListener(() => 
            {
                CloseSelfPanel();
            });

        }


    }
}
