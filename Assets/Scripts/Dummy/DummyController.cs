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

    bool IsInvincible;  //無敵状態の時のフラグ

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
        else if (_count > 0)    //角ハメが出来ないように何回か攻撃されたら無敵状態になる
        {
            _timer += Time.deltaTime;

            if (_timer >= _timeLimit)   //タイマーがリミットに達するとカウントをリセット
            {
                _timer = 0;
                _count = 0;
            }

            if(_count >= _damageLimit)   //とりあえず5回くらい
            {
                IsInvincible = true;
                _timer = 0;
                _anim.SetTrigger("HitLimit");
            }
        }

        if (_anim.GetCurrentAnimatorStateInfo(0).IsName("Hit3"))    //連続で攻撃を受けた時に無敵状態にする
        {
            IsInvincible = true;
        }
    }
    public void TakeDamage(float damage, Transform other)
    {
        if (!IsInvincible)
        {
            _rb.AddForce((this.transform.position - other.position).normalized * _knockBackPower, ForceMode.Impulse);   //ノックバック処理

            _anim.SetTrigger("GetHit");
            _count++;
            _timer = 0;
        }
    }
}
