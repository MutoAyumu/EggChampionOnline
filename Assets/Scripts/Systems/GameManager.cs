using NCMB;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    int[] _levels;
    NCMBObject _obj;

    public int[] Levels { get => _levels;}
    public NCMBObject Obj { get => _obj; set => _obj = value; }

    protected override void OnAwake()
    {
        
    }

    /// <summary>
    /// �T�[�o�[�ɕۑ�����Ă��邨����ǂݍ���
    /// </summary>
    /// <returns></returns>
    public int LoadMoney()
    {
        var m = 0;

        _obj.FetchAsync((NCMBException e) =>
        {
            if (e != null)
            {
                Debug.LogError("�f�[�^�̃��[�h�Ɏ��s���܂���");
            }
            else
            {
                Debug.Log("�f�[�^�̃��[�h�ɐ������܂���");
                m = (int)_obj["Money"];
            }
        });

        return m;
    }
    public void SaveMoney(int money)
    {
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
            }
        });
    }
    public void SaveLevelDate(int i, int value)
    {
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
