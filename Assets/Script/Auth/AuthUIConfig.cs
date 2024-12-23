using UnityEngine;

public class AuthUIConfig : MonoBehaviour
{
    // 인증 페이지 테마 수정
    public void SetAuthUIConfig()
    {
        using (AndroidJavaClass phoneAuthClass = new AndroidJavaClass("com.mobile.auth.gatewayauth.PhoneNumberAuthHelper"))
        {
            phoneAuthClass.CallStatic("setAuthUIConfig", new object[] { /* 파라미터들 */ });
        }
    }
}
