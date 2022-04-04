using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class PlayerController : Singleton<PlayerController>
{
    [Header("Component")]
    [SerializeField] Rigidbody _rb = default;
    [SerializeField] Animator _anim = default;

    [Header("Parameter")]
    [SerializeField] float _moveSpeed = 5f;
    [SerializeField] float _rotatePower = 600f;
    [SerializeField] float _animDampTime = 0.2f;
    [SerializeField] float _attackPower = 3f;
    [SerializeField] float _divePower = 10f;

    [Header("Objects")]
    [SerializeField] GameObject _barrierObj = default;

    [Header("KeyBindingsName")]
    [SerializeField] string _attackInputName = "Fire1"; //後で絶対に変更しないものってconstとかにした方がいい？
    [SerializeField] string _diveInputName = "Fire2";
    [SerializeField] string _guardInputName = "Fire3";

    Quaternion _targetRotation;
    Vector3 _dir;

    private void Start()
    {
        _targetRotation = this.transform.rotation;
        Cursor.lockState = CursorLockMode.Locked;

        OnStart();
    }
    private void Update()
    {
        OnUpdate();
    }

    /// <summary>
    /// AttackStateからMoveStateに戻る為にアニメーショントリガーで使う
    /// </summary>
    public void ResetState()
    {
        ChangeState(_moveState);
    }
}
