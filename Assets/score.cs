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
    public void addscore(float accuracy, float speed, int wordcomplex)
    {
        float score = (100 * wordcomplex) + (accuracy * 20) - speed;
        int roundedscore = (int)Math.Round(score);
        totalscore += roundedscore;
        text.text = totalscore.ToString();
    }
}
