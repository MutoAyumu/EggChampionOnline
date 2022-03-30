using UnityEngine;
using TMPro;
using NCMB;

/// <summary>
/// ユーザー情報の登録・取得などをする
/// </summary>
public class TitleManager : MonoBehaviour
{
    [SerializeField] TMP_InputField _nameField = default;
    [SerializeField] TMP_InputField _passWordField = default;

    [SerializeField] int _startMoney = 100;

    bool isFirst;

    /* ToDo
             
     */

    /// <summary>
    /// ログイン用の関数
    /// </summary>
    public void Login()
    {
        var user = new NCMBUser();

        NCMBUser.LogInAsync(_nameField.text, _passWordField.text, (NCMBException e) =>
         {
             if (e != null)
             {
                 Debug.LogError("ログイン失敗 " + e.ErrorMessage);
             }
             else
             {
                 Debug.Log("ログイン成功");

                 if(isFirst)
                 {
                     SetStartParam();
                 }

                 //シーンの遷移
             }
         });
    }
    /// <summary>
    /// サインイン用の関数
    /// </summary>
    public void Signin()
    {
        var user = new NCMBUser();

        user.UserName = _nameField.text;
        user.Password = _passWordField.text;

        user.SignUpAsync((NCMBException e) =>
        {
            if(e != null)
            {
                Debug.LogError("新規登録失敗 " + e.ErrorMessage);
            }
            else
            {
                Debug.Log("新規登録成功");

                isFirst = true;
                Login();
            }
        });
    }

    /// <summary>
    /// 初期設定をするための関数
    /// </summary>
    void SetStartParam()
    {
        var cu = NCMBUser.CurrentUser;

        var o = new NCMBObject("UserDate");
        o["Name"] = _nameField.text;
        o["Money"] = _startMoney;

        string ID;

        var acl = new NCMBACL();    //82行から90行でアクセス権限の制御

        acl.PublicReadAccess = true;
        acl.PublicWriteAccess = false;  //全員が書き込み出来ないようにする

        acl.SetReadAccess(cu.ObjectId, true);
        acl.SetWriteAccess(cu.ObjectId, true);  //自分だけ書き込みが出来るようにする

        o.ACL = acl;

        o.SaveAsync((NCMBException e) =>
        {
            if(e != null)
            {
                Debug.LogError("UserDateの保存に失敗");
            }
            else
            {
                ID = o.ObjectId;
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
