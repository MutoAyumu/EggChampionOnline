using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class PlayerController
{
    public class GuardState : PlayerStateBase
    {
        public override void OnEnter(PlayerController player, PlayerStateBase state)
        {
            player.Guard(true);
        }
        public override void OnUpdate(PlayerController player)
        {
            if(Input.GetButtonUp(player._guardInputName))
            {
                player.ChangeState(_moveState);
            }
        }
        public override void OnExit(PlayerController player, PlayerStateBase nextState)
        {
            player.Guard(false);
        }
    }

    /// <summary>
    /// ñhå‰ÇÃèàóù
    /// </summary>
    /// <param name="isbool"></param>
    void Guard(bool isbool)
    {
        _anim.SetBool("Guard", isbool);
        _barrierObj.SetActive(isbool);
    }
}
