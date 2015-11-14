using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class WeightedListLevelData : BaseLevelData
{
    public static string LEVEL_DATA_TYPE = "WEIGHTED_LIST";

    private int _difficulty;
    private float _time;
    private WeightedList<BaseTargetFactory> _factoryList;

    //Constructors
    public WeightedListLevelData(int difficulty, float time)
    {
        _difficulty = difficulty;
        _time = time;
        _factoryList = new WeightedList<BaseTargetFactory>();
    }

    public void addFactory(BaseTargetFactory factory, int weight = 1)
    {
        _factoryList.addItem(factory, weight);
    }

    //Getters
    public WeightedList<BaseTargetFactory> getFactoryList() { return _factoryList; }

    //Overrides
    public override string getLevelDataType() { return LEVEL_DATA_TYPE; }
    public override int getDifficulty() { return _difficulty; }
    public override float getTime() { return _time; }
}
