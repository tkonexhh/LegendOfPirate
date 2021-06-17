using UnityEngine;
using UnityEngine.UI;
using Qarth.Extension;
using Qarth;
using UniRx;
using TMPro;

namespace GameWish.Game
{
    public class BattleFieldPanelData : UIPanelData
    {
        public BattleFieldPanelData()
        {
        }
    }

    public partial class BattleFieldPanel
    {
        private BattleFieldPanelData m_PanelData = null;

        private void AllocatePanelData(params object[] args)
        {
            m_PanelData = UIPanelData.Allocate<BattleFieldPanelData>();
        }

        private void ReleasePanelData()
        {
            ObjectPool<BattleFieldPanelData>.S.Recycle(m_PanelData);
        }

        private void BindModelToUI()
        {
        }

        private void BindUIToModel()
        {

        }

    }
}
