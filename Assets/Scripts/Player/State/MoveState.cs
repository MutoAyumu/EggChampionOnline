using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class PlayerController
{
    public class MoveState : PlayerStateBase
    {
        public override void OnUpdate(PlayerController player)
        {
            //ˆÚ“®‚Ìˆ—‚ğ‹Lq
            player.ChangeState(_moveState);
        }
    }
}
