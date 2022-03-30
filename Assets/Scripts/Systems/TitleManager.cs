using UnityEngine;
using TMPro;
using NCMB;

/// <summary>
/// ���[�U�[���̓o�^�E�擾�Ȃǂ�����
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
    /// ���O�C���p�̊֐�
    /// </summary>
    public void Login()
    {
        var user = new NCMBUser();

        NCMBUser.LogInAsync(_nameField.text, _passWordField.text, (NCMBException e) =>
         {
             if (e != null)
             {
                 Debug.LogError("���O�C�����s " + e.ErrorMessage);
             }
             else
             {
                 Debug.Log("���O�C������");

                 if(isFirst)
                 {
                     SetStartParam();
                 }

                 //�V�[���̑J��
             }
         });
    }
    /// <summary>
    /// �T�C���C���p�̊֐�
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
                Debug.LogError("�V�K�o�^���s " + e.ErrorMessage);
            }
            else
            {
                Debug.Log("�V�K�o�^����");

                isFirst = true;
                Login();
            }
        });
    }

    /// <summary>
    /// �����ݒ�����邽�߂̊֐�
    /// </summary>
    void SetStartParam()
    {
        var cu = NCMBUser.CurrentUser;

        var o = new NCMBObject("UserDate");
        o["Name"] = _nameField.text;
        o["Money"] = _startMoney;

        string ID;

        var acl = new NCMBACL();    //82�s����90�s�ŃA�N�Z�X�����̐���

        acl.PublicReadAccess = true;
        acl.PublicWriteAccess = false;  //�S�����������ݏo���Ȃ��悤�ɂ���

        acl.SetReadAccess(cu.ObjectId, true);
        acl.SetWriteAccess(cu.ObjectId, true);  //���������������݂��o����悤�ɂ���

        o.ACL = acl;

        o.SaveAsync((NCMBException e) =>
        {
            if(e != null)
            {
                Debug.LogError("UserDate�̕ۑ��Ɏ��s");
            }
            else
            {
                ID = o.ObjectId;
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
