using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using Qarth;


namespace GameWish.Game
{
    public class BottomRoleModel
    {
        public int roleID;
        public BoolReactiveProperty isSelected;
        public TrainingSlotModel trainingSlotModel;
        public BottomTrainingRole bottomTrainingRole;
        public TrainingRoomPanel trainingRoomPanel;

        public BottomRoleModel(TrainingSlotModel trainingSlotModel, TrainingRoomPanel trainingRoomPanel, bool selected)
        {
            this.trainingSlotModel = trainingSlotModel;
            this.trainingRoomPanel = trainingRoomPanel;
            this.isSelected = new BoolReactiveProperty(selected);
        }
        public BottomRoleModel(int roleID, TrainingRoomPanel trainingRoomPanel, bool selected)
        {
            this.roleID = roleID;
            this.trainingRoomPanel = trainingRoomPanel;
            this.isSelected = new BoolReactiveProperty(selected);
        }
        public void SetTRoleData(BottomTrainingRole bottomTrainingRole)
        {
            this.bottomTrainingRole = bottomTrainingRole;
        }

      
    }

}