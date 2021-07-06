using UnityEngine;
using UnityEngine.UI;
using Qarth.Extension;
using Qarth;
using UniRx;
using System;

namespace GameWish.Game
{
    public class RoleEquipDetailsPanelData : UIPanelData
    {
        public int roleEquipID;
        public int roleID;
        public bool IsLocked;
        public EquipmentUnitConfig equipConfig;
        public RoleEquipModel roleEquipModel;
        public EquipmentType equipmentType;
        public RoleEquipDetailsPanelData()
        {

        }
    }

    public partial class RoleEquipDetailsPanel
    {
        private RoleEquipDetailsPanelData m_PanelData = null;

        private void AllocatePanelData()
        {
            m_PanelData = UIPanelData.Allocate<RoleEquipDetailsPanelData>();
        }

        private void ReleasePanelData()
        {
            ObjectPool<RoleEquipDetailsPanelData>.S.Recycle(m_PanelData);
        }

        private void BindModelToUI()
        {
            if (m_PanelData.roleEquipModel != null)
            {
                //≤‚ ‘
                m_PanelData.roleEquipModel.equipLevel.Subscribe(level=>OnLevelChange()).AddTo(this);
            }

        }

        private void OnLevelChange()
        {
           
            m_PanelData.equipConfig = m_PanelData.roleEquipModel.equipConfig;
            m_StrengthMaterials.SetDataCount(0);
            m_StrengthMaterials.SetDataCount(m_PanelData.equipConfig.equipStrengthenCosts.Length);

            
            //foreach (var item in m_PanelData.equipConfig.equipStrengthenCosts) 
            //{
            //    var cobj = Instantiate(m_MaterialItem);
            //    var materialCount = InvModel.GetItemCountByID(item.materialID);
              
            //    cobj.GetComponentInChildren<TextMeshProUGUI>().text=string.Format(materialCount>=item.materialCostNumber?"<color=green>{0}</color>/{1}": "<color=red>{0}</color>/{1}", materialCount,item.materialCostNumber);
            //    //TODO …Ë÷√≤ƒ¡œÕº∆¨
            //}

            string equipAttribute = string.Empty;
            foreach (var attributePair in m_PanelData.roleEquipModel.equipAttributeDic)
            {
                equipAttribute += (attributePair.Key.ToString() + attributePair.Value.ToString("f2") + "\n");
            }
            m_EquipAttribute.text = equipAttribute;
        }

        private void BindUIToModel()
        {
            
        }

    }
}
