using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Typing : MonoBehaviour
{
    private Dictionary<string, bool> theWords = new Dictionary<string, bool>();
    public int Accuracy = 10;
    public Wordbank wordBank = null;
    public Text wordOutput = null;
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
        Accuracy = 10;
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
        if(theWords[letter]==true)
    //    if(letter == currentWord)
        {
            Debug.Log(letter+"Correct");
            Debug.Log(Accuracy);
            submitWord();
            //setCurrentWord();
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
        Accuracy -= 1;
    }
    private void submitWord()
    {
        int Index = remainingWord.Length;
        string newString = remainingWord.Remove(0,Index);
        setRemainingWord(newString);
    }
}
