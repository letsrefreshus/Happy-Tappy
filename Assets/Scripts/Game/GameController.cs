using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private static Dictionary<string, BaseLevelLoader> _levelLoaders;

    private List<BaseTarget> _targets;
    private float _time;
    
    private static void initLevelLoaders()
    {
        _levelLoaders = new Dictionary<string, BaseLevelLoader>();

        registerLevelLoader(SimpleLevelData.LEVEL_DATA_TYPE, new SimpleLevelLoader());
    }

    private static void registerLevelLoader(string name, BaseLevelLoader loader)
    {
        _levelLoaders.Add(name, loader);
    }

    void Awake()
    {
        if (_levelLoaders == null)
        {
            GameController.initLevelLoaders();
        }
    }

    void Start()
    {
        if(GameSettings.instance == null)
        {
            Debug.Log("No game setting data");
            return;
        }

        loadLevel(GameSettings.instance.getLevelToLoad(), GameSettings.instance.getLoaderToUse());
    }

    /// <summary>
    /// Loads the level with the given loader type.
    /// </summary>
    /// <param name="level">The level data to load</param>
    /// <param name="levelLoaderType">The level data loader to use, if not specified it will default to the level data type.</param>
    public void loadLevel(BaseLevelData level, string levelLoaderType = null)
    {
        if(levelLoaderType == null)
        {
            levelLoaderType = level.getLevelDataType();
        }

        if(_levelLoaders.ContainsKey(levelLoaderType) == false)
        {
            Debug.Log("Unable to locate level loader type : " + levelLoaderType);
            return;
        }

        if(_targets == null)
        {
            _targets = new List<BaseTarget>();
        }
        else
        {
            _targets.Clear();
        }

        _levelLoaders[levelLoaderType].loadLevel(level, this);
    }

    public void addTarget(BaseTarget target, float targetX, float targetY, float targetScale)
    {
        if(_targets.Contains(target))
        {
            return;
        }

        _targets.Add(target);
        target.transform.parent = gameObject.transform;
        target.transform.localScale = new Vector3(targetScale, targetScale);
        target.transform.localPosition = new Vector3(targetX, targetY);

        //Probably do something with setting the layer of the target in here, probably.
    }

    //Setters
    public void setTime(float time)
    {
        _time = time;
    }

    //Getters
}
