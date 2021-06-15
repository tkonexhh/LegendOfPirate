using UnityEngine;
using UnityEngine.UI;
using Qarth.Extension;
using Qarth;
using UniRx;

namespace GameWish.Game
{
	public class SubMiddleDownMaterialData : UIPanelData
	{
		public SubMiddleDownMaterialData()
		{
		}
	}
	
	public partial class SubMiddleDownMaterial
	{
		private SubMiddleDownMaterialData m_PanelData = null;
		
		private void AllocatePanelData(params object[] args)
		{
			 m_PanelData = UIPanelData.Allocate<SubMiddleDownMaterialData>();
		}
		
		private void ReleasePanelData()
		{
			ObjectPool<SubMiddleDownMaterialData>.S.Recycle(m_PanelData);
		}
		
		private void BindModelToUI()
		{
		}
		
		private void BindUIToModel()
		{
		}
		
	}
}
