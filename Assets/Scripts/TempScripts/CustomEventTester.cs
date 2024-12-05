using CustomEventSystem;
using UnityEngine;

namespace TempScripts
{
    public class CustomEventTester : MonoBehaviour
    {
        public CustomEvent firstEventToTest;
        public CustomEvent secondEventToTest;
        public CustomEvent thirdEventToTest;

        private int _counter = 1;

        private void Update()
        {
            if (!Input.GetKeyDown(KeyCode.Space)) return;
            switch (_counter)
            {
                case 1:
                    firstEventToTest.Raise();
                    Debug.Log("First event raised");
                    _counter++;
                    break;
                case 2:
                    secondEventToTest.Raise();
                    Debug.Log("Second event raised");
                    _counter++;
                    break;
                case 3:
                    thirdEventToTest.Raise();
                    Debug.Log("Third event raised");
                    break;
                default:
                    Debug.Log("No more events to raise");
                    break;
            }
        }
    }
}