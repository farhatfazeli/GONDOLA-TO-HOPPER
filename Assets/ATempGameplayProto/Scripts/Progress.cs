using UnityEngine;

public class Progress
{
    public bool IsComplete => _currentAmount >= _targetAmount;
    public float Value => _currentAmount / _targetAmount;

    private readonly float _targetAmount;
    private float _currentAmount;

    public Progress(float targetAmount)
    {
        _targetAmount = (targetAmount > 0) ? targetAmount : 1;
    }
    
    public void UpdateProgress(float amount)
    {
        _currentAmount += amount;
    }
    
    public void UpdateProgressPercentage(float amount)
    {
        _currentAmount += _targetAmount * amount;
    }
}