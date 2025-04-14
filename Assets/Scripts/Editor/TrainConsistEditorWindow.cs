using System.Collections.Generic;
using ScriptableObjects;
using TrainGame.Model.RollingStock;
using TrainGame.Model.TrainConsist;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    public class TrainConsistEditorWindow : EditorWindow
    {
        private List<SO_RollingStock> rollingStockList = new();
        private string trainName = "New Train";
        private Vector2 scroll;

        private TrainConsistModelFactory factory = new();
    
        [MenuItem("Tools/Train Consist Builder")]
        public static void ShowWindow()
        {
            GetWindow<TrainConsistEditorWindow>("Train Consist Builder");
        }

        private void OnGUI()
        {
            GUILayout.Label("Train Consist Builder", EditorStyles.boldLabel);

            trainName = EditorGUILayout.TextField("Train Name", trainName);

            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Rolling Stock List", EditorStyles.boldLabel);

            scroll = EditorGUILayout.BeginScrollView(scroll, GUILayout.Height(200));
            for (int i = 0; i < rollingStockList.Count; i++)
            {
                EditorGUILayout.BeginHorizontal();

                rollingStockList[i] = (SO_RollingStock)EditorGUILayout.ObjectField(rollingStockList[i], typeof(SO_RollingStock), false);

                if (GUILayout.Button("Remove", GUILayout.Width(60)))
                {
                    rollingStockList.RemoveAt(i);
                    break;
                }

                EditorGUILayout.EndHorizontal();
            }
            EditorGUILayout.EndScrollView();

            EditorGUILayout.Space();
            if (GUILayout.Button("Add Empty Slot"))
            {
                rollingStockList.Add(null);
            }

            EditorGUILayout.Space(10);

            if (GUILayout.Button("Create Train Model"))
            {
                try
                {
                    var converted = ConvertToModelList(rollingStockList);
                    var train = factory.CreateTrainConsist(trainName, converted);
                    Debug.Log($"Train '{trainName}' created with {converted.Count} elements.");
                }
                catch (System.Exception e)
                {
                    Debug.LogError($"Failed to create train: {e.Message}");
                }
            }
        }

        private List<RollingStockModel> ConvertToModelList(List<SO_RollingStock> soList)
        {
            var modelList = new List<RollingStockModel>();
            factory = new TrainConsistModelFactory(); // Reset state (important for _hasLocomotive flag)

            foreach (var t in soList)
            {
                if (!t)
                    throw new System.Exception("Null rolling stock in list");
                
                RollingStockModel model = RollingStockRepository.I.GetModel(t);

                factory.AddRollinStock(model);
                modelList.Add(model);
            }

            return modelList;
        }
    }
}
