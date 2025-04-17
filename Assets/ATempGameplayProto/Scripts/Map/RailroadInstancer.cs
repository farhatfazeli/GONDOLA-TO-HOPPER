using System;
using System.Collections.Generic;
using UnityEngine;

public class RailroadInstancer : MonoBehaviour
{
    private readonly List<RailroadTrackPiece> _instances = new List<RailroadTrackPiece>();
    
    private int _currentIndex = 0;
    private int _previousIndex = 0;

    [Range(0, 1)]
    public float progressionAmount = 0.0f;
    private void Start()
    {
        _instances.AddRange(GetComponentsInChildren<RailroadTrackPiece>());
        
        // hide all to begin with
        foreach (var instance in _instances)
        {
            instance.gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        TransformProgressionAmountToListIndex(progressionAmount);
        ShowUntilIndex();
    }

    private void ShowUntilIndex()
    {
        if (_currentIndex == _previousIndex)
        {
            return;
        }
        if (_currentIndex > _previousIndex)
        {
            ShowFromPreviousToCurrent();
        }
        else
        {
            HideFromCurrentToPrevious();
        }
        _previousIndex = _currentIndex;
    }

    private void HideFromCurrentToPrevious()
    {
        for (int i = _currentIndex; i < _previousIndex; i++)
        {
            _instances[i].gameObject.SetActive(false);
        }
    }

    private void ShowFromPreviousToCurrent()
    {
        for (int i = _previousIndex; i < _currentIndex; i++)
        {
            _instances[i].gameObject.SetActive(true);
        }
    }

    private void TransformProgressionAmountToListIndex(float f)
    {
        // Calculate the index of the list based on the progression amount
        _currentIndex = Mathf.FloorToInt(f * _instances.Count);
        // Debug.Log(_currentIndex);
    }
}
