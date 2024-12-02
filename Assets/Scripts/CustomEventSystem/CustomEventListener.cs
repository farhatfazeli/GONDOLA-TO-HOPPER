using UnityEngine;
using UnityEngine.Events;

namespace CustomEventSystem
{
    public class CustomEventListener : MonoBehaviour
    {
        [Tooltip("Event to register with.")] public CustomEvent customEvent;

        [Tooltip("Response to invoke when Event with GameData is raised.")]
        public CustomResponse response;

        private void OnEnable()
        {
            if (customEvent != null)
            {
                customEvent.RegisterListener(this);
            }
        }

        private void OnDisable()
        {
            if (customEvent == null)
            {
                Debug.LogError("CustomEvent reference not set on " + gameObject.name);
                return;
            }

            Debug.Log("Unregistering CustomEvent listeners from " + gameObject.name);
            customEvent.UnregisterListener(this);
        }

        public void OnEventRaised(Component sender, object data)
        {
            if (response == null)
            {
                Debug.LogError("Response is null in " + gameObject.name);
                return;
            }

            response.Invoke(sender, data);
        }
    }
}