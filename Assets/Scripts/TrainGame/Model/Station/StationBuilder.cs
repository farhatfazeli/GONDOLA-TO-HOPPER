using System;
using ScriptableObjects;
using TrainGame.Model.RollingStock;

namespace TrainGame.Model.Station
{
    public class StationBuilder
    {
        private readonly ProgressTracker _constructionProgressTracker;
        public bool IsBuilt => _constructionProgressTracker.IsFinished;
        public event Action OnBuildComplete;

        
        private readonly LoadType _buildResourceType;
        private readonly int _buildResourceCost;
        private readonly int _baseBuildManualRate;

        public StationBuilder(SO_Station station)
        {
            _constructionProgressTracker = new ProgressTracker(new ConstructionProgress(station.maxBuildPoints), station.baseBuildAutoRate);
            if (station.isBuiltAtStart) _constructionProgressTracker.Finish();
            _buildResourceType = station.buildResourceType;
            _buildResourceCost = station.buildResourceCost;
            _baseBuildManualRate = station.baseBuildManualRate;
        }

        public void StartBuild()
        {
            if (IsBuilt) return;
            _constructionProgressTracker.Start();
        }

        public void ManualBuild()
        {
            if (IsBuilt) return;
            _constructionProgressTracker.AdvanceBy(_baseBuildManualRate);
        }

        public void Update(float deltaTime)
        {
            if (IsBuilt) return;
            _constructionProgressTracker.Advance(deltaTime);
        }
        
        public void Pause()
        {
            _constructionProgressTracker.Pause();
        }
        
        public void Resume()
        {
            _constructionProgressTracker.Resume();
        }
    }
}