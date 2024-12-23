using UnityEngine;

public class LoginToken : MonoBehaviour
{
    // 원클릭 로그인으로 토큰 얻기
    public void GetLoginToken()
    {
        using (AndroidJavaClass phoneAuthClass = new AndroidJavaClass("com.mobile.auth.gatewayauth.PhoneNumberAuthHelper"))
        {
            phoneAuthClass.CallStatic("getLoginToken", new object[] { /* 파라미터들 */ });
        }
    }
}
