using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Rigidbody _rb = default;

    [SerializeField] float _moveSpeed = 5f;
    [SerializeField] float _rotatePower = 600f;

    Quaternion _targetRotation;

    private void Start()
    {
        _targetRotation = this.transform.rotation;
    }
    private void Update()
    {
        OnMove();
    }

    void OnMove()
    {
        var h = Input.GetAxisRaw("Horizontal");
        var v = Input.GetAxisRaw("Vertical");

        var dir = new Vector3(h, 0, v);

        var rotateSpeed = _rotatePower * Time.deltaTime;

        if(dir != Vector3.zero)
        {
            dir = Camera.main.transform.TransformDirection(dir);
            dir.y = 0;

            _targetRotation = Quaternion.LookRotation(dir, Vector3.up);

            dir = dir.normalized * _moveSpeed;
            dir.y = _rb.velocity.y;
            _rb.velocity = dir;
        }

        this.transform.rotation = Quaternion.RotateTowards(this.transform.rotation, _targetRotation, rotateSpeed);
    }
}
