using UnityEngine;
using UnityEngine.UI;
using Qarth.Extension;
using Qarth;
using UniRx;
using TMPro;
using DG.Tweening;
namespace GameWish.Game
{
	public class RoleGrowthPanelData : UIPanelData
	{
		public int roleID;
		public RoleModel roleModel = null;
		public RoleGrowthPanelData()
		{
		}
	}
	
	public partial class RoleGrowthPanel
	{
		private RoleGrowthPanelData m_PanelData = null;
	    private int   exHp;
        private float exAtk;
        private int   exExp;
        private int   starLevel;

        private void AllocatePanelData()
		{
			 m_PanelData = UIPanelData.Allocate<RoleGrowthPanelData>();
			
		}

		private void GetRoleData(int roleId) 
		{
			m_PanelData.roleModel = ModelMgr.S.GetModel<RoleGroupModel>().GetRoleModel(roleId);
			exHp = m_PanelData.roleModel.curHp.Value;
			exAtk = m_PanelData.roleModel.curHp.Value;
			exExp = m_PanelData.roleModel.curExp.Value;
			starLevel = m_PanelData.roleModel.starLevel.Value;
			m_PanelData.roleModel.level.Value += 1;
			SetPanelMsg();
		}

		private void SetPanelMsg() 
		{
			SetHpMsg();
			SetAttackMsg();	
        }
		/// <summary>
		/// 只实现显示功能 需要修改Model与Data
		/// </summary>
		private void SetExpMsg() 
		{
            var Attributes = m_GrowthMsgCap.GetComponentInChildren<TextMeshProUGUI>().GetComponentsInChildren<TextMeshProUGUI>();
            Attributes[1].text = exExp.ToString();
            DG.Tweening.Core.DOSetter<float> setter = (x) =>
            {
                Attributes[2].text = string.Format("{0}", (Mathf.RoundToInt(x)).ToString());
            };
            DOTween.To(setter, exExp, m_PanelData.roleModel.GetCurExp(), 1.0f);
        }

		private void SetHpMsg() 
		{
            var Attributes = m_GrowthMsgHp.GetComponentInChildren<TextMeshProUGUI>().GetComponentsInChildren<TextMeshProUGUI>();
            Attributes[1].text = exHp.ToString();
            DG.Tweening.Core.DOSetter<float> setter = (x) =>
            {
                Attributes[2].text = string.Format("{0}", (Mathf.RoundToInt(x)).ToString());
            };
            DOTween.To(setter, 0, m_PanelData.roleModel.GetCurHp(), 1.0f);
        }

		private void SetAttackMsg() 
		{
            var Attributes = m_GrowthMsgAtt.GetComponentInChildren<TextMeshProUGUI>().GetComponentsInChildren<TextMeshProUGUI>();
            Attributes[1].text = exAtk.ToString();
            DG.Tweening.Core.DOSetter<float> setter = (x) =>
            {
                Attributes[2].text = string.Format("{0}", (Mathf.RoundToInt(x)).ToString());
            };
            DOTween.To(setter, 0, m_PanelData.roleModel.GetCurAtk(), 1.0f);
        }

		private void ReleasePanelData()
		{
			ObjectPool<RoleGrowthPanelData>.S.Recycle(m_PanelData);
		}
		
		private void BindModelToUI()
		{
		}
		
		private void BindUIToModel()
		{

		}
		
	}
}
