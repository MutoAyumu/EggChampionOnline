using NCMB;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    int[] _levels;
    NCMBObject _obj;
    [SerializeField]int _money;

    public int[] Levels { get => _levels;}
    public int Money { get => _money;}

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
                var s = _obj["Money"].ToString();
                _money = int.Parse(s);
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

                var s = _obj["Money"].ToString();
                _money = int.Parse(s);
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
    public int LoadLevelDate(int i)
    {
        var levelDate = (int[])_obj["LevelDate"];
        var level = levelDate[i];

        _obj.FetchAsync((NCMBException e) =>
        {
            if (e != null)
            {
                Debug.LogError("�f�[�^�̃��[�h�Ɏ��s���܂���");
            }
            else
            {
                Debug.Log("�f�[�^�̃��[�h�ɐ������܂���");
            }
        });

        return level;
    }

    /// <summary>
    /// �T�[�o�[�ɕۑ�����Ă��郌�x���f�[�^���X�V
    /// </summary>
    /// <param name="i"></param>
    /// <param name="value"></param>
    public void SaveLevelDate(int i, int value)
    {
        var levelDate = (int[])_obj["LevelDate"];
        levelDate[i] = value;
        _obj["LevelDate"] = levelDate;

        _obj.SaveAsync((NCMBException e) =>
        {
            if(e != null)
            {
                Debug.LogError("�f�[�^�̃Z�[�u�Ɏ��s���܂���");
            }
            else
            {
                Debug.Log("�f�[�^�̃Z�[�u�ɐ������܂���");
            }
        });
    }
}
