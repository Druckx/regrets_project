using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossInitFightState : State
{
    private BossController _bossController;
    private Vector3 _movement;

    public override void InitState<T>(T param)
    {
        _bossController = param as BossController;

        if (_bossController != null)
        {
            _movement.Set(0.0f, 0.0f, 0.0f);
            _bossController.rigidBody.velocity = _movement;
        }
    }

    public override void UpdateState(float delta)
    {
        //throw new System.NotImplementedException();
    }

    public override void ExitState()
    {
        //throw new System.NotImplementedException();
    }

    
}
