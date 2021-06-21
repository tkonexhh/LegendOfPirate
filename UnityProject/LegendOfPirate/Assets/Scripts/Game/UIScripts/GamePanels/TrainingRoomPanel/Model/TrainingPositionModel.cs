namespace GameWish.Game
{
    public class TrainingPositionModel
    {
        public TrainingSlotModel trainingSlotModel;
        public TrainingPosition traPos;

        public TrainingPositionModel(TrainingSlotModel trainingSlotModel)
        {
            this.trainingSlotModel = trainingSlotModel;
        }

        public void SetTraPosData(TrainingPosition traPos)
        {
            this.traPos = traPos;
        }
    }
}