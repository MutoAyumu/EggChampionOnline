using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class LevelUpSystem : Singleton<LevelUpSystem>
{
    [SerializeField] TextAsset _csv = default;
    [SerializeField, Tooltip("Xが行　Yが列")] Vector2Int _matrix = default;
    int[,] _levelTable = default;

    protected override void OnAwake()
    {
        //テーブルデータの入れ物を作る
        _levelTable = new int[_matrix.x, _matrix.y];

        var stR = new StringReader(_csv.text);
        //一行目を捨てる
        var trash = stR.ReadLine();

        if(stR != null)
        {
            for(int i1 = 0; i1 < _matrix.x; i1++)
            {
                var status = stR.ReadLine().Split(',');

                for(int i2 = 0; i2 < _matrix.y; i2++)
                {
                    _levelTable[i1, i2] = int.Parse(status[i2]);
                }
            }
        }
    }
}
