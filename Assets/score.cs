using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class score : MonoBehaviour
{
    public TMP_Text text;
    public int totalscore = 0;
    public void addscore(float Bonus, float accuracy, float speed, int wordcomplex)
    {
        float score = Bonus * ((100 * wordcomplex) + (accuracy * 20) - speed);
        int roundedscore = (int)Math.Round(score);
        totalscore += roundedscore;
        text.text = totalscore.ToString();
    }
    public void minusscore(float Bonus, float accuracy, float speed, int wordcomplex)
    {
        float score1 = Bonus * ((100 * wordcomplex) + (accuracy * 20) - speed);
        int roundedscore = (int)Math.Round(score1);
        int random1 = UnityEngine.Random.Range(50, 100);
        int random2 = UnityEngine.Random.Range(1, 5);
        int score = random1 * random2;
        int scorefinal = roundedscore + score;
        Debug.Log(scorefinal);
        totalscore -= scorefinal;
        text.text = totalscore.ToString();
    }
    public void setScore(int score)
    {
        totalscore = score;
        text.text = totalscore.ToString();
    }
}
