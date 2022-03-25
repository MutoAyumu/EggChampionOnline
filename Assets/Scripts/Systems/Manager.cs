using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : Singleton<Manager>
{
    string _avater;

    public string Avater { get => _avater; set => _avater = value; }

    protected override void OnAwake()
    {
        DontDestroyOnLoad(this.gameObject);
    }


}
