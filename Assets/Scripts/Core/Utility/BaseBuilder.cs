using System;
using TrainGame.Model.Progress;
using TrainGame.Model.RollingStock;

namespace Core.Utility
{
    public abstract class BuilderBase
    {
        protected readonly ProgressTracker constructionProgressTracker;
        public bool IsBuilt => constructionProgressTracker.IsFinished;
        public float BuildProgress => constructionProgressTracker.Progress;
        
        public event Action OnBuildComplete;

        protected readonly LoadType buildResourceType;
        protected readonly int buildResourceCost;
        protected readonly int baseBuildManualRate;

        protected BuilderBase(ConstructionProgress progress, int baseBuildAutoRate, LoadType buildResourceType, int buildResourceCost, int baseBuildManualRate)
        {
            constructionProgressTracker = new ProgressTracker(progress, baseBuildAutoRate);
            this.buildResourceType = buildResourceType;
            this.buildResourceCost = buildResourceCost;
            this.baseBuildManualRate = baseBuildManualRate;
        }

        public void StartBuild()
        {
            if (IsBuilt) return;
            constructionProgressTracker.Start();
        }

        public void ManualBuild()
        {
            if (IsBuilt) return;
            constructionProgressTracker.AdvanceBy(baseBuildManualRate);
        }

        public void Update(float deltaTime)
        {
            if (IsBuilt) return;
            constructionProgressTracker.Advance(deltaTime);
        }
        
        public void SetBuildProgress(float progress)
        {
            if (IsBuilt) return;
            constructionProgressTracker.SetProgress(progress);
        }

        public void SetBuilt()
        {
            constructionProgressTracker.Finish();
            OnBuildComplete?.Invoke();
        }

        public void Pause() => constructionProgressTracker.Pause();
        public void Resume() => constructionProgressTracker.Resume();
    }

}