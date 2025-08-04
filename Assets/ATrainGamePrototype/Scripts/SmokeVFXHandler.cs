using System;
using ATrainGamePrototype.Scripts;
using UnityEngine;

public class SmokeVFXHandler : MonoBehaviour
{
    [SerializeField] private float maxObservableSpeed;
    [SerializeField] private float maxParticleForceX;
    
    private ParticleSystem _smokeVFX;

    private void Awake() {
        _smokeVFX = GetComponent<ParticleSystem>();
    }

    private void Update() {
        var forceOverLifetime = _smokeVFX.forceOverLifetime;
        forceOverLifetime.xMultiplier = RemapValues();
        Debug.Log("Force multiplier: " + forceOverLifetime.xMultiplier);
    }

    private float RemapValues() {
        float normalisedSpeed = Mathf.InverseLerp(0, maxObservableSpeed, TrainController.I.trainEngine.Speed);
        return Mathf.Lerp(0, maxParticleForceX, normalisedSpeed);
    }
}
