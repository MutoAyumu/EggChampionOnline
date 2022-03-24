using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUpManager : Singleton<LevelUpManager>
{
    static int[] _levels = new int[3];
    static bool[] _buy = new bool[3];

    public static int[] Levels { get => _levels; set => _levels = value; }
    public static bool[] Buy { get => _buy; set => _buy = value; }

    public int LevelUp(int num)
    {
        return _levels[num]++;
    }
    public bool BuyEgg(int num)
    {
        return _buy[num] = true;
    }
}
