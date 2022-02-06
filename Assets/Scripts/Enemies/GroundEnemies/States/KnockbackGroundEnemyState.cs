using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockbackGroundEnemyState : State
{
    //-- VARIABLES -----------------------------------------------------------------------------------------------------------------

    private GroundEnemyPatrolController _enemyController;
    private Vector3 _movement;

    private float _direction;
    private float _timeToWait;
    private float _knockbackTime;

    private const string MAINCOLOR = "_MainColor";


    //-- INIT, UPDATE & EXIT -------------------------------------------------------------------------------------------------------

    public override void InitState<T>(T param)
    {
        _enemyController = param as GroundEnemyPatrolController;

        if (_enemyController != null)
        {
            _enemyController.material.SetColor(MAINCOLOR, Color.red);

            _enemyController._animator.SetBool("walk", false);
            _enemyController.isAnyStateRunning = true;

            ApplyKnockbackDirection();

            _timeToWait = _enemyController.knockbackStateDuration;
            _knockbackTime = _enemyController.knockbackDuration;
        }
    }

    public override void UpdateState(float delta)
    {
        if (_timeToWait > 0)
        {
            _timeToWait -= delta;
        }
        else
        {
            _enemyController.isAnyStateRunning = false;
        }

        if (_knockbackTime > 0)
        {
            _movement.Set(_enemyController.knockbackForce * _direction, _enemyController.rigidBody.velocity.y, 0.0f);
            _enemyController.rigidBody.velocity = _movement;
            _knockbackTime -= delta;
        }
        else
        {
            _movement.Set(0.0f, 0.0f, 0.0f);
            _enemyController.rigidBody.velocity = _movement;
        }
    }

    public override void ExitState() {
        _enemyController.material.SetColor(MAINCOLOR, _enemyController.initColorMaterial);
    }

    
    //-- AUXILIAR ------------------------------------------------------------------------------------------------------------------

    private void ApplyKnockbackDirection()
    {
        _movement.Set(0.0f, 0.0f, 0.0f);
        _enemyController.rigidBody.velocity = _movement;

        _direction = _enemyController.transform.position.x - _enemyController.player.transform.position.x;

        if (_direction > 0)
        {
            _direction = 1;
        }
        else
        {
            _direction = -1;
        }
    }

}
