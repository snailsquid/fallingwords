using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ModeSelect : MonoBehaviour
{
    Button button_WordMode;
    Button button_TimeMode;
    Button button_EndlessMode;

    // Start is called before the first frame update
    void Start()
    {
        button_WordMode.onClick.AddListener(() =>
        {
            WordMode();
        });
        button_TimeMode.onClick.AddListener(() =>
        {
            TimeMode();
        });
        button_EndlessMode.onClick.AddListener(() =>
        {
            EndlessMode();
        });
    }
}
