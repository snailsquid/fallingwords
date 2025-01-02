using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class Wordbank : MonoBehaviour
{
    private List<string> originalWords = new List<string>()
    {"pen","bag","key","cup","mug","bed"};
    private List<string> workingWords = new List<string>();
    private void Awake()
    {
        workingWords.AddRange(originalWords);
        Shuffle(workingWords);
        lowerCase(workingWords);
    }
    private void Shuffle(List<string> list)
    {
        for(int i = 0; i < list.Count; i++)
        {
            int random = Random.Range(i,list.Count);
            string temporary = list[i];
            list[i]=list[random];
            list[random]=temporary;
        }
    }
    private void lowerCase(List<string> list)
    {
        for(int i = 0; i < list.Count; i++)
        {
            list[i] = list[i].ToLower();
        }
    }
    public string getWord()
    {
        string newWord = string.Empty;
        if(workingWords.Count > 0)
        {
            newWord = workingWords.Last();
            workingWords.Remove(newWord);
        }
        else
        {
            workingWords.AddRange(originalWords);
            Shuffle(workingWords);
            lowerCase(workingWords);
        }
        return newWord;
    }
}
