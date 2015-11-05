using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class SimpleTarget : BaseTarget
{
    //Simple target will be a score giving target on hit. and a time sucking target on miss.

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
