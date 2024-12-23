using UnityEngine;

public class AuthSDKManager : MonoBehaviour
{
    private AndroidJavaObject authHelper;

    // 인증 인스턴스를 가져오는 함수
    public void InitializeAuthSDK(string secretInfo, AndroidJavaProxy tokenResultListener)
    {
        using (AndroidJavaClass unityPlayerClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
        {
            AndroidJavaObject currentActivity = unityPlayerClass.GetStatic<AndroidJavaObject>("currentActivity");

            using (AndroidJavaClass authHelperClass = new AndroidJavaClass("com.mobile.auth.gatewayauth.PhoneNumberAuthHelper"))
            {
                authHelper = authHelperClass.CallStatic<AndroidJavaObject>("getInstance", currentActivity, tokenResultListener);
                authHelper.Call("setAuthSDKInfo", secretInfo); // SDK 키 설정
            }
        }
    }

    // SDK 키를 설정하는 메서드 (필요시 호출)
    public void SetAuthSDKInfo(string secretInfo)
    {
        if (authHelper != null)
        {
            authHelper.Call("setAuthSDKInfo", secretInfo);  // SDK 키 설정
        }
    }
}
