using UnityEngine;
using UnityEngine.UI;
using Qarth.Extension;
using Qarth;
using UniRx;

namespace GameWish.Game
{
    public class BattlePreparePanelData : UIPanelData
    {
        public RoleGroupModel roleGroupModel;
        public BattlePreparePanelData()
        {
        }
    }

    public partial class BattlePreparePanel
    {
        private BattlePreparePanelData m_PanelData = null;

        private void AllocatePanelData(params object[] args)
        {
            m_PanelData = UIPanelData.Allocate<BattlePreparePanelData>();
            m_PanelData.roleGroupModel = ModelMgr.S.GetModel<RoleGroupModel>();
        }

        private void ReleasePanelData()
        {
            ObjectPool<BattlePreparePanelData>.S.Recycle(m_PanelData);
        }

        private void BindModelToUI()
        {
        }

        private void BindUIToModel()
        {
        }

    }
}
