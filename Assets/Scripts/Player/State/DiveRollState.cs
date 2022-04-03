using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class PlayerController
{
    public class DiveRollState : PlayerStateBase
    {
        public override void OnEnter(PlayerController player, PlayerStateBase state)
        {
            player.DiveRoll();
        }
    }

    /// <summary>
    /// ����̏���
    /// </summary>
    void DiveRoll()
    {
        _anim.SetTrigger("DiveRoll");

        if (_dir != Vector3.zero)   //�ړ����͂����鎞�ƂȂ����ŕ����Ă���
        {
            this.transform.rotation = Quaternion.LookRotation(_dir, Vector3.up);
        }
        else
        {
            var rot = Camera.main.transform.forward;
            rot.y = 0;
            _targetRotation = Quaternion.LookRotation(rot, Vector3.up);
            this.transform.rotation = _targetRotation;
        }

        _rb.velocity = Vector3.zero;
        _rb.AddForce(this.transform.forward * _divePower, ForceMode.Impulse);
    }
}
