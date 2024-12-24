using UnityEngine;

public class AliyunProcess : MonoBehaviour
{
    private AndroidJavaObject phoneNumberAuthHelper;
    private string accessKeyId;
    private string accessKeySecret;
    //[SerializeField] private string templateCode = "SMS_139870006";
    //[SerializeField] private string signName = "alibabatest";

    private void Start()
    {
        try
        {
            AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            AndroidJavaObject currentActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");

            phoneNumberAuthHelper = new AndroidJavaObject(
                "com.mobile.auth.gatewayauth.PhoneNumberAuthHelper",
                currentActivity,
                new TokenResultListener()
            );

            var reporter = phoneNumberAuthHelper.Call<AndroidJavaObject>("getReporter");
            if (reporter != null)
            {
                reporter.Call("setLoggerEnable", true);
                Debug.Log("Logger enabled successfully.");
            }
            else
            {
                Debug.LogError("Reporter object is null.");
            }
        }
        catch (System.Exception ex)
        {
            Debug.LogError("Error initializing PhoneNumberAuthHelper: " + ex.Message);
        }
    }

    private class TokenResultListener : AndroidJavaProxy
    {
        public TokenResultListener() : base("com.mobile.auth.gatewayauth.TokenResultListener") { }

        public void onTokenSuccess(string token)
        {
            Debug.Log("Token Success: " + token);
        }

        public void onTokenFailed(string error)
        {
            Debug.LogError("Token Failed: " + error);
        }
    }
}