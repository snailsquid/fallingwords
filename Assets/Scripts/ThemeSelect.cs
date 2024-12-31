using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ThemeSelect : MonoBehaviour
{
    public Button button_Eve;
    public Button button_Foo;
    public Button button_Ani;
    public Button button_Tec;
    public Button button_Geo;

    [SerializeField] Transform gameManager;
    public void Start()
    {
        button_Eve.onClick.AddListener(() =>
        {
            gameManager.GetComponent<GameStateManager>().SetTheme(WordGenerator.Theme.eve);
        });
        button_Foo.onClick.AddListener(() =>
        {
            gameManager.GetComponent<GameStateManager>().SetTheme(WordGenerator.Theme.foo);
        });
        button_Ani.onClick.AddListener(() =>
        {
            gameManager.GetComponent<GameStateManager>().SetTheme(WordGenerator.Theme.ani);
        });
        button_Tec.onClick.AddListener(() =>
        {
            gameManager.GetComponent<GameStateManager>().SetTheme(WordGenerator.Theme.tec);
        });
        button_Geo.onClick.AddListener(() =>
        {
            gameManager.GetComponent<GameStateManager>().SetTheme(WordGenerator.Theme.geo);
        });
    }
}
