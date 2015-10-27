using UnityEngine;
using System.Collections;
using System;

/// <summary>
/// The singleton for sharing data between scenes.
/// </summary>
public class GameSettings : MonoBehaviour {
    public static GameSettings instance;

    private string _loaderToUse;
    private BaseLevelData _levelToLoad;

    private static void init(GameSettings theInstance)
    {
        instance = theInstance;
    }

    void Awake()
    {
        if (GameSettings.instance == null)
        {
            GameSettings.init(this);
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void loadTestLevel()
    {
        Debug.Log("Load Test Level Called!");

        SimpleLevelData level = new SimpleLevelData(10, 30f);
        loadLevel(level);
    }

    public void loadLevel(BaseLevelData levelData, string loaderToUse = null)
    {
        Debug.Log("Load Level Called!");

        setLevelToLoad(levelData);
        setLoaderToUse(loaderToUse);
        
        Application.LoadLevel("sceneGame");
    }

    //Setters
    public void setLoaderToUse(string loaderToUse)
    {
        _loaderToUse = loaderToUse;
    }

    public void setLevelToLoad(BaseLevelData levelData)
    {
        _levelToLoad = levelData;
    }

    //Getters
    public string getLoaderToUse()
    {
        return _loaderToUse;
    }

    public BaseLevelData getLevelToLoad()
    {
        return _levelToLoad;
    }
}
