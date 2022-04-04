using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class PlayerController
{
    public class DamageState : PlayerStateBase
    {
        //�Ƃ肠�������̂Ƃ���͉��������Ȃ�
    }

    /// <summary>
    /// �U�����󂯂�ׂ̏���
    /// </summary>
    /// <param name="damage"></param>
    public void TakeDamage(float damage)
    {
        if (_currentState != _guardState && _currentState != _diveState)    //�X�e�[�g���h��Ɖ������Ȃ����
        {
            ChangeState(_damageState);  //�_���[�W���󂯂����̃X�e�[�g�ɑJ��

            _rb.velocity = Vector3.zero;
            _anim.SetTrigger("GetHit");

            if (_currentHp > 0)
            {
                _currentHp -= damage;

                if (_currentHp <= 0)
                {
                    ChangeState(_deathState);   //���񂾎��̃X�e�[�g�ɑJ��
                }
            }

            Debug.Log("�U�����󂯂܂��� : �c���HP " + _currentHp);
        }
    }
}

/// <summary>
/// �_���[�W�p�̃C���^�[�t�F�[�X
/// </summary>
public interface IDamage
{
    void TakeDamage(float damage);
}
