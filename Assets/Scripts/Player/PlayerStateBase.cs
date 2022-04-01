using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerStateBase
{
    public virtual void OnEnter(PlayerController player, PlayerStateBase state)
    {

    }

    public virtual void OnUpdate(PlayerController player)
    {

    }

    public virtual void OnExit(PlayerController player, PlayerStateBase nextState)
    {

    }
}
