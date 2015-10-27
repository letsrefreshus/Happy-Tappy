using UnityEngine;

public class SimpleLevelLoader : BaseLevelLoader
{
    private static float X_MIN = -10f;
    private static float X_MAX = 10f;
    private static float Y_MIN = -10f;
    private static float Y_MAX = 10f;
    private static float TARGET_MIN = 1f;
    private static float TARGET_MAX = 3f;

    public override void loadLevel(BaseLevelData data, GameController game)
    {
        if(data.getLevelDataType() != SimpleLevelData.LEVEL_DATA_TYPE)
        {
            Debug.Log("Error: Invalid level data for this loader.");
            return;
        }

        SimpleLevelData levelData = data as SimpleLevelData;

        //Load the level into the game.
        for (int i = 0; i < levelData.getDifficulty(); i++)
        {
            float targetX = Random.Range(X_MIN, X_MAX);
            float targetY = Random.Range(Y_MIN, Y_MAX);
            float targetSize = Random.Range(TARGET_MIN, TARGET_MAX);

            game.addTarget(new SimpleTarget(), targetX, targetY)
        }
            //For each difficulty, add 1 smiliey of random size to the game
            //Set the time for the game.
        game.setTime(levelData.getTime());
    }
}
