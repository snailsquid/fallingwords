using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Typing : MonoBehaviour
{
    public Dictionary<string, bool> theWords = new Dictionary<string, bool>();
    public int totalScore;
    public float time = 0f;
    private int Accuracy = 0;
    public string remainingWord = string.Empty;
    private string currentWord = string.Empty;
    public void addTheWords(string words)
    {
        if(theWords.ContainsKey(words)){}
        else theWords.Add(words, false);
    }
    public void removeTheWords(string word)
    {
        if(theWords.ContainsKey(word))
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
    }
    private void checkInput()
    {
        if(Input.GetKeyDown(KeyCode.Return))
        {
            Debug.Log("tes");
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
        Debug.Log("tes2");
        if(theWords.ContainsKey(letter))
        {
            Debug.Log("Correct");
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
        if(Index >0)
        {
            string newString = remainingWord.Remove((Index-1),1);
            setRemainingWord(newString);
            if(Accuracy> -2)
            {
                Accuracy -= 1;
            }
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
        //scoreOutput.text = totalScore.ToString();
    }
}
public class Typer 
{
    
}
