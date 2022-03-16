using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

/// <summary>
/// �v���C���[�A�o�^�[�𑀍삷��N���X
/// </summary>
public class PlayerController : MonoBehaviourPunCallbacks
{
    [SerializeField] Rigidbody _rb = default;
    [SerializeField, Tooltip("�ʏ�̈ړ����ɎQ�Ƃ����l")] float _moveSpeed = 2f;

    private void FixedUpdate()
    {
        //�����̃A�o�^�[����������
        if(photonView.IsMine)
        {
            Move();
        }
    }
    /// <summary>
    /// �A�o�^�[�̈ړ�����������֐�
    /// </summary>
    void Move()
    {
        var dir = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")).normalized;
        _rb.AddForce(dir * _moveSpeed, ForceMode.Force);
    }
}
