using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Typing : MonoBehaviour
{
    private Dictionary<string, bool> theWords = new Dictionary<string, bool>();
    public int totalScore;
    public float time = 0f;
    private int Accuracy = 0;
    public Wordbank wordBank;
    public Text wordOutput;
    public Text scoreOutput;
    private string remainingWord = string.Empty;
    private string currentWord = string.Empty;

    void Start()
    {
        setCurrentWord();
        theWords.Add("pen",false);
        theWords.Add("penys",false);
        theWords.Add("bag",false);
        theWords.Add("bang",false);
        foreach(var item in theWords)
        {
            Debug.Log(item.Key);
        }
        
    }
    private void setCurrentWord()
    {
        Accuracy = 0;
        currentWord = wordBank.getWord();
        Debug.Log(currentWord);
        if(currentWord == "")
        {
            setCurrentWord();
        }
    }
    private void setRemainingWord(string newWord)
    {
        remainingWord = newWord;
        wordOutput.text = remainingWord;
    }
    void Update()
    {
        time += Time.deltaTime;
        checkInput();
        foreach(string item in theWords.Keys.ToList())
        {
            if(remainingWord != "")
            {
                if((item.StartsWith(remainingWord))&&(theWords[item] == false))
                {
                    theWords[item] = true;
                    Debug.Log(item);
                    Debug.Log(theWords[item]);
                }
                else if((theWords[item] == true)&&(!item.StartsWith(remainingWord)))
                {
                    theWords[item] = false;
                    Debug.Log(item);
                    Debug.Log(theWords[item]);
                }
            }
        }
    }
    private void checkInput()
    {
        if(Input.GetKeyDown(KeyCode.Return))
        {
            isCorrect(remainingWord);
        }
        else if(Input.GetKeyDown(KeyCode.Backspace))
        {
            removeLetter();
        }
        else if(Input.anyKeyDown)
        {
            string keyPressed = Input.inputString;
            if(keyPressed.Length == 1)
            {
                enterWord(keyPressed);
            }
        }
    }
    private void enterWord(string typed)
    {
        addLetter(typed);
    }
    private void isCorrect(string letter)
    {
        if(theWords.ContainsKey(letter))
        {
            if(theWords[letter]==true)
            //if(letter == currentWord)
            {
                Debug.Log(letter+"Correct");
                Debug.Log(Accuracy);
                addScore(letter,Accuracy,Mathf.Floor(time));
                submitWord();
                //setCurrentWord();
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
        string newString = remainingWord.Insert(Index,add);
        setRemainingWord(newString);
    }
    private void removeLetter()
    {
        int Index = remainingWord.Length;
        string newString = remainingWord.Remove((Index-1),1);
        setRemainingWord(newString);
        if(Accuracy> -2)
        {
            Accuracy -= 1;
        }
    }
    private void submitWord()
    {
        int Index = remainingWord.Length;
        string newString = remainingWord.Remove(0,Index);
        setRemainingWord(newString);
        Accuracy = 0;
        time = 0;
    }
    private void addScore(string word,int akurasi,float speed)
    {
        int score = (100*word.Length)+(akurasi*20)-((int)speed);
        if(score >= 0)
        {
            totalScore += score;
        }
        scoreOutput.text = totalScore.ToString();
    }
}
