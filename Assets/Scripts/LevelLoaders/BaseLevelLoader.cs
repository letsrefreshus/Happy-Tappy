using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public abstract class BaseLevelLoader
{
    abstract public void loadLevel(BaseLevelData data, GameController game);
}