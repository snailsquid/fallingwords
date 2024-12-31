using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordGenerator : MonoBehaviour
{
    public Theme theme { get; private set; }
    public enum Theme
    {
        eve,
        foo,
        ani,
        tec,
        geo
    }
}
