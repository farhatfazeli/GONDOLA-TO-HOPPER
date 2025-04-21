using System;
using System.ComponentModel;
using TrainGame.Model.Progress;
using TrainGame.Model.RollingStock;
using TrainGame.Model.Station;
using UnityEngine;

namespace Core.Utility
{
    public enum BuildState
    {
        Built,
        UnderConstruction,
        NotBuilt,
        NotAvailableForBuilding
    }
    
    public abstract class BuilderBase
    {
        protected readonly ProgressTracker constructionProgressTracker;
        public bool IsBuilt => constructionProgressTracker.IsFinished;
        public float BuildProgress => constructionProgressTracker.Progress;
        
        public event Action OnBuildComplete;

        protected readonly LoadType buildResourceType;
        protected readonly int buildResourceCost;
        protected readonly int baseBuildManualRate;

        protected BuilderBase(int maxBuildPoints, int baseBuildAutoRate, LoadType buildResourceType, int buildResourceCost, int baseBuildManualRate)
        {
            ConstructionProgress constructionProgress = new ConstructionProgress(maxBuildPoints);
            constructionProgressTracker = new ProgressTracker(constructionProgress, baseBuildAutoRate);
            this.buildResourceType = buildResourceType;
            this.buildResourceCost = buildResourceCost;
            this.baseBuildManualRate = baseBuildManualRate;
        }

        protected BuildState GetBuildState()
        {
            return BuildProgress switch
            {
                >= 1f => BuildState.Built,
                >  0f => BuildState.UnderConstruction,
                0f    => BuildState.NotBuilt,  // exact match
                _     => throw new InvalidEnumArgumentException("Invalid build progress")
            };
        }

        public void StartBuild()
        {
            if (IsBuilt) return;
            constructionProgressTracker.StartAuto();
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
            constructionProgressTracker.ForceFinish();
            OnBuildComplete?.Invoke();
        }

        public void Pause() => constructionProgressTracker.Pause();
        public void Resume() => constructionProgressTracker.Resume();
    }

}