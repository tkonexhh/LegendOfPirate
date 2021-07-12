using UniRx;

namespace GameWish.Game
{
    public class TrainingPositionModel
    {
        private TrainingSlotModel m_TrainingSlotModel;
        private TrainingPosition m_TraPos;

        public IReadOnlyReactiveProperty<string> trainingCountDown;
        public IReadOnlyReactiveProperty<bool> isHaveRole;
        public IntReactiveProperty unlockLevel;

        #region Public
        public TrainingPositionModel(TrainingSlotModel trainingSlotModel)
        {
            this.m_TrainingSlotModel = trainingSlotModel;

            trainingCountDown = m_TrainingSlotModel.trainRemainTime.Select(val => CommonMethod.SplicingTime((int)val)).ToReactiveProperty();
            isHaveRole = m_TrainingSlotModel.heroId.Select(val => val != -1).ToReactiveProperty();
            unlockLevel = new IntReactiveProperty(m_TrainingSlotModel.slotIDAndUnlockLevel);
        }

        public void SetTraPosData(TrainingPosition traPos)
        {
            this.m_TraPos = traPos;
        }

        public ReactiveProperty<TrainingSlotState> GetTrainingSlotState()
        {
            return m_TrainingSlotModel.trainState;
        }
        #endregion
    }
}