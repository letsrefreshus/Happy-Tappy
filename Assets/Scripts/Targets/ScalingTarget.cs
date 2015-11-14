using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class ScalingTarget : BaseTarget
{
    private Vector3 _initialScale;
    private float _minScale;
    private float _maxScale;
    private float _timePerCycle;
    private bool _active = false;
    private float _timeStarted = -1;

    public void init(float minScale, float maxScale, float timePerCycle)
    {
        _minScale = minScale;
        _maxScale = maxScale;
        _timePerCycle = timePerCycle;
    }

    void Update()
    {
        if (_active == true)
        {
            if (_timeStarted < 0)
            {
                _timeStarted = Time.time;
            }

            float timeOffset = (Time.time - _timeStarted) % _timePerCycle;

            //animate
            float scaleMultiplier = _minScale + ((_maxScale - _minScale) * Math.Abs((float)Math.Sin(timeOffset * 2 * Math.PI / _timePerCycle)));
            gameObject.transform.localScale = scaleMultiplier * _initialScale;
        }
    }


    public override void onAddedToGame(GameController game)
    {
        _active = true;
        _initialScale = gameObject.transform.localScale;
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
            if (target.getTargetLayerOrder() > getTargetLayerOrder())
            {
                Collider2D targetCollider = target.gameObject.GetComponent<Collider2D>();
                if (Physics2D.IsTouching(targetCollider, gameObject.GetComponent<Collider2D>()))
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
