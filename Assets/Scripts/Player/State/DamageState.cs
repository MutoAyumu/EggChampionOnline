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
    public void TakeDamage(float damage, Transform other)
    {
        if (_currentState != _guardState && _currentState != _diveState && !IsInvincible)    //�X�e�[�g���h��E����E���G��Ԃ���Ȃ����
        {
            _rb.AddForce((this.transform.position - other.position).normalized * _knockBackPower, ForceMode.Impulse);

            ChangeState(_damageState);  //�_���[�W���󂯂����̃X�e�[�g�ɑJ��

            _rb.velocity = Vector3.zero;
            _anim.SetTrigger("GetHit");
            _count++;
            _timer = 0;

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
    void TakeDamage(float damage, Transform other);
}
