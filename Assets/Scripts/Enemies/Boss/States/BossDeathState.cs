using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDeathState : State
{
    private BossController _bossController;
    private Vector3 _movement;

    //-- START, UPDATE, EXIT STATE -------------------------------------------------------------------------------------------------

    public override void InitState<T>(T param)
    {
        _bossController = param as BossController;

        if (_bossController != null)
        {
            _bossController.isAnyStateRunning = true;
            _bossController.isFallen = true;

            _movement.Set(0.0f, 0.0f, 0.0f);
            Flip();
            _bossController.animatorController.Defeated();
            _bossController.Defeated();
        }
    }

    public override void UpdateState(float delta) {}

    public override void ExitState() {}

    private void Flip()
    {
        if (_bossController.player.transform.position.x > _bossController.transform.position.x && _bossController.facingDirection < 0)
        {
            _bossController.Flip();
        }
        else if (_bossController.player.transform.position.x < _bossController.transform.position.x && _bossController.facingDirection > 0)
        {
            _bossController.Flip();
        }
    }

}
