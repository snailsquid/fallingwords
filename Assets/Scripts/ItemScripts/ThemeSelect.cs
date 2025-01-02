using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ThemeSelect : MonoBehaviour
{
    [SerializeField] WordGenerator.Theme theme;
    ServiceLocator serviceLocator;
    UIManager uiManager;
    GameStateManager gameStateManager;
    public void Start()
    {
        serviceLocator = ServiceLocator.Instance;
        uiManager = serviceLocator.uiManager;
        gameStateManager = serviceLocator.gameStateManager;
        GetComponent<Button>().onClick.AddListener(() =>
        {
            gameStateManager.SetTheme(theme);
            gameStateManager.SetGameState(GameStateManager.GameState.Playing);
        });
    }
}
