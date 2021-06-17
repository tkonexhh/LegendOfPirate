using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using Qarth;


namespace GameWish.Game
{
    public class MiddleSlotModel
    {
        public TrainingSlotModel trainingSlotModel;
        public MiddleTrainingRole middleTrainingRole;

        public MiddleSlotModel(TrainingSlotModel trainingSlotModel)
        {
            this.trainingSlotModel = trainingSlotModel;
        }

        public void SetTRoleData(MiddleTrainingRole middleTrainingRole)
        {
            this.middleTrainingRole = middleTrainingRole;
        }
    }
}