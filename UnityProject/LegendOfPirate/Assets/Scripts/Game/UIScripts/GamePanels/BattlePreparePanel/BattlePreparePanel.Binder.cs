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
        private static ResLoader s_ResLoader = null;
        private static BattleFieldConfigSO s_Instance = null;

        private void AllocatePanelData(params object[] args)
        {
            m_PanelData = UIPanelData.Allocate<BattlePreparePanelData>();
            m_PanelData.roleGroupModel = ModelMgr.S.GetModel<RoleGroupModel>();
        }

        private void ReleasePanelData()
        {
            ObjectPool<BattlePreparePanelData>.S.Recycle(m_PanelData);
            ReleaseEnemy();
        }
        private BattleFieldConfigSO CreateEnemy(string levelId)
        {
            string name = string.Format("BattleFieldConfigSO_{0}", levelId);
            s_ResLoader = ResLoader.Allocate(name, null);

            UnityEngine.Object obj = s_ResLoader.LoadSync(name);
            if (obj == null)
            {
                Log.e(string.Format("Not Find BattleFieldConfigSO_{0}, Will Use Default new_BattleFieldConfigSO.", levelId));
                s_ResLoader.ReleaseAllRes();

                return null;
            }

            Log.i(string.Format("Success Load BattleFieldConfigSO_{0}.", levelId));

            s_Instance = obj as BattleFieldConfigSO;

            return s_Instance;
        }

        public static void ReleaseEnemy()
        {
            s_Instance = null;

            s_ResLoader.Recycle2Cache();
            s_ResLoader = null;
        }
        private void BindModelToUI()
        {
        }

        private void BindUIToModel()
        {
        }

    }
}
