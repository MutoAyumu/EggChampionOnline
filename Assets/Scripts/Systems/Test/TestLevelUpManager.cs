using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestLevelUpManager : Singleton<TestLevelUpManager>
{
    static bool[] _purchaseHistory = new bool[6];
    static int[] _levelHistory = new int[6];

    public static bool[] PurchaseHistory { get => _purchaseHistory; set => _purchaseHistory = value; }
    public static int[] LevelHistory { get => _levelHistory; set => _levelHistory = value; }

    /// <summary>
    /// �w��������z���True�ɂ���
    /// </summary>
    /// <param name="num"></param>
    public void PurchaseUpdate(int num)
    {
        _purchaseHistory[num] = true;
    }
    /// <summary>
    /// ���x���̍X�V��ۑ����Ă���
    /// </summary>
    /// <param name="num"></param>
    /// <param name="value"></param>
    public void LevelUpdate(int num, int value)
    {
        _levelHistory[num] = value;
    }
}
