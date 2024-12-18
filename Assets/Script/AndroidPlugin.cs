using UnityEngine;

public class AndroidPlugin : MonoBehaviour
{
    void Start()
    {
        using (AndroidJavaClass myClass = new AndroidJavaClass("com.example.MyJavaClass"))
        {
            string result = myClass.CallStatic<string>("getStringFromJava");
            Debug.Log("Received from Java: " + result);
        }
    }

    public void CallJavaMethod()
    {
        using (AndroidJavaClass myClass = new AndroidJavaClass("com.example.MyJavaClass"))
        {
            using (AndroidJavaObject myObject = myClass.CallStatic<AndroidJavaObject>("getInstance"))
            {
                int value = myObject.Call<int>("getIntValue");
                Debug.Log("Integer value from Java: " + value);
            }
        }
    }
}
