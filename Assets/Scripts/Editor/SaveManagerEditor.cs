using Core.Persistence;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    [CustomEditor(typeof(SaveManager))]
    public class SaveManagerEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            SaveManager saveManager = (SaveManager)target;

            if (GUILayout.Button("Save Game"))
            {
                saveManager.SaveGame();
            }
        
            if (GUILayout.Button("Load Game"))
            {
                saveManager.LoadGame();
            }
        }
    }
}