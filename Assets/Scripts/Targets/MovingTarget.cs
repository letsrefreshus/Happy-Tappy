using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class MovingTarget : BaseTarget
{
    private Vector3 _initialPosition;
    private float _direction;
    private float _distance;
    private float _timePerCycle;
    private bool _active = false;
    private float _timeStarted = -1;

    public void init(float direction, float distance, float timePerCycle)
    {
        _direction = direction;
        _distance = distance;
        _timePerCycle = timePerCycle;
    }

    void Update()
    {
        if(_active == true)
        {
            if(_timeStarted < 0)
            {
                _timeStarted = Time.time;
            }

            float timeOffset = (Time.time - _timeStarted) % _timePerCycle;

            //animate
            float distance = (_distance / 2) * (float)Math.Sin(timeOffset * 2 * Math.PI / _timePerCycle);
            Vector3 positionOffest = new Vector3(distance * (float)Math.Cos(_direction), distance * (float)Math.Sin(_direction), 0);
            gameObject.transform.localPosition = _initialPosition + positionOffest;

            if(base.getTargetLayerOrder() == 1)
            {
                Debug.Log("Initial Position : " + _initialPosition);
            }
        }
    }


    public override void onAddedToGame(GameController game)
    {
        _active = true;
        _initialPosition = gameObject.transform.localPosition;
    }

    public override void onGameEnd(GameController game)
    {
        //Not necessary but I want to stop motion when the game ends.
        _active = false;
    }

    public override void onClick(GameController game)
    {
        foreach (BaseTarget target in game.getTargets())
        {
            if(target.getTargetLayerOrder() > getTargetLayerOrder())
            {
                Collider2D targetCollider = target.gameObject.GetComponent<Collider2D>();
                if(Physics2D.IsTouching(targetCollider, gameObject.GetComponent<Collider2D>()))
                {
                    Debug.Log("Invalid!");
                    game.timeBonus(-1f);
                    return;
                }

            }
        }

        //What to do if it's a valid click.
        Debug.Log("Valid!");
        game.getCurrentPlayer().addScore(1);
        game.RemoveTarget(this);
    }
}
