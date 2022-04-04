using System.Collections;
using System.Collections.Generic;
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
    public void TakeDamage(float damage)
    {
        if (_currentState != _guardState && _currentState != _diveState)    //ステートが防御と回避じゃなければ
        {
            ChangeState(_damageState);  //ダメージを受けた時のステートに遷移

            _rb.velocity = Vector3.zero;
            _anim.SetTrigger("GetHit");

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
    void TakeDamage(float damage);
}
