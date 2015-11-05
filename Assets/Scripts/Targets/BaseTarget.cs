using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

abstract public class BaseTarget : MonoBehaviour
{
    protected int _targetLayerOrder;
    
    abstract public void onClick(GameController game);

    virtual public void onAddedToGame(GameController game) { }
    virtual public void onGameEnd(GameController game) { }

    //Setters
    public void setTargetLayerOrder(int value) { _targetLayerOrder = value; }

    //Getters
    public int getTargetLayerOrder() { return _targetLayerOrder; }
}
