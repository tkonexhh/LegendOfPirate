using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using Qarth;


namespace GameWish.Game
{
    public class PreparatorRoleModel
    {
        public int roleID;
        public BoolReactiveProperty isSelected;
        public TrainingSlotModel trainingSlotModel;
        public PreparatorRole prepRole;
        public TrainingRoomPanel trainingRoomPanel;

        public PreparatorRoleModel(TrainingSlotModel trainingSlotModel, TrainingRoomPanel trainingRoomPanel, bool selected)
        {
            this.trainingSlotModel = trainingSlotModel;
            this.trainingRoomPanel = trainingRoomPanel;
            this.isSelected = new BoolReactiveProperty(selected);
        }
        public PreparatorRoleModel(int roleID, TrainingRoomPanel trainingRoomPanel, bool selected)
        {
            this.roleID = roleID;
            this.trainingRoomPanel = trainingRoomPanel;
            this.isSelected = new BoolReactiveProperty(selected);
        }
        public void SetPrepRoleData(PreparatorRole prepRole)
        {
            this.prepRole = prepRole;
        }

      
    }

}