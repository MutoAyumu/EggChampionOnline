using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class PlayerController
{
    public class AttackState : PlayerStateBase
    {
        public override void OnEnter(PlayerController player, PlayerStateBase state)
        {
            player.Attack();
        }
        public override void OnUpdate(PlayerController player)
        {
            if (Input.GetButtonDown(player._attackInputName))
            {
                player.Attack();
            }
        }
    }

    /// <summary>
    /// çUåÇÇÃèàóù
    /// </summary>
    void Attack()
    {
        _anim.SetTrigger("Attack");

        _rb.velocity = Vector3.zero;
        _rb.AddForce(this.transform.forward * _attackMovePower, ForceMode.Impulse);
    }

}
