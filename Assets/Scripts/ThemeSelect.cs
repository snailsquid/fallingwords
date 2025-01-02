using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ThemeSelect : MonoBehaviour
{
    [SerializeField] WordGenerator.Theme theme;
    Button button;
    public void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(() =>
        {
            ServiceLocator.Instance.gameStateManager.SetTheme(theme);
        });
    }
}
