using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using Qarth;


namespace GameWish.Game
{
    public class TrainingPreparatorRoleModel
    {
        public int roleID;
        public BoolReactiveProperty isSelected;
        public TrainingSlotModel trainingSlotModel;
        public TrainingPreparatorRole traPrepRole;
        public TrainingRoomPanel trainingRoomPanel;

        public TrainingPreparatorRoleModel(TrainingSlotModel trainingSlotModel, TrainingRoomPanel trainingRoomPanel, bool selected)
        {
            this.trainingSlotModel = trainingSlotModel;
            this.trainingRoomPanel = trainingRoomPanel;
            this.isSelected = new BoolReactiveProperty(selected);
        }
        public TrainingPreparatorRoleModel(int roleID, TrainingRoomPanel trainingRoomPanel, bool selected)
        {
            this.roleID = roleID;
            this.trainingRoomPanel = trainingRoomPanel;
            this.isSelected = new BoolReactiveProperty(selected);
        }
        public void SetPrepRoleData(TrainingPreparatorRole prepRole)
        {
            this.traPrepRole = prepRole;
        }

      
    }

}