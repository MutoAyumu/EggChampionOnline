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
     スペルミス恥ずかしいから修正する
    Date → Deta
    
      データ取得の説明サイト
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
                //var s = _obj["Money"].ToString();
                //_money = int.Parse(s);
                var m = (long)_obj["Money"];
                _money = (int)m;
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

                //var s = _obj["Money"].ToString();
                //_money = int.Parse(s);
                var m = (long)_obj["Money"];
                _money = (int)m;
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
                var levelDate = (ArrayList)_obj["LevelDate"];
                var l = (long)levelDate[i];
                _level = (int)l;
            }
        });
    }

    /// <summary>
    /// サーバーに保存されているレベルデータを更新
    /// </summary>
    /// <param name="i"></param>
    /// <param name="value"></param>
    public void SaveLevelDate(int i, int value)
    {
        _obj.FetchAsync((NCMBException e) =>
        {
            if(e != null)
            {
                Debug.LogError("データのロードに失敗しました");
            }
            else
            {
                Debug.Log("データのロードに成功しました");

                var levelDate = (ArrayList)_obj["LevelDate"];
                levelDate[i] = value;
                _obj["LevelDate"] = levelDate;

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
        });
    }
}
