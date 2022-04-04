using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class PlayerController : Singleton<PlayerController>, IDamage
{
    [Header("Component")]
    [SerializeField] Rigidbody _rb = default;
    [SerializeField] Animator _anim = default;

    [Header("Parameter")]
    [SerializeField] float _moveSpeed = 5f;

    [SerializeField] float _rotatePower = 600f;
    [SerializeField] float _animDampTime = 0.2f;

    [SerializeField] float _attackMovePower = 3f;
    [SerializeField] float _attackPower = 1f;

    [SerializeField] float _divePower = 10f;

    [SerializeField] float _maxHp = 10f;
    float _currentHp;

    [Header("Objects")]
    [SerializeField] GameObject _barrierObj = default;

    [Header("KeyBindingsName")]
    [SerializeField] string _attackInputName = "Fire1"; //��Ő�΂ɕύX���Ȃ����̂���const�Ƃ��ɂ������������H
    [SerializeField] string _diveInputName = "Fire2";
    [SerializeField] string _guardInputName = "Fire3";

    Quaternion _targetRotation;
    Vector3 _dir;
    IDamage _target;

    private void Start()
    {
        _currentHp = _maxHp;

        _targetRotation = this.transform.rotation;
        Cursor.lockState = CursorLockMode.Locked;

        OnStart();
    }
    private void Update()
    {
        OnUpdate();

        if(Input.GetKeyDown(KeyCode.E))
        {
            TakeDamage(1);
        }
    }

    /// <summary>
    /// AttackState����MoveState�ɖ߂�ׂɃA�j���[�V�����g���K�[�Ŏg��
    /// </summary>
    public void ResetState()
    {
        ChangeState(_moveState);
    }

    private void OnTriggerEnter(Collider other)
    {
        _target = other.GetComponent<IDamage>();
    }
    private void OnTriggerExit(Collider other)
    {
        _target = null;
    }
}
