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
    /// サーバーに保存されているお金を読み込む
    /// </summary>
    /// <returns></returns>
    public void LoadMoney()
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
                var s = _obj["Money"].ToString();
                _money = int.Parse(s);
            }
        });
    }

    /// <summary>
    /// サーバーに保存されているお金を更新する
    /// </summary>
    /// <param name="money"></param>
    public void SaveMoney(int money)
    {
        _obj.FetchAsync((NCMBException e) =>    //読み込んで
        {
            if (e != null)
            {
                Debug.LogError("データのロードに失敗しました");
            }
            else
            {
                Debug.Log("データのロードに成功しました");

                var s = _obj["Money"].ToString();
                _money = int.Parse(s);
                _money -= money;
                _obj["Money"] = _money;

                _obj.SaveAsync((NCMBException e) =>     //保存する
                {
                    if (e != null)
                    {
                        Debug.LogError("ユーザーデータのセーブに失敗しました");
                    }
                    else
                    {
                        Debug.Log("ユーザーデータのセーブに成功しました");
                    }
                });
            }
        });
    }

    /// <summary>
    /// サーバーに保存されているレベルデータを読み込む
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
                Debug.LogError("データのロードに失敗しました");
            }
            else
            {
                Debug.Log("データのロードに成功しました");
            }
        });

        return level;
    }

    /// <summary>
    /// サーバーに保存されているレベルデータを更新
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
                Debug.LogError("データのセーブに失敗しました");
            }
            else
            {
                Debug.Log("データのセーブに成功しました");
            }
        });
    }
}
