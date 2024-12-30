using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Typing : MonoBehaviour
{
    public int Accuracy = 10;
    public Wordbank wordBank = null;
    public Text wordOutput = null;
    private string remainingWord = string.Empty;
    private string currentWord = string.Empty;

    void Start()
    {
        setCurrentWord();
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
        if(letter == currentWord)
        {
            Debug.Log("Correct");
            Debug.Log(Accuracy);
            submitWord();
            setCurrentWord();
        }
        else
        {
            Debug.Log("Nigga you typo");
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
