using UnityEngine;
using UnityEngine.UI;
using Qarth.Extension;
using Qarth;
using UniRx;
using GFrame;
using System;
using TMPro;
namespace GameWish.Game
{
	public class BuildingLevelUpPanelData : UIPanelData
	{
		public ShipUnitModel buidingModel;
		public BuildingLevelUpData levelUpData;
		public ShipUnitType unitType;
		
		public BuildingLevelUpPanelData()
		{

		}

    }
	
	public partial class BuildingLevelUpPanel
	{
		private BuildingLevelUpPanelData m_PanelData = null;
		
		private void AllocatePanelData(params object[] args)
		{
			
			 m_PanelData = UIPanelData.Allocate<BuildingLevelUpPanelData>();
			
        }

		private void SetPanelData(params object[] args) 
		{
            if (args != null && args.Length > 0)
            {
                m_PanelData.unitType = (ShipUnitType)args[0];
                m_PanelData.buidingModel = ModelMgr.S.GetModel<ShipModel>().GetShipUnitModel(m_PanelData.unitType);
			}
			BindModelToUI();
        }

        private void ReleasePanelData()
		{
			ObjectPool<BuildingLevelUpPanelData>.S.Recycle(m_PanelData);
		}
		
		private void BindModelToUI()
        {
			m_PanelData.buidingModel.level.AsObservable().Subscribe(level => OnLevelChange(level)).AddTo(this);
		}
		
		private void BindUIToModel()
		{

		}

		private void InitEventListener() 
		{
            m_CloseBtn.OnClickAsObservable().Subscribe(_ => HideSelfWithAnim()).AddTo(this);
            m_LevelUpBtn.OnClickAsObservable().Subscribe(_ => OnLevelUpBtnClick()).AddTo(this);
			m_MaterialsList.SetCellRenderer(OnMaterialsListChange);

        }

        private void OnMaterialsListChange(Transform root, int index)
        {
			var tmp = root.GetComponentInChildren<TextMeshProUGUI>();
			tmp.text = "0/"+ m_PanelData.levelUpData.needElementCounts[index].ToString();
			//TODO 设置材料Icon 接入材料仓库
			//root.GetComponentInChildren<GTextMeshProUGUI>().text = string.Format("0/{1}", m_PanelData.levelUpData.needElementCounts[index]);
		}

        private void OnLevelChange(int level) 
		{
			m_PanelData.levelUpData = BuildingLevelUpDataFactory.GetBuildingLevelUpData(m_PanelData.unitType);
			//TODO 设置升级前后Icon
			m_LevelCount.text = string.Format("lv.{0}→lv.{1}", level, level + 1);
			m_BuildingName.text = m_PanelData.levelUpData.buildingName;
			m_MaterialsList.SetDataCount(m_PanelData.levelUpData.needElementIds.Count);
			m_Title.text = m_PanelData.levelUpData.buildingName + " to level up";
		}
		private void OnLevelUpBtnClick() 
		{
#if UNITY_EDITOR
			m_PanelData.buidingModel.OnLevelUpgrade(1);
#endif
		}
		
    }

}
