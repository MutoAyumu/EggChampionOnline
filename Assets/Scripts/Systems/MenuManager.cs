using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public void Test2(int i)
    {
        GameManager.Instance.SaveMoney(i);
    }
    public void Test1()
    {
        GameManager.Instance.LoadMoney();
    }
}
