using System.Collections.Generic;
using UnityEngine;

namespace CustomEventSystem
{
    [CreateAssetMenu(menuName = "CustomEventSystem/CustomEvent")]
    public class CustomEvent : ScriptableObject
    {
        public List<CustomEventListener> listeners = new();
        
        // Raise event through different method signatures
        // ############################################################

        public void Raise()
        {
            Raise(null, null);
        }

        public void Raise(object data)
        {
            Raise(null, data);
        }

        public void Raise(Component sender)
        {
            Raise(sender, null);
        }

        public void Raise(Component sender, object data)
        {
            for (var i = listeners.Count - 1; i >= 0; i--) listeners[i].OnEventRaised(sender, data);
        }

        // Manage Listeners
        // ############################################################

        public void RegisterListener(CustomEventListener listener)
        {
            if (!listeners.Contains(listener))
                listeners.Add(listener);
        }

        public void UnregisterListener(CustomEventListener listener)
        {
            if (listeners.Contains(listener))
                listeners.Remove(listener);
        }
    }
}