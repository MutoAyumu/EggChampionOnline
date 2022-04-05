using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class PlayerController
{
    public class DeathState : PlayerStateBase
    {
        public override void OnEnter(PlayerController player, PlayerStateBase state)
        {
            player.Death();
        }
    }

    void Death()
    {
        //死んだ時の処理を記述
    }
}
