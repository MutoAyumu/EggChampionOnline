using UnityEngine;

public partial class PlayerController
{
    bool IsAttack;

    public class AttackState : PlayerStateBase
    {
        public override void OnEnter(PlayerController player, PlayerStateBase state)
        {
            player.AttackMove();
        }
        public override void OnUpdate(PlayerController player)
        {
            if (Input.GetButtonDown(player._attackInputName) && !player.IsAttack)
            {
                player.AttackMove();
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
            _target.TakeDamage(_attackPower, this.transform);
        }
    }
    void AttackMove()
    {
        _anim.SetTrigger("Attack");
        IsAttack = true;

        _rb.velocity = Vector3.zero;
        _rb.AddForce(this.transform.forward * _attackMovePower, ForceMode.Impulse);
    }
}
