using System;
using UnityEngine;

public class WeightedListLevelLoader : BaseLevelLoader
{
    // If desired all of this can be moved to simple level data and loaded on a per level basis.
    private static float X_MIN = -1.5f;
    private static float X_MAX = 1.5f;
    private static float Y_MIN = -3.5f;
    private static float Y_MAX = 3.5f;
    private static float TARGET_MIN = 0.5f;
    private static float TARGET_MAX = 1.5f;

    public WeightedListLevelLoader()
    {
    }

    //TODO: actually finish this up.
    public override void loadLevel(BaseLevelData data, GameController game)
    {
        if (data.getLevelDataType() != WeightedListLevelData.LEVEL_DATA_TYPE)
        {
            Debug.Log("Error: Invalid level data for this loader.");
            return;
        }

        WeightedListLevelData levelData = data as WeightedListLevelData;

        WeightedList<BaseTargetFactory> _factoryList = levelData.getFactoryList();

        //Load the level into the game.
        for (int i = 0; i < levelData.getDifficulty(); i++)
        {
            float targetX = UnityEngine.Random.Range(X_MIN, X_MAX);
            float targetY = UnityEngine.Random.Range(Y_MIN, Y_MAX);
            float targetSize = UnityEngine.Random.Range(TARGET_MIN, TARGET_MAX);

            BaseTargetFactory factory = _factoryList.getItem();

            BaseTarget target = factory.makeTarget();

            game.addTarget(target, targetX, targetY, targetSize);
        }
        game.setTime(levelData.getTime(), true);
    }
}
