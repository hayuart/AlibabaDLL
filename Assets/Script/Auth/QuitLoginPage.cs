using UnityEngine;

public class QuitLoginPage : MonoBehaviour
{
    // 로그인 페이지 종료
    public void QuitPage()
    {
        using (AndroidJavaClass phoneAuthClass = new AndroidJavaClass("com.mobile.auth.gatewayauth.PhoneNumberAuthHelper"))
        {
            phoneAuthClass.CallStatic("quitLoginPage");
        }
    }
}
