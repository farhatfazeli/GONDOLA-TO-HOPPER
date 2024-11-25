using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int money;
    public int passengers;
    public int cargo;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }
    
}


// using System;
// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.UI;
//
// public class GameManager : MonoBehaviour
// {
//     public Image trainImage;
//     
//     public float trainSpeed = 1f;
//     public float trainSpeedIncrement = 0.1f;
//     public bool isTrainDispatched = false;
//     
//     public AudioSource trainSound;
//     
//     public void DispatchTrain()
//     {
//         isTrainDispatched = true;
//         trainSound.Play();
//     }
//
//     private void Update()
//     {
//         if (!isTrainDispatched)
//             return;
//         trainImage.transform.position += Vector3.right * (Time.deltaTime * trainSpeed);
//         trainSpeed += Time.deltaTime * trainSpeedIncrement;
//     }
// }