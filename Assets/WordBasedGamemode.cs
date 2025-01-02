using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WordBasedGamemode : MonoBehaviour
{
    // Start is called before the first frame update
    GameStateManager gameStateManager;
    public int maxWord = 60;
    public int wordCount = 0;

    public TMP_Text wordCountText;
    void Start()
    {
    gameStateManager = ServiceLocator.Instance.gameStateManager;
    }
    public void StartWordBased()
    {
        wordCountText.gameObject.SetActive(true);
    }
    public void AddWordCounter()
    {
        wordCount += 1;
        wordCountText.text = wordCount.ToString() + "/" + maxWord;
        if (wordCount >= maxWord)
        {
            EndRun();
        }
    }   
    void EndRun()
    {
        wordCountText.gameObject.SetActive(false);
    }
}
