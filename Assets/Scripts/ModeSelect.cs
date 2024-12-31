using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine;

public class ModeSelect : MonoBehaviour
{
    [SerializeField] Transform gameManager;
    public Button button_WordMode;
    public Button button_TimeMode;
    public Button button_EndlessMode;

    public void Start()
    {
        button_WordMode.onClick.AddListener(() =>
        {
            gameManager.GetComponent<GameStateManager>().SetGameMode(GameStateManager.GameMode.Word);
            Menu.ModetoTheme();
        });
        button_TimeMode.onClick.AddListener(() =>
        {
            gameManager.GetComponent<GameStateManager>().SetGameMode(GameStateManager.GameMode.Time);
            Menu.ModetoTheme();
        });
        button_EndlessMode.onClick.AddListener(() =>
        {
            gameManager.GetComponent<GameStateManager>().SetGameMode(GameStateManager.GameMode.Endless);
            Menu.ModetoTheme();
        });
    }
}
