using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyController : MonoBehaviour, IDamage
{
    [SerializeField] Animator _anim = default;
    [SerializeField] Rigidbody _rb = default;

    [SerializeField] float _knockBackPower = 2f;

    [SerializeField] float _timeLimit = 3f;
    float _timer;

    [SerializeField] int _damageLimit = 5;

    bool IsInvincible;  //���G��Ԃ̎��̃t���O

    int _count;

    private void Update()
    {
        if(IsInvincible)
        {
            _timer += Time.deltaTime;

            if(_timer >= _timeLimit)
            {
                _timer = 0;
                _count = 0;
                IsInvincible = false;
            }
        }
        else if (_count > 0)    //�p�n�����o���Ȃ��悤�ɉ��񂩍U�����ꂽ�疳�G��ԂɂȂ�
        {
            _timer += Time.deltaTime;

            if (_timer >= _timeLimit)   //�^�C�}�[�����~�b�g�ɒB����ƃJ�E���g�����Z�b�g
            {
                _timer = 0;
                _count = 0;
            }

            if(_count >= _damageLimit)   //�Ƃ肠����5�񂭂炢
            {
                IsInvincible = true;
                _timer = 0;
                _anim.SetTrigger("HitLimit");
            }
        }

        if (_anim.GetCurrentAnimatorStateInfo(0).IsName("Hit3"))    //�A���ōU�����󂯂����ɖ��G��Ԃɂ���
        {
            IsInvincible = true;
        }
    }
    public void TakeDamage(float damage, Transform other)
    {
        if (!IsInvincible)
        {
            _rb.AddForce((this.transform.position - other.position).normalized * _knockBackPower, ForceMode.Impulse);   //�m�b�N�o�b�N����

            _anim.SetTrigger("GetHit");
            _count++;
            _timer = 0;
        }
    }
}
