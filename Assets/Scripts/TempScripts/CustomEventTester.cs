using CustomEventSystem;
using UnityEngine;

public class CustomEventTester : MonoBehaviour
{
    public CustomEvent eventToTest;


    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            eventToTest.Raise();
            Debug.Log("Event tested");
        }
    }
}
