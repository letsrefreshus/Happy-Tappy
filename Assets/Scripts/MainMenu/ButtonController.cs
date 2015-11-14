using UnityEngine;
using System;

public class ButtonController : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void loadTestLevel()
    {
        Debug.Log("Load Test Level Called!");

        SimpleLevelData level = new SimpleLevelData(10, 15f);
        GameSettings.instance.loadLevel(level);
    }

    public void loadMovingTestLevel()
    {
        Debug.Log("Load Moving Test Level Called!");

        WeightedListLevelData level = new WeightedListLevelData(10, 15f);
        BaseTargetFactory movingTargetFactory = new MovingTargetFactory(0, (float)Math.PI * 2, 0.5f, 1.25f, 0.5f, 5.0f);
        level.addFactory(movingTargetFactory);

        GameSettings.instance.loadLevel(level);
    }


    public void loadMovingAndStillTestLevel()
    {
        Debug.Log("Load Moving Test Level Called!");

        WeightedListLevelData level = new WeightedListLevelData(10, 15f);
        BaseTargetFactory movingTargetFactory = new MovingTargetFactory(0, (float)Math.PI * 2, 0.5f, 1.25f, 0.5f, 5.0f);
        BaseTargetFactory stillTargetFactory = new SimpleTargetFactory();
        level.addFactory(movingTargetFactory);
        level.addFactory(stillTargetFactory);

        GameSettings.instance.loadLevel(level);

    }
}
