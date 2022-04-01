using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class PlayerController
{
    public class MoveState : PlayerStateBase
    {
        public override void OnUpdate(PlayerController player)
        {
            //移動の処理を記述
            player.ChangeState(_moveState);
        }
    }
}
