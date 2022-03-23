using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] static int _money = 10000;
    [SerializeField]TMP_Text _moneyText = default;

    public int Money { get => _money; set => _money = value; }

    protected override void OnAwake()
    {
        //DontDestroyOnLoad(this.gameObject);
        MoneyUpdate();
    }
    public void MoneyUpdate()
    {
        _moneyText.text = "����:" + _money.ToString("D7");
    }
}
