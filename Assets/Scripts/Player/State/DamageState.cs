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
        ChangeState(_damageState);  //ダメージを受けた時のステートに遷移

        _anim.SetTrigger("GetHit");

        if(_currentHp > 0)
        {
            _currentHp -= damage;

            if(_currentHp <= 0)
            {
                ChangeState(_deathState);   //死んだ時のステートに遷移
            }
        }

        Debug.Log("攻撃を受けました : 残りのHP " + _currentHp);
    }
}
