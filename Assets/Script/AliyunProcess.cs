using UnityEngine;

public class AliyunProcess : MonoBehaviour
{
    private AndroidJavaObject authHelper;

    void Start()
    {
        // Unity의 Main Activity 가져오기
        AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject unityActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");

        // PhoneNumberAuthHelper 인스턴스 생성
        AndroidJavaClass phoneAuthHelperClass = new AndroidJavaClass("com.aliyun.phone.PhoneNumberAuthHelper");
        authHelper = phoneAuthHelperClass.CallStatic<AndroidJavaObject>("getInstance", unityActivity, new TokenResultListener());

        // SDK 설정
        string secretInfo = "사용자 비밀 키";
        authHelper.Call("setAuthSDKInfo", secretInfo);
        authHelper.Call("checkEnvAvailable", 1); // 1은 PhoneNumberAuthHelper.SERVICE_TYPE_LOGIN 상수
    }

    public void AccelerateLoginPage(int timeout)
    {
        // 로그인 페이지 가속
        authHelper.Call("accelerateLoginPage", timeout, new PreLoginResultListener());
    }

    public void ShowLoginPage(int timeout)
    {
        // 로그인 페이지 표시 및 Token 요청
        authHelper.Call("getLoginToken", new TokenResultListener(), timeout);
    }

    // Java의 TokenResultListener 구현
    private class TokenResultListener : AndroidJavaProxy
    {
        public TokenResultListener() : base("com.aliyun.phone.TokenResultListener") { }

        public void onTokenSuccess(string token)
        {
            Debug.Log("Token Success: " + token);
        }

        public void onTokenFailed(string error)
        {
            Debug.LogError("Token Failed: " + error);
        }
    }

    // Java의 PreLoginResultListener 구현
    private class PreLoginResultListener : AndroidJavaProxy
    {
        public PreLoginResultListener() : base("com.aliyun.phone.PreLoginResultListener") { }

        public void onTokenSuccess(string result)
        {
            Debug.Log("PreLogin Success: " + result);
        }

        public void onTokenFailed(string code, string message)
        {
            Debug.LogError($"PreLogin Failed: {code}, {message}");
        }
    }
}
