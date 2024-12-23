using UnityEngine;

public class TokenResultListener : AndroidJavaProxy
{
    public TokenResultListener() : base("com.mobile.auth.gatewayauth.TokenResultListener") { }

    // 토큰 성공 시 콜백
    public void onTokenSuccess(string token)
    {
        Debug.Log("Token Success: " + token);
    }

    // 토큰 실패 시 콜백
    public void onTokenFailed(string error)
    {
        Debug.Log("Token Failed: " + error);
    }
}
