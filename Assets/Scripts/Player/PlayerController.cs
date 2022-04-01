using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Rigidbody _rb = default;
    [SerializeField] Animator _anim = default;

    [SerializeField] float _moveSpeed = 5f;
    [SerializeField] float _rotatePower = 600f;
    [SerializeField] float _animDampTime = 0.2f;
    [SerializeField] float _attackPower = 3f;

    [SerializeField] GameObject _barrierObj = default;

    [SerializeField] PlayerStatus _status = PlayerStatus.IDLE;

    Quaternion _targetRotation;

    enum PlayerStatus
    {
        IDLE,
        MOVE,
        ATTACK,
        GUARD,
        KNOCKBACK,
        DIVEROLL
    }
    private void Start()
    {
        _targetRotation = this.transform.rotation;
        Cursor.lockState = CursorLockMode.Locked;
    }
    private void Update()
    {
        switch(_status)
        {
            case PlayerStatus.IDLE:
                break;

            case PlayerStatus.MOVE:
                break;

            case PlayerStatus.ATTACK:
                break;

            case PlayerStatus.KNOCKBACK:
                break;

            case PlayerStatus.DIVEROLL:
                break;
        }

        Move();

        if (Input.GetButtonDown("Fire1") && _status != PlayerStatus.GUARD)
        {
            _status = PlayerStatus.ATTACK;
            Attack();
        }

        if(Input.GetButtonDown("Fire2") && _status != PlayerStatus.ATTACK)
        {
            _status = PlayerStatus.GUARD;
            _barrierObj.SetActive(true);
            _anim.SetBool("Guard", true);
        }
        else if(Input.GetButtonUp("Fire2") && _status == PlayerStatus.GUARD)
        {
            _status = PlayerStatus.IDLE;
            _barrierObj.SetActive(false);
            _anim.SetBool("Guard", false);
        }

    }

    void Move()
    {
        var h = Input.GetAxisRaw("Horizontal");
        var v = Input.GetAxisRaw("Vertical");

        var dir = new Vector3(h, 0, v);

        var rotateSpeed = _rotatePower * Time.deltaTime;

        if(dir != Vector3.zero)
        {
            _status = PlayerStatus.MOVE;

            dir = Camera.main.transform.TransformDirection(dir);
            dir.y = 0;

            var rot = Camera.main.transform.forward;
            rot.y = 0;

            _targetRotation = Quaternion.LookRotation(rot, Vector3.up);

            dir = dir.normalized * _moveSpeed;
            dir.y = _rb.velocity.y;
            _rb.velocity = dir;

            _anim.SetFloat("Horizontal", h, _animDampTime, Time.deltaTime);
            _anim.SetFloat("Vertical", v, _animDampTime, Time.deltaTime);
        }
        else
        {
            _status = PlayerStatus.IDLE;

            _anim.SetFloat("Horizontal", 0, _animDampTime, Time.deltaTime);
            _anim.SetFloat("Vertical", 0, _animDampTime, Time.deltaTime);
        }

        this.transform.rotation = Quaternion.RotateTowards(this.transform.rotation, _targetRotation, rotateSpeed);
    }

    void Attack()
    {
        _anim.SetTrigger("Attack");
        _rb.AddForce(this.transform.forward * _attackPower, ForceMode.Impulse);
    }

    public void ResetStatus()
    {
        _status = PlayerStatus.IDLE;
    }

}
