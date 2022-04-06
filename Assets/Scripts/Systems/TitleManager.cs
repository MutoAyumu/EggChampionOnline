using UnityEngine;
using TMPro;
using NCMB;
using UnityEngine.SceneManagement;

/// <summary>
/// ユーザー情報の登録・取得などをする
/// </summary>
public class TitleManager : MonoBehaviour
{
    [Header("Login")]
    [SerializeField] TMP_InputField _nameField = default;
    [SerializeField] TMP_InputField _passWordField = default;
    [Header("Signin")]
    [SerializeField] TMP_InputField _newNameField = default;
    [SerializeField] TMP_InputField _newPassWordField = default;
    [Header("Error")]
    [SerializeField] GameObject _loginErrorPanel = default;
    [SerializeField] GameObject _siginErrorPanel = default;
    [Header("Status")]
    [SerializeField] int _startMoney = 100;
    [SerializeField] string _menuSceneName = "MenuScene";


    bool isFirst;

    /* ToDo
             
     */

    /// <summary>
    /// ログイン用の関数
    /// </summary>
    public void OnLogin()
    {
        if (!string.IsNullOrEmpty(_nameField.text) && !string.IsNullOrEmpty(_passWordField.text))
        {
            Login(_nameField, _passWordField);
        }
    }

    /// <summary>
    /// サインイン用の関数
    /// </summary>
    public void OnSignin()
    {
        if (!string.IsNullOrEmpty(_newNameField.text) && !string.IsNullOrEmpty(_newPassWordField.text))
        {
            Signin();
        }
    }

    void Login(TMP_InputField name, TMP_InputField pass)
    {
        var user = new NCMBUser();

        NCMBUser.LogInAsync(name.text, pass.text, (NCMBException e) =>
         {
             if (e != null)
             {
                 Debug.LogError("ログイン失敗 " + e.ErrorMessage);
                 _loginErrorPanel.SetActive(true);
             }
             else
             {
                 Debug.Log("ログイン成功");

                 if(isFirst)
                 {
                     SetStartParam();
                 }

                 //シーンの遷移
                 Debug.Log("シーンの遷移が行われました");
                 SceneLoad.Instance.LoadScene(_menuSceneName);
             }
         });
    }
    
    void Signin()
    {
        var user = new NCMBUser();

        user.UserName = _newNameField.text;
        user.Password = _newPassWordField.text;

        user.SignUpAsync((NCMBException e) =>
        {
            if (e != null)
            {
                Debug.LogError("新規登録失敗 " + e.ErrorMessage);
                _siginErrorPanel.SetActive(true);
            }
            else
            {
                Debug.Log("新規登録成功");

                isFirst = true;
                Login(_newNameField, _newPassWordField);
            }
        });
    }

    /// <summary>
    /// 初期設定をするための関数
    /// </summary>
    void SetStartParam()
    {
        var cu = NCMBUser.CurrentUser;

        var obj = new NCMBObject("UserDate");
        obj["Name"] = _newNameField.text;
        obj["PassWord"] = _newPassWordField.text;
        obj["Money"] = _startMoney;

        string ID;

        var acl = new NCMBACL();    //82行から90行でアクセス権限の制御

        acl.PublicReadAccess = true;
        acl.PublicWriteAccess = false;  //全員が書き込み出来ないようにする

        acl.SetReadAccess(cu.ObjectId, true);
        acl.SetWriteAccess(cu.ObjectId, true);  //自分だけ書き込みが出来るようにする

        obj.ACL = acl;

        obj.SaveAsync((NCMBException e) =>
        {
            if(e != null)
            {
                Debug.LogError("UserDateの保存に失敗");
            }
            else
            {
                ID = obj.ObjectId;
                cu["UserDateID"] = ID;

                cu.SaveAsync((NCMBException ee) =>
                {
                    if(ee != null)
                    {
                        Debug.LogError("ユーザーID設定失敗");
                    }
                    else
                    {
                        Debug.Log("ユーザーID設定成功");
                    }
                });
            }
        });
    }
}
