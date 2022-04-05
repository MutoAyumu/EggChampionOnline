using UnityEngine;

public partial class PlayerController
{
    public class DamageState : PlayerStateBase
    {
        //とりあえず今のところは何も書かない
    }

    /// <summary>
    /// 攻撃を受ける為の処理
    /// </summary>
    /// <param name="damage"></param>
    public void TakeDamage(float damage, Transform other)
    {
        if (_currentState != _guardState && _currentState != _diveState && !IsInvincible)    //ステートが防御・回避・無敵状態じゃなければ
        {
            _rb.AddForce((this.transform.position - other.position).normalized * _knockBackPower, ForceMode.Impulse);

            ChangeState(_damageState);  //ダメージを受けた時のステートに遷移

            _rb.velocity = Vector3.zero;
            _anim.SetTrigger("GetHit");
            _count++;
            _timer = 0;

            if (_currentHp > 0)
            {
                _currentHp -= damage;

                if (_currentHp <= 0)
                {
                    ChangeState(_deathState);   //死んだ時のステートに遷移
                }
            }

            Debug.Log("攻撃を受けました : 残りのHP " + _currentHp);
        }
    }
}

/// <summary>
/// ダメージ用のインターフェース
/// </summary>
public interface IDamage
{
    void TakeDamage(float damage, Transform other);
}
