using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class TestGameManager : Singleton<TestGameManager>
{
    [SerializeField] static int _money = 10000;
    [SerializeField]TMP_Text _moneyText = default;

    public int Money { get => _money; set => _money = value; }

    protected override void OnAwake()
    {
        MoneyUpdate();
    }
    public void MoneyUpdate()
    {
        _moneyText.text = "‚¨‹à:" + _money.ToString("D7");
    }
    public void MoneyUpdate(int money)
    {
        _money -= money;
        _moneyText.text = "‚¨‹à:" + _money.ToString("D7");
    }
}
