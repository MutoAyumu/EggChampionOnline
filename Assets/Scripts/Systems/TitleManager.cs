using UnityEngine;
using TMPro;
using NCMB;
using UnityEngine.SceneManagement;

/// <summary>
/// ���[�U�[���̓o�^�E�擾�Ȃǂ�����
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
    /// ���O�C���p�̊֐�
    /// </summary>
    public void OnLogin()
    {
        if (!string.IsNullOrEmpty(_nameField.text) && !string.IsNullOrEmpty(_passWordField.text))
        {
            Login(_nameField, _passWordField);
        }
    }

    /// <summary>
    /// �T�C���C���p�̊֐�
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
                 Debug.LogError("���O�C�����s " + e.ErrorMessage);
                 _loginErrorPanel.SetActive(true);
             }
             else
             {
                 Debug.Log("���O�C������");

                 if(isFirst)
                 {
                     SetStartParam();
                 }

                 //�V�[���̑J��
                 Debug.Log("�V�[���̑J�ڂ��s���܂���");
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
                Debug.LogError("�V�K�o�^���s " + e.ErrorMessage);
                _siginErrorPanel.SetActive(true);
            }
            else
            {
                Debug.Log("�V�K�o�^����");

                isFirst = true;
                Login(_newNameField, _newPassWordField);
            }
        });
    }

    /// <summary>
    /// �����ݒ�����邽�߂̊֐�
    /// </summary>
    void SetStartParam()
    {
        var cu = NCMBUser.CurrentUser;

        var obj = new NCMBObject("UserDate");
        obj["Name"] = _newNameField.text;
        obj["PassWord"] = _newPassWordField.text;
        obj["Money"] = _startMoney;

        string ID;

        var acl = new NCMBACL();    //82�s����90�s�ŃA�N�Z�X�����̐���

        acl.PublicReadAccess = true;
        acl.PublicWriteAccess = false;  //�S�����������ݏo���Ȃ��悤�ɂ���

        acl.SetReadAccess(cu.ObjectId, true);
        acl.SetWriteAccess(cu.ObjectId, true);  //���������������݂��o����悤�ɂ���

        obj.ACL = acl;

        obj.SaveAsync((NCMBException e) =>
        {
            if(e != null)
            {
                Debug.LogError("UserDate�̕ۑ��Ɏ��s");
            }
            else
            {
                ID = obj.ObjectId;
                cu["UserDateID"] = ID;

                cu.SaveAsync((NCMBException ee) =>
                {
                    if(ee != null)
                    {
                        Debug.LogError("���[�U�[ID�ݒ莸�s");
                    }
                    else
                    {
                        Debug.Log("���[�U�[ID�ݒ萬��");
                    }
                });
            }
        });
    }
}
