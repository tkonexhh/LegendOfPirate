using UnityEngine;
using UnityEngine.UI;
using Qarth.Extension;
using Qarth;
using UniRx;

namespace GameWish.Game
{
    public class SmuggleChooseRolePanelData : UIPanelData
    {
        public SmuggleChooseRolePanelData()
        {
        }
    }

    public partial class SmuggleChooseRolePanel
    {
        private SmuggleChooseRolePanelData m_PanelData = null;

        private void AllocatePanelData(params object[] args)
        {
            m_PanelData = UIPanelData.Allocate<SmuggleChooseRolePanelData>();
        }

        private void ReleasePanelData()
        {
            ObjectPool<SmuggleChooseRolePanelData>.S.Recycle(m_PanelData);
        }

        private void BindModelToUI()
        {
        }

        private void BindUIToModel()
        {
        }
        private void OnClickAddListener()
        {
            BgBtn.OnClickAsObservable().Subscribe(_ =>
            {
                ExitBtnEvent();
            });
        }
    }
}
