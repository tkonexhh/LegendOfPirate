using UnityEngine;
using UnityEngine.UI;
using Qarth.Extension;
using Qarth;
using UniRx;

namespace GameWish.Game
{
    public class RoleGetPanelData : UIPanelData
    {
        public RoleModel roleModel;
        public int[] skillId;
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

        private void InitPanelMsg() 
        {
            m_RoleName.text = m_PanelData.roleModel.name;
            m_Attribute1.text = string.Format("Health {0}", m_PanelData.roleModel.curHp);
            m_Attribute2.text = string.Format("Attack {0}", m_PanelData.roleModel.curAtk);
        }

        private void GetRoleModel(int id) 
        {
            m_PanelData.roleModel = ModelMgr.S.GetModel<RoleGroupModel>().GetRoleModel(id);
            InitPanelData();
        }

        private void InitPanelData() 
        {
            for (int i = 0; i < m_PanelData.roleModel.skillList.Count; i++) 
            {
                m_PanelData.skillId[i] = m_PanelData.roleModel.skillList[i].skillId;
            }
        }

        private void OnClickAddListener()
        {
            m_CloseBtn.onClick.AddListener(() => 
            {
                HideSelfWithAnim();
            });

        }


    }
}
