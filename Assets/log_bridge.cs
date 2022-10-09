using InstantGamesBridge;
using UnityEngine;

public class log_bridge : MonoBehaviour
{
private void Start()
{
    Debug.Log("Hello world");
    Bridge.Initialize(success =>
    {
        if (success)
        {
           Debug.Log("Initializate");
        }
        else
        {
            Debug.Log("Error");
        }
    });
}
}