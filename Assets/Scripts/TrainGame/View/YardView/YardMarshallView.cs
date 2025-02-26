using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ScriptableObjects;
using TrainGame.Model.RollingStock;
using TrainGame.Repositories;
using TrainGame.View.TrainView;
using UnityEngine;

namespace TrainGame.View.YardView
{
    public class YardMarshallView : MonoBehaviour
    {
        [Header("Transforms for Positioning")]
        [SerializeField] private Transform yardAnchorTransform;  // Where the first car should be
        [SerializeField] private Transform entranceTransform;     // Off-screen spawn point to the left

        private readonly float _driveSpeed = SO_GameParameters.I.driveSpeed;

        // Maintains references to the "currently displayed" rolling stock
        private readonly List<RollingStockModel> _currentModels = new List<RollingStockModel>();
        private readonly Dictionary<RollingStockModel, TrainView.RollingStockView> _modelToView = 
            new Dictionary<RollingStockModel, TrainView.RollingStockView>();

        /// <summary>
        /// Called by YardWorldView whenever the model's consist changes.
        /// We add/remove rolling stock views to match the new list,
        /// and handle incremental positioning.
        /// </summary>
        public void UpdateMarshalling(IReadOnlyList<RollingStockModel> newConsist)
        {
            // 1) Remove any rolling stock that is no longer in newConsist
            RemoveOldRollingStock(newConsist);

            // 2) Add any brand-new rolling stock that we don't currently have displayed
            AddNewRollingStock(newConsist);

            // 3) Re-align the final positions (especially if the order changed)
            AlignAllRollingStock(newConsist);

            // 4) Update our "current" references
            _currentModels.Clear();
            _currentModels.AddRange(newConsist);
        }

        /// <summary>
        /// For each RollingStockModel that is in _currentModels but not in newConsist,
        /// remove its view from the scene.
        /// </summary>
        private void RemoveOldRollingStock(IReadOnlyList<RollingStockModel> newConsist)
        {
            var removedModels = new List<RollingStockModel>();

            foreach (var oldModel in _currentModels)
            {
                if (!newConsist.Contains(oldModel))
                {
                    // Mark for removal
                    removedModels.Add(oldModel);
                }
            }

            // Now remove them and destroy the associated views
            foreach (var rm in removedModels)
            {
                if (_modelToView.TryGetValue(rm, out var view))
                {
                    Destroy(view.gameObject);
                    _modelToView.Remove(rm);
                }
            }
        }

        /// <summary>
        /// For each RollingStockModel in newConsist but not in _currentModels,
        /// instantiate a new RollingStockView off-screen.
        /// </summary>
        private void AddNewRollingStock(IReadOnlyList<RollingStockModel> newConsist)
        {
            foreach (var model in newConsist)
            {
                // If we already have this model displayed, skip
                if (_modelToView.ContainsKey(model)) 
                    continue;

                // Spawn a new view from the repository
                var prefab = RollingStockRepository.I.GetViewPrefab(model);
                var instance = Instantiate(prefab, entranceTransform.position, Quaternion.identity, transform);

                var rollingStockView = instance.GetComponent<RollingStockView>();
                rollingStockView.Initialize(model);

                _modelToView[model] = rollingStockView;
            }
        }

        /// <summary>
        /// Re-align all rolling stock in the order they appear in newConsist.
        /// The first one gets anchored to yardAnchorTransform; subsequent ones line up behind the previous.
        /// If a newly added rolling stock was placed off-screen, animate it in.
        /// </summary>
        private void AlignAllRollingStock(IReadOnlyList<RollingStockModel> newConsist)
        {
            RollingStockView previousView = null;

            for (int i = 0; i < newConsist.Count; i++)
            {
                var model = newConsist[i];
                RollingStockView view = _modelToView[model];

                // If it's the first in the list:
                if (i == 0)
                {
                    // Move or animate it to yardAnchorTransform
                    StartCoroutine(DriveIn(view.transform, yardAnchorTransform.position));
                }
                else
                {
                    // Align behind the previous car
                    if (previousView != null)
                    {
                        StartCoroutine(DriveInBehind(view, previousView));
                    }
                }

                previousView = view;
            }
        }

        /// <summary>
        /// Smoothly moves a transform from current position to target position using a coroutine.
        /// </summary>
        private IEnumerator DriveIn(Transform item, Vector3 targetPos)
        {
            // If it's *already* near the anchor, skip the animation
            while ((item.position - targetPos).sqrMagnitude > 0.001f)
            {
                item.position = Vector3.MoveTowards(
                    item.position,
                    targetPos,
                    _driveSpeed * Time.deltaTime
                );
                yield return null;
            }
            item.position = targetPos; // finalize
        }

        /// <summary>
        /// Moves "newCar" so its front coupler lines up with "previousCar"'s rear coupler.
        /// </summary>
        private IEnumerator DriveInBehind(RollingStockView newCar, RollingStockView previousCar)
        {
            while (true)
            {
                Vector3 targetPos = CalculateCouplerAlignment(newCar, previousCar);
                newCar.transform.position = Vector3.MoveTowards(
                    newCar.transform.position,
                    targetPos,
                    _driveSpeed * Time.deltaTime
                );

                if ((newCar.transform.position - targetPos).sqrMagnitude < 0.001f)
                {
                    newCar.transform.position = targetPos;
                    break;
                }

                yield return null;
            }
        }

        /// <summary>
        /// The position where newCar's front coupler matches previousCar's rear coupler.
        /// </summary>
        private Vector3 CalculateCouplerAlignment(RollingStockView newCar, RollingStockView previousCar)
        {
            Vector3 prevRearPos = previousCar.rearCoupler.position;
            Vector3 newFrontPos = newCar.frontCoupler.position;

            // The offset from newCar's transform to its front coupler
            Vector3 offset = newFrontPos - newCar.transform.position;
            offset.y = 0; // ignore vertical offset

            // So we want newCar's transform such that front coupler == prevRearPos
            return prevRearPos - offset;
        }

        // BONUS: If you want a direct method to forcibly remove all rolling stock:
        public void ResetAllRollingStock()
        {
            // Clear the dictionary, destroy everything
            foreach (var kvp in _modelToView)
            {
                if (kvp.Value != null)
                    Destroy(kvp.Value.gameObject);
            }
            _modelToView.Clear();
            _currentModels.Clear();
        }
    }
}