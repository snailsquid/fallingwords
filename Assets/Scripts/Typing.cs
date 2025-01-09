using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class Typing : MonoBehaviour
{
    public Dictionary<string, bool> theWords = new Dictionary<string, bool>();
    public float nSeconds;
    public int totalScore;
    public float time = 0f;
    private int Accuracy = 0;
    public string remainingWord = string.Empty;
    private string currentWord = string.Empty;
    static public FallingWordManager fallingWordManager;
    public FallingWordItem fallingWordItem;
    GameStateManager gameStateManager;
    GameModeManager gameModeManager;
    score score;
    void Start()
    {
        fallingWordManager = ServiceLocator.Instance.fallingWordManager;
        gameStateManager = ServiceLocator.Instance.gameStateManager;
        gameModeManager = ServiceLocator.Instance.gameModeManager;
        score = GameObject.FindWithTag("Player").GetComponent<score>();
    }
    public void addTheWords(string words)
    {
        if (!theWords.ContainsKey(words)) theWords.Add(words, false);
    }
    public void removeTheWords(string word)
    {
        if (theWords.ContainsKey(word))
        {
            theWords.Remove(word);
        }
    }
    private void setRemainingWord(string newWord)
    {
        remainingWord = newWord;
    }
    void Update()
    {
        time += Time.deltaTime;
        checkInput();
        foreach (string item in theWords.Keys.ToList())
        {
            if (remainingWord != "")
            {
                if ((item.StartsWith(remainingWord)) && (theWords[item] == false))
                {
                    theWords[item] = true;
                }
                else if ((theWords[item] == true) && (!item.StartsWith(remainingWord)))
                {
                    theWords[item] = false;
                }
            }
        }
    }
    private void checkInput()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            isCorrect(remainingWord);
        }
        else if (Input.GetKeyDown(KeyCode.Backspace))
        {
            removeLetter();
        }
        else if (Input.anyKeyDown)
        {
            string keyPressed = Input.inputString;
            if (keyPressed.Length == 1)
            {
                enterWord(keyPressed);
            }
        }
    }
    private void enterWord(string typed)
    {
        addLetter(typed);
    }
    IEnumerator StartSlowCoroutine()
    {
        float time = 0f;
        while (time <= nSeconds)
        {
            fallingWordManager.initialSpeed = 0.5f;
            yield return time += Time.deltaTime;
        }
        fallingWordManager.initialSpeed = 1.0f;
    }
    IEnumerator StartFastCoroutine()
    {
        float time = 0f;
        while (time <= nSeconds)
        {
            fallingWordManager.initialSpeed = 2.0f;
            yield return time += Time.deltaTime;
        }
        fallingWordManager.initialSpeed = 1.0f;
    }
    IEnumerator StartFreezeCoroutine()
    {
        fallingWordItem = GameObject.FindWithTag("WordItem").GetComponent<FallingWordItem>();
        float time = 0f;
        while (time <= nSeconds)
        {
            fallingWordItem.stop = 0.0f;
            yield return time += Time.deltaTime;
        }
        fallingWordItem.stop = 1.0f;
    }
    private void isCorrect(string letter)
    {
        if (theWords.ContainsKey(letter))
        {
            if (theWords[letter] == true)
            {
                Debug.Log(letter + " Correct");
                Debug.Log(Accuracy);
                score.addscore(Accuracy, Mathf.Floor(time), letter.Length);
                submitWord();
                fallingWordManager.wordsContainer.RemoveWord(letter);
                fallingWordManager.wordItems[letter].Despawn();
                switch (letter)
                {
                    case "Slow":
                        StartCoroutine(StartSlowCoroutine());
                        break;
                    case "Freeze":
                        StartCoroutine(StartFreezeCoroutine());
                        break;
                    case "Fast":
                        StartCoroutine(StartFastCoroutine());
                        break;
                    case "2xBonus":
                        break;
                    case "3xBonus":
                        break;
                    case "Clear":
                        break;
                    case "Minus":
                        break;
                    case "Halfx":
                        break;
                    case "Blind":
                        break;
                    case "Shuffle":
                        break;

                }
                if (gameStateManager.gameMode == GameStateManager.GameMode.Endless)
                {
                    if (letter == "Shield")
                    {

                    }
                    else if (letter == "Heal")
                    {

                    }
                    else if (letter == "Vigor")
                    {

                    }
                    else if (letter == "Hurt")
                    {

                    }
                    else if (letter == "Sick")
                    {

                    }
                }
                if (gameModeManager.game is WordMode wordMode)
                {
                    wordMode.AddWordCounter();
                }
            }
        }
        else
        {
            Debug.Log("Nice person you typo");
            submitWord();
        }
    }
    private void addLetter(string add)
    {
        int Index = remainingWord.Length;
        string newString = remainingWord.Insert(Index, add);
        setRemainingWord(newString);

    }
    private void removeLetter()
    {
        int Index = remainingWord.Length;
        if (Index > 0)
        {
            string newString = remainingWord.Remove((Index - 1), 1);
            setRemainingWord(newString);
            if (Accuracy > -2)
            {
                Accuracy -= 1;
            }
        }
    }
    private void submitWord()
    {
        int Index = remainingWord.Length;
        string newString = remainingWord.Remove(0, Index);
        setRemainingWord(newString);
        Accuracy = 0;
        time = 0;
    }
}
