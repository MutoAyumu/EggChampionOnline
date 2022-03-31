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

    Quaternion _targetRotation;

    private void Start()
    {
        _targetRotation = this.transform.rotation;
        Cursor.lockState = CursorLockMode.Locked;
    }
    private void Update()
    {
        Move();
    }

    void Move()
    {
        var h = Input.GetAxisRaw("Horizontal");
        var v = Input.GetAxisRaw("Vertical");

        var dir = new Vector3(h, 0, v);

        var rotateSpeed = _rotatePower * Time.deltaTime;

        if(dir != Vector3.zero)
        {
            dir = Camera.main.transform.TransformDirection(dir);
            dir.y = 0;

            //Ç±Ç±èCê≥àƒÇçlÇ¶ÇÈ
            _targetRotation = Quaternion.LookRotation(Camera.main.transform.forward, Vector3.up);

            dir = dir.normalized * _moveSpeed;
            dir.y = _rb.velocity.y;
            _rb.velocity = dir;

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
