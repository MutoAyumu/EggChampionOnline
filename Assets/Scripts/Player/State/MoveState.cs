using UnityEngine;

public partial class PlayerController
{
    public class MoveState : PlayerStateBase
    {
        public override void OnUpdate(PlayerController player)
        {
            player.Move();

            if (Input.GetButtonDown(player._attackInputName))   //UŒ‚‚É‘JˆÚ
            {
                player.ChangeState(_attackState); //Ÿ‚ÌState‚ğˆø”‚Éw’è
            }
            else if(Input.GetButtonDown(player._diveInputName))   //‰ñ”ğ‚É‘JˆÚ
            {
                player.ChangeState(_diveState);
            }
            else if(Input.GetButtonDown(player._guardInputName))    //–hŒä‚É‘JˆÚ
            {
                player.ChangeState(_guardState);
            }
        }
    }

    /// <summary>
    /// ˆÚ“®‚Ìˆ—
    /// </summary>
    void Move()
    {
        var h = Input.GetAxisRaw("Horizontal");
        var v = Input.GetAxisRaw("Vertical");

        _dir = new Vector3(h, 0, v);

        var rotateSpeed = _rotatePower * Time.deltaTime;

        if (_dir != Vector3.zero)
        {
            _dir = Camera.main.transform.TransformDirection(_dir);
            _dir.y = 0;

            var rot = Camera.main.transform.forward;
            rot.y = 0;

            _targetRotation = Quaternion.LookRotation(rot, Vector3.up);

            _dir = _dir.normalized * _moveSpeed;
            _dir.y = _rb.velocity.y;
            _rb.velocity = _dir;

            _anim.SetFloat("Horizontal", h, _animDampTime, Time.deltaTime);
            _anim.SetFloat("Vertical", v, _animDampTime, Time.deltaTime);
        }
        else
        {
            _anim.SetFloat("Horizontal", 0, _animDampTime, Time.deltaTime);
            _anim.SetFloat("Vertical", 0, _animDampTime, Time.deltaTime);
        }

        this.transform.rotation = Quaternion.RotateTowards(this.transform.rotation, _targetRotation, rotateSpeed);
    }
}
