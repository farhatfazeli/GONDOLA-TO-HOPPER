using UnityEngine;
using System.Collections.Generic;

namespace CustomEventSystem
{
    public class CustomEventManager : MonoBehaviour
    {
        private static CustomEventManager _instance;
        
        public static CustomEventManager Instance => _instance;

        // Dictionary to hold the names and references of the loaded CustomEvents
        public Dictionary<string, CustomEvent> List;

        // Folder in Resources where the CustomEvents are stored
        public string resourcesFolder = "CustomEvents";

        void Awake()
        {
            // Ensure only one instance of CustomEventLoader exists
            if (_instance != null && _instance != this)
            {
                Destroy(gameObject);
                return;
            }

            // Set the instance to this
            _instance = this;

            // Make this GameObject persistent across scenes
            DontDestroyOnLoad(gameObject);

            // Initialize the dictionary
            List = new Dictionary<string, CustomEvent>();

            // Load all CustomEvents from the specified folder in Resources
            LoadCustomEvents();
        }

        private void LoadCustomEvents()
        {
            // Load all CustomEvents at the specified path within Resources
            Object[] objects = Resources.LoadAll(resourcesFolder, typeof(CustomEvent));

            foreach (CustomEvent customEvent in objects)
            {
                if (customEvent != null)
                {
                    // Use the name of the CustomEvent as the key
                    List.Add(customEvent.name, customEvent);
                    Debug.Log("Loaded CustomEvent: " + customEvent.name);
                }
                else
                {
                    Debug.LogError("Failed to load a CustomEvent from the Resources folder.");
                }
            }
        }
    }
}
