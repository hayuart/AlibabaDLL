using UnityEngine;

public class AndroidPlugin : MonoBehaviour
{    public void StartAuthProcess()
    {
        using (AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
        {
            AndroidJavaObject activity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
            using (AndroidJavaClass pluginClass = new AndroidJavaClass("com.example.unityplugin.AuthActivity"))
            {
                pluginClass.CallStatic("startAuthActivity", activity);
            }
        }
    }

    public void OnAuthSuccess(string token)
    {
        Debug.Log("Auth Success: " + token);
    }

    public void OnAuthFailed(string error)
    {
        Debug.LogError("Auth Failed: " + error);
    }
}
