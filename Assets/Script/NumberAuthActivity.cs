using UnityEngine;
using UnityEngine.UI;
using System.Threading.Tasks;

public class NumberAuthActivity : MonoBehaviour
{
    private const string TAG = "NumberAuthActivity";
    private PhoneNumberAuthHelper mPhoneNumberAuthHelper;
    private TokenResultListener mVerifyListener;
    public Button authButton;
    public InputField numberInputField;
    private string phoneNumber;
    private ProgressDialog mProgressDialog;

    private void Start()
    {
        authButton.onClick.AddListener(OnAuthButtonClick);
        SdkInit();
        AccelerateVerify(5000);
    }

    private void OnAuthButtonClick()
    {
        phoneNumber = numberInputField.text;
        // Check if the phone number is valid
        if (!string.IsNullOrEmpty(phoneNumber))
        {
            ShowLoadingDialog("正在进行本机号码校验");
            NumberAuth(5000);
        }
    }

    private void SdkInit()
    {
        mVerifyListener = new TokenResultListener
        {
            OnTokenSuccess = (s) =>
            {
                Debug.Log(TAG + ": 获取token成功：" + s);
                try
                {
                    TokenRet pTokenRet = TokenRet.FromJson(s);
                    if (ResultCode.CODE_SUCCESS.Equals(pTokenRet.GetCode()) && !string.IsNullOrEmpty(pTokenRet.GetToken()))
                    {
                        GetResultWithToken(pTokenRet.GetToken(), phoneNumber);
                    }
                    mPhoneNumberAuthHelper.SetAuthListener(null);
                }
                catch (System.Exception e)
                {
                    Debug.LogError(e);
                }
            },
            OnTokenFailed = (s) =>
            {
                Debug.Log(TAG + ": 获取token失败：" + s);
                HideLoadingDialog();
                // Handle failure (e.g., notify user)
            }
        };

        mPhoneNumberAuthHelper = PhoneNumberAuthHelper.GetInstance(Application.Context, mVerifyListener);
        mPhoneNumberAuthHelper.GetReporter().SetLoggerEnable(BuildConfig.NeedLogger);
    }

    public void AccelerateVerify(int timeout)
    {
        mPhoneNumberAuthHelper.AccelerateVerify(timeout, new PreLoginResultListener
        {
            OnTokenSuccess = (vendor) =>
            {
                Debug.Log(TAG + ": accelerateVerify：" + vendor);
            },
            OnTokenFailed = (vendor, errorMsg) =>
            {
                Debug.LogError(TAG + ": accelerateVerify：" + vendor + "， " + errorMsg);
            }
        });
    }

    public void NumberAuth(int timeout)
    {
        mPhoneNumberAuthHelper.SetAuthListener(mVerifyListener);
        mPhoneNumberAuthHelper.GetVerifyToken(timeout);
    }

    public void GetResultWithToken(string token, string phoneNumber)
    {
        Task.Run(() =>
        {
            string result = VerifyNumber(token, phoneNumber);
            UnityMainThreadDispatcher.Instance().Enqueue(() =>
            {
                HideLoadingDialog();
                // Handle result (e.g., pass it back to Unity)
            });
        });
    }

    public void ShowLoadingDialog(string hint)
    {
        if (mProgressDialog == null)
        {
            mProgressDialog = new ProgressDialog();
            mProgressDialog.SetProgressStyle(ProgressDialog.StyleSpinner);
        }
        mProgressDialog.SetMessage(hint);
        mProgressDialog.SetCancelable(true);
        mProgressDialog.Show();
    }

    public void HideLoadingDialog()
    {
        if (mProgressDialog != null)
        {
            mProgressDialog.Dismiss();
        }
    }
}
