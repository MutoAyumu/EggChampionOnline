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
    /// サーバーに保存されているお金を読み込む
    /// </summary>
    /// <returns></returns>
    public int LoadMoney()
    {
        var m = 0;

        _obj.FetchAsync((NCMBException e) =>
        {
            if (e != null)
            {
                Debug.LogError("データのロードに失敗しました");
            }
            else
            {
                Debug.Log("データのロードに成功しました");
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
                Debug.LogError("データのセーブに失敗しました");
            }
            else
            {
                Debug.Log("データのセーブに成功しました");
            }
        });
    }
    public void LoadLevelDate(int i)
    {
        _obj.FetchAsync((NCMBException e) =>
        {
            if (e != null)
            {
                Debug.LogError("データのロードに失敗しました");
            }
            else
            {
                Debug.Log("データのロードに成功しました");
            }
        });
    }
    public void SaveLevelDate(int i, int value)
    {
        _obj.SaveAsync((NCMBException e) =>
        {
            if(e != null)
            {
                Debug.LogError("データのセーブに失敗しました");
            }
            else
            {
                Debug.Log("データのセーブに成功しました");
            }
        });
    }
}
