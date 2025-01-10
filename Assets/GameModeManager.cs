using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
public class GameModeManager : MonoBehaviour
{
    UIManager uiManager;
    GameStateManager gameStateManager;
    public Game game;
    [SerializeField] float maxTime;
    [SerializeField] int maxWord, maxLife;
    static public Typing typing;
    void Start()
    {
        uiManager = ServiceLocator.Instance.uiManager;
        gameStateManager = ServiceLocator.Instance.gameStateManager;
        typing = ServiceLocator.Instance.typing;
    }
    public void StartGameMode(GameStateManager.GameMode gameMode)
    {
        switch (gameMode)
        {
            case GameStateManager.GameMode.Time:
                game = new TimeMode(maxTime);
                break;
            case GameStateManager.GameMode.Word:
                game = new WordMode(maxWord);
                break;
            case GameStateManager.GameMode.Endless:
                game = new EndlessMode(maxLife);
                break;
        }
    }
    void Update()
    {
        uiManager.SetText("TypingText", typing.remainingWord);
        if (game != null)
        {
            if (game.GetType() == typeof(TimeMode) && gameStateManager.gameState == GameStateManager.GameState.Playing)
            {
                TimeMode timeMode = (TimeMode)game;
                timeMode.AddTime(Time.deltaTime);
            }
        }
    }
}


public abstract class Game
{
    protected UIManager uiManager;
    protected GameStateManager gameStateManager;
    public string uiElement;
    public abstract void StartGame();
    public void SetUIManager()
    {
        uiManager = ServiceLocator.Instance.uiManager;
    }
    public void EndGame()
    {
        ServiceLocator.Instance.gameStateManager.SetGameState(GameStateManager.GameState.GameOver);
        ServiceLocator.Instance.fallingWordManager.Reset();
    }
    public void UpdateUI(string currentValue, string maxValue)
    {
        if (uiElement == null || uiManager == null) return;
        uiManager.SetText(uiElement, currentValue + "/" + maxValue);

    }
}
public class TimeMode : Game
{
    public float timePassed;
    public float maxTime;
    public TimeMode(float maxTime)
    {
        uiElement = "TimeText";
        uiManager = ServiceLocator.Instance.uiManager;
        this.maxTime = maxTime;
        StartGame();
    }
    public override void StartGame()
    {
        SetTime(0);
    }
    public void AddTime(float time)
    {
        SetTime(timePassed + time);
    }
    public void SetTime(float time)
    {
        timePassed = time;
        UpdateUI(Mathf.RoundToInt(time).ToString(), maxTime.ToString());
        if (timePassed >= maxTime)
        {
            EndGame();
        }
    }
}
public class WordMode : Game
{
    public int maxWord = 60;
    public int wordCount;
    public WordMode(int maxWord)
    {
        uiElement = "WordText";
        uiManager = ServiceLocator.Instance.uiManager;
        this.maxWord = maxWord;
        StartGame();
    }
    public override void StartGame()
    {
        SetWordCounter(0);
    }
    public void AddWordCounter()
    {
        SetWordCounter(wordCount + 1);
    }
    public void SetWordCounter(int wordCount)
    {
        this.wordCount = wordCount;
        UpdateUI(wordCount.ToString(), maxWord.ToString());
        if (wordCount >= maxWord)
        {
            EndGame();
        }
    }
}
public class EndlessMode : Game
{
    public int maxLife;
    public int life;
    public bool Shield;
    public EndlessMode(int maxLife)
    {
        uiElement = "LifeText";
        uiManager = ServiceLocator.Instance.uiManager;
        this.maxLife = maxLife;
        StartGame();
    }
    public override void StartGame()
    {
        SetLife(maxLife);
    }
    public void ChangeLife(int plusmin)
    {
        SetLife(life + (1*plusmin));
    }
    public void SetMaxLife(int plusmin)
    {
        maxLife += (1*plusmin);
        UpdateUI(life.ToString(), maxLife.ToString());
    }
    public void SetLife(int life)
    {
        this.life = life;
        UpdateUI(life.ToString(), maxLife.ToString());
        if (life <= 0)
        {
            EndGame();
        }
    }
    public void ShieldActive(bool Active)
    {
        if(Active)
        {
            uiManager.SetText("ShieldText", "Active");
            Shield = true;
        }
        else
        {
            uiManager.SetText("ShieldText","");
            Shield = false;
        }
    }
}