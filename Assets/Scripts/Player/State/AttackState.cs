using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class PlayerController
{
    bool IsAttack;

    public class AttackState : PlayerStateBase
    {
        public override void OnEnter(PlayerController player, PlayerStateBase state)
        {
            player._anim.SetTrigger("Attack");
            player.IsAttack = true;

            player._rb.velocity = Vector3.zero;
            player._rb.AddForce(player.transform.forward * player._attackMovePower, ForceMode.Impulse);
        }
        public override void OnUpdate(PlayerController player)
        {
            if (Input.GetButtonDown(player._attackInputName) && !player.IsAttack)
            {
                player._anim.SetTrigger("Attack");
                player.IsAttack = true;

                player._rb.velocity = Vector3.zero;
                player._rb.AddForce(player.transform.forward * player._attackMovePower, ForceMode.Impulse);
            }
        }
    }

    /// <summary>
    /// çUåÇÇÃèàóù
    /// </summary>
    void Attack()
    {
        IsAttack = false;

        if (_target != null)
        {
            _target.TakeDamage(_attackPower);
        }
    }
}
