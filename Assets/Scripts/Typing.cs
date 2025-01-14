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
    private float Bonus = 1f;
    public string remainingWord = string.Empty;
    private string currentWord = string.Empty;
    static public FallingWordManager fallingWordManager;
    public FallingWordItem fallingWordItem;
    GameStateManager gameStateManager;
    GameModeManager gameModeManager;
    UIManager uiManager;
    score score;
    void Start()
    {
        uiManager = ServiceLocator.Instance.uiManager;
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
            foreach(FallingWordItem item in fallingWordManager.wordItems.Values)
            {
                item.speed = 0.5f;
            }
            yield return time += Time.deltaTime;
        }
        foreach(FallingWordItem item in fallingWordManager.wordItems.Values)
            {
                item.speed = 1.0f;
            }
    }
    IEnumerator StartFastCoroutine()
    {
        float time = 0f;
        while (time <= nSeconds)
        {
            foreach(FallingWordItem item in fallingWordManager.wordItems.Values)
            {
                item.speed = 2.0f;
            }
            yield return time += Time.deltaTime;
        }
        foreach(FallingWordItem item in fallingWordManager.wordItems.Values)
            {
                item.speed = 1.0f;
            }
    }
    IEnumerator StartFreezeCoroutine()
    {
        float time = 0f;
        while (time <= nSeconds)
        {
            foreach(FallingWordItem item in fallingWordManager.wordItems.Values)
            {
                item.stop = 0.0f;
            }
            yield return time += Time.deltaTime;
        }
        foreach(FallingWordItem item in fallingWordManager.wordItems.Values)
            {
                item.stop = 1.0f;
            }
    }
    IEnumerator Start2xBonusCoroutine()
    {
        float time = 0f;
        while (time <= nSeconds)
        {
            if (Bonus < 3f)
            {
                Bonus = 2f;
            }
            yield return time += Time.deltaTime;
        }
        Bonus = 1f;
    }
    IEnumerator Start3xBonusCoroutine()
    {
        float time = 0f;
        while (time <= nSeconds)
        {
            
            Bonus = 3f;
            yield return time += Time.deltaTime;
        }
        Bonus = 1f;
    }
    IEnumerator StartHalfxCoroutine()
    {
        float time = 0f;
        while (time <= nSeconds)
        {
            Bonus = 0.5f;
            yield return time += Time.deltaTime;
        }
        Bonus = 1f;
    }
    IEnumerator StartBlindCoroutine()
    {
        float time = 0f;
        while (time <= nSeconds)
        {
            uiManager.SetActive("BlindCanvas", true);
            yield return time += Time.deltaTime;
        }
        uiManager.SetActive("BlindCanvas", false);
    }
    private void isCorrect(string letter)
    {
        if (theWords.ContainsKey(letter))
        {
            if (theWords[letter] == true)
            {
                Debug.Log(letter + " Correct");
                Debug.Log(Bonus.ToString()+" "+ Accuracy.ToString()+" "+ Mathf.Floor(time).ToString()+" "+ letter.Length.ToString());
                score.addscore(Bonus, Accuracy, Mathf.Floor(time), letter.Length);
                submitWord();
                fallingWordManager.wordsContainer.RemoveWord(letter);
                fallingWordManager.wordItems[letter].Despawn();
                switch (letter)
                {
                    case "Slow":
                        StartCoroutine(StartSlowCoroutine());
                        break;
                    case "Freeze":
                        StartCoroutine(StartFreezeCoroutine()); //Still error
                        break;
                    case "Fast":
                        StartCoroutine(StartFastCoroutine());
                        break;
                    case "2xBonus":
                        StartCoroutine(Start2xBonusCoroutine());
                        break;
                    case "3xBonus":
                        StartCoroutine(Start3xBonusCoroutine());
                        break;
                    case "Clear":
                        foreach(string item in fallingWordManager.wordItems.Keys.ToList())
                        {
                            fallingWordManager.wordItems[item].Despawn();
                        }
                        break;
                    case "Minus":
                        Debug.Log("trigger minus");
                        score.minusscore(Bonus, Accuracy, Mathf.Floor(time), letter.Length);
                        break;
                    case "Halfx":
                        StartCoroutine(StartHalfxCoroutine());
                        break;
                    case "Blind":
                        StartCoroutine(StartBlindCoroutine());
                        break;
                    case "Shuffle":

                        break;

                }
                if (gameModeManager.game is EndlessMode endlessMode)
                {
                    switch (letter)
                    {
                        case "Shield":
                            endlessMode.ShieldActive(true);
                            break;
                        case "Heal":
                            endlessMode.ChangeLife(1);
                            break;
                        case "Vigor":
                            endlessMode.SetMaxLife(1);
                            break;
                        case "Hurt":
                            endlessMode.ChangeLife(-1);
                            break;
                        case "Sick":
                            endlessMode.SetMaxLife(-1);
                            break;
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
