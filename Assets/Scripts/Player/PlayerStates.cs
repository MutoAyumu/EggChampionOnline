using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class PlayerController
{
    //�eState�������ɋL�q����
    static readonly MoveState _moveState = new MoveState(); //readonly�œǂݎ���p�ɂ��Ă���
    static readonly AttackState _attackState = new AttackState();
    static readonly DiveRollState _diveState = new DiveRollState();

    PlayerStateBase _currentState = _moveState;

    void OnStart()
    {
        _currentState.OnEnter(this, null);
    }

    void OnUpdate()
    {
        _currentState.OnUpdate(this);
    }

    void ChangeState(PlayerStateBase nextState)
    {
        _currentState.OnExit(this, nextState);
        nextState.OnEnter(this, _currentState);
        _currentState = nextState;
    }

    void OnDeath()
    {

    }
}
