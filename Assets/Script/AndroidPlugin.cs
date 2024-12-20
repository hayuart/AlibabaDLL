using UnityEngine;

public class AndroidPlugin : MonoBehaviour
{
    void Start()
    {
        using (AndroidJavaClass myClass = new AndroidJavaClass("com.AgAlibtest.AlibabaDL"))
        {
            string result = myClass.CallStatic<string>("getStringFromJava");
            Debug.Log("Received from Java: " + result);
        }
    }
}
