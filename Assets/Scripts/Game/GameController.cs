using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    private static Dictionary<string, BaseLevelLoader> _levelLoaders;

    private SimplePlayer _currentPlayer;
    private List<SimplePlayer> _players;    //Probably should change SimplePlayer/BasePlayer thing into simply just Player.
    private List<BaseTarget> _targets;
    private float _maxTime;
    private float _initialTime;
    private float _time;
    private bool _gameActive;

    public GameObject timerBar;
    public GameObject gameOverCanvas;

    private static void initLevelLoaders()
    {
        _levelLoaders = new Dictionary<string, BaseLevelLoader>();

        registerLevelLoader(SimpleLevelData.LEVEL_DATA_TYPE, new SimpleLevelLoader());
        registerLevelLoader(GameSettings.SIMPLE_WEIGHTED_LIST_LOADER, new WeightedListLevelLoader());
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

        gameOverCanvas.SetActive(false);
        _gameActive = false;
        loadLevel(GameSettings.instance.getLevelToLoad(), GameSettings.instance.getLoaderToUse());

        if (_players != null)
        {
            _players.Clear();
        }
        else
        {
            _players = new List<SimplePlayer>();
        }
        _players.Add(new SimplePlayer(GameSettings.instance.getPlayerName()));
        _currentPlayer = _players[0];

        //Maybe wait until a button is pressed, who knows.
        startGame();
    }

    void Update()
    {
        if(_gameActive == true)
        {
            //Update Time
            _time -= Time.deltaTime;
            updateTimerBar();
            if(_time <= 0)
            {
                endGame();
                return;
            }

            //Check for touches, only new touches count.
            foreach (Touch touch in Input.touches)
            {
                if (touch.phase == TouchPhase.Began)
                {
                    //Determine what the touch is touching.
                    RaycastHit2D hitInfo = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

                    if(hitInfo.collider != null)
                    {
                        Debug.Log("Object Hit : " + hitInfo.collider.gameObject.name);
                        BaseTarget targetComp = hitInfo.collider.gameObject.GetComponent<BaseTarget>();
                        if (targetComp != null)
                        {
                            targetComp.onClick(this);
                        }
                    }
                }
            }
        }
    }

    private void endGame()
    {
        _gameActive = false;

        //Callback to all targets
        foreach (BaseTarget target in _targets)
        {
            target.onGameEnd(this);
        }

        //Update the game over canvas

        //Open the game over popup
        gameOverCanvas.SetActive(true);
    }

    private void updateTimerBar()
    {
        timerBar.transform.localScale = new Vector3(1, Math.Max(_time / _maxTime, 0));
        //Maybe i'll reposition this here...
    }

    public void onGameOverClick()
    {
        Application.LoadLevel("sceneLevelSelect");
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
        SpriteRenderer renderer = target.GetComponent<SpriteRenderer>();
        if(renderer != null)
        {
            renderer.sortingLayerName = "targets";
            renderer.sortingOrder = _targets.Count;
        }
        target.setTargetLayerOrder(_targets.Count);
        target.transform.parent = gameObject.transform;
        target.transform.localScale = new Vector3(targetScale, targetScale);
        target.transform.localPosition = new Vector3(targetX, targetY, -(_targets.Count * 0.01f));  //The z hack fixes an issue with which target is touched detection.

        target.onAddedToGame(this);
    }

    public void RemoveTarget(BaseTarget target)
    {
        if(_targets.Contains(target))
        {
            _targets.Remove(target);
            Destroy(target.gameObject);
            target = null;
        }

        if(_targets.Count == 0)
        {
            Debug.Log("You Win!");
            endGame();
        }
    }

    public void startGame()
    {
        _gameActive = true;
    }

    public void timeBonus(float timeBonus)
    {
        addTime(timeBonus);
        if(timeBonus > 0)
        {
            //Animate time gain
            timerBar.GetComponent<Animator>().SetTrigger("loseTime");
        }
        else if(timeBonus < 0)
        {
            //Animate time loss
            timerBar.GetComponent<Animator>().SetTrigger("loseTime");
        }
    }

    public void addTime(float timeToAdd)
    {
        float newTime = _time + timeToAdd;
        setTime(newTime);
    }

    //Setters
    public void setTime(float time, bool setInitial = false)
    {
        _time = time;

        if(_time > _initialTime)
        {
            _maxTime = _time;
        }

        if (setInitial == true)
        {
            _initialTime = _time;
            _maxTime = _time;
        }
    }

    //Getters
    public List<BaseTarget> getTargets() { return _targets; }
    public SimplePlayer getCurrentPlayer() { return _currentPlayer; }
}
