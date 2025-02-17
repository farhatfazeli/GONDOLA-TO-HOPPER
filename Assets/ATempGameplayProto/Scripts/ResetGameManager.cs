// using ScriptableObjects;
// using Train;
// using UnityEngine;
//
// public class ResetGameManager : MonoBehaviour
// {
//     public void ResetGame()
//     {
//         ResetResearch();
//     }
//
//     private void ResetResearch()
//     {
//         Locomotive[] loadedLocomotives = Resources.LoadAll<Locomotive>(SO_GameParameters.I.locomotivesFolderPathShort);
//         foreach (var locomotive in loadedLocomotives)
//         {
//             locomotive.Reset();
//         }
//         
//         Wagon[] loadedWagons = Resources.LoadAll<Wagon>(SO_GameParameters.I.wagonsFolderPathShort);
//         foreach (var wagon in loadedWagons)
//         {
//             wagon.Reset();
//         }
//         
//         TrainController.Instance.Reset();
//         
//     }
// }
