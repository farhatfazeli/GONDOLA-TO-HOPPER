using System;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Camera _camera;
    public Transform train;
    public float routeDistance;

    [Range(0, 1)]public float startPan = 0.1f;
    [Range(0, 1)]public float startLockPan = 0.3f;
    [Range(0, 1)]public float endLockPan = 0.7f;
    [Range(0, 1)]public float viewPortLockX = 0.5f;
    [Range(0, 1)]public float endPan = 0.9f;
    
    
    private void Awake()
    {
        _camera = GetComponent<Camera>();
        Vector3 offset = new Vector3(GetCameraTargetWorldPositionOffset(0).x, 0, 0);
        transform.position -= offset;
    }

    private void Update()
    {
        if (GetTrainTravelProgress() < startPan || GetTrainTravelProgress() > endPan)
        {
            return;
        }
        Pan();
    }

    private void Pan()
    {
        Vector3 offset = new Vector3(GetCameraTargetWorldPositionOffset(GetTargetViewportX()).x, 0, 0);
        transform.position -= offset;
        transform.position += train.position;
    }
    
    private Vector3 GetCameraTargetWorldPositionOffset(float targetViewportX)
    {
        return _camera.ViewportToWorldPoint(new Vector3(targetViewportX, 0, Mathf.Abs(train.position.z - _camera.transform.position.z)));
    }

    private float GetTargetViewportX()
    {
        if (GetTrainTravelProgress() < startLockPan)
        {
            return Mathf.Lerp(GetViewportXAtProgress(startLockPan), viewPortLockX, GetTrainTravelProgress()/startLockPan);
        }
        if (GetTrainTravelProgress() > endLockPan)
        {
            return Mathf.Lerp(GetViewportXAtProgress(endLockPan), 1, (GetTrainTravelProgress() - endLockPan) / (1 - endLockPan));
        }
        return viewPortLockX;
    }
    
    private float GetTrainTravelProgress()
    {
        return Mathf.Clamp01(train.position.x / routeDistance);
    }
    
    private float GetViewportXAtProgress(float progress)
    {
        return _camera.WorldToViewportPoint(new Vector3(progress * routeDistance, train.position.y, train.position.z)).x;
    }
}
