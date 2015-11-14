using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class SimpleLevelData : BaseLevelData
{
    public static string LEVEL_DATA_TYPE = "SIMPLE";

    private int _difficulty;
    private float _time;

    //Constructors
    public SimpleLevelData(int difficulty, float time)
    {
        _difficulty = difficulty;
        _time = time;
    }

    //Overrides
    public override string getLevelDataType() { return LEVEL_DATA_TYPE; }
    public override int getDifficulty() { return _difficulty; }
    public override float getTime() { return _time; }
}
