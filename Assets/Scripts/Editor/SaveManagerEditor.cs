using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(SaveManager))]
public class SaveManagerEditor : Editor
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