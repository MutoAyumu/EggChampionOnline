using NCMB;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    int _level;
    NCMBObject _obj;
    [SerializeField]int _money;

    public int Level { get => _level;}
    public int Money { get => _money;}

    /* ToDo
     �X�y���~�X�p������������C������
    Date �� Deta
    
      �f�[�^�擾�̐����T�C�g
    https://blog.mbaas.nifcloud.com/entry/2021/09/17/185329#%E9%85%8D%E5%88%97%E5%9E%8B
     */

    protected override void OnAwake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
    private void Start()
    {
        _obj = new NCMBObject("UserDate");
        _obj.ObjectId = NCMBUser.CurrentUser["UserDateID"].ToString();

        LoadMoney();
    }

    /// <summary>
    /// �T�[�o�[�ɕۑ�����Ă��邨����ǂݍ���
    /// </summary>
    /// <returns></returns>
    public void LoadMoney()
    {
        _obj.FetchAsync((NCMBException e) =>
        {
            if (e != null)
            {
                Debug.LogError("�f�[�^�̃��[�h�Ɏ��s���܂���");
            }
            else
            {
                Debug.Log("�f�[�^�̃��[�h�ɐ������܂���");
                //var s = _obj["Money"].ToString();
                //_money = int.Parse(s);
                var m = (long)_obj["Money"];
                _money = (int)m;
            }
        });
    }

    /// <summary>
    /// �T�[�o�[�ɕۑ�����Ă��邨�����X�V����
    /// </summary>
    /// <param name="money"></param>
    public void SaveMoney(int money)
    {
        _obj.FetchAsync((NCMBException e) =>    //�ǂݍ����
        {
            if (e != null)
            {
                Debug.LogError("�f�[�^�̃��[�h�Ɏ��s���܂���");
            }
            else
            {
                Debug.Log("�f�[�^�̃��[�h�ɐ������܂���");

                //var s = _obj["Money"].ToString();
                //_money = int.Parse(s);
                var m = (long)_obj["Money"];
                _money = (int)m;
                _money -= money;
                _obj["Money"] = _money;

                _obj.SaveAsync((NCMBException e) =>     //�ۑ�����
                {
                    if (e != null)
                    {
                        Debug.LogError("���[�U�[�f�[�^�̃Z�[�u�Ɏ��s���܂���");
                    }
                    else
                    {
                        Debug.Log("���[�U�[�f�[�^�̃Z�[�u�ɐ������܂���");
                    }
                });
            }
        });
    }

    /// <summary>
    /// �T�[�o�[�ɕۑ�����Ă��郌�x���f�[�^��ǂݍ���
    /// </summary>
    /// <param name="i"></param>
    /// <returns></returns>
    public void LoadLevelDate(int i)
    {
        _obj.FetchAsync((NCMBException e) =>
        {
            if (e != null)
            {
                Debug.LogError("�f�[�^�̃��[�h�Ɏ��s���܂���");
            }
            else
            {
                Debug.Log("�f�[�^�̃��[�h�ɐ������܂���");
                var levelDate = (ArrayList)_obj["LevelDate"];
                var l = (long)levelDate[i];
                _level = (int)l;
            }
        });
    }

    /// <summary>
    /// �T�[�o�[�ɕۑ�����Ă��郌�x���f�[�^���X�V
    /// </summary>
    /// <param name="i"></param>
    /// <param name="value"></param>
    public void SaveLevelDate(int i, int value)
    {
        _obj.FetchAsync((NCMBException e) =>
        {
            if(e != null)
            {
                Debug.LogError("�f�[�^�̃��[�h�Ɏ��s���܂���");
            }
            else
            {
                Debug.Log("�f�[�^�̃��[�h�ɐ������܂���");

                var levelDate = (ArrayList)_obj["LevelDate"];
                levelDate[i] = value;
                _obj["LevelDate"] = levelDate;

                _obj.SaveAsync((NCMBException e) =>
                {
                    if (e != null)
                    {
                        Debug.LogError("�f�[�^�̃Z�[�u�Ɏ��s���܂���");
                    }
                    else
                    {
                        Debug.Log("�f�[�^�̃Z�[�u�ɐ������܂���");
                    }
                });
            }
        });
    }
}
