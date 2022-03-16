using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

/// <summary>
/// プレイヤーアバターを操作するクラス
/// </summary>
public class PlayerController : MonoBehaviourPunCallbacks
{
    [SerializeField] Rigidbody _rb = default;
    [SerializeField, Tooltip("通常の移動時に参照される値")] float _moveSpeed = 2f;

    private void FixedUpdate()
    {
        //自分のアバターだけ動かす
        if(photonView.IsMine)
        {
            Move();
        }
    }
    /// <summary>
    /// アバターの移動処理をする関数
    /// </summary>
    void Move()
    {
        var dir = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")).normalized;
        _rb.AddForce(dir * _moveSpeed, ForceMode.Force);
    }
}
