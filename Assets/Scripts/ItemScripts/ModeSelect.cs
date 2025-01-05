using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine;

public class ModeSelect : MonoBehaviour
{
    [SerializeField] GameStateManager.GameMode gameMode;
    GameStateManager gameStateManager;
    UIManager uiManager;
    public void Start()
    {
        gameStateManager = ServiceLocator.Instance.gameStateManager;
        uiManager = ServiceLocator.Instance.uiManager;
        GetComponent<Button>().onClick.AddListener(() =>
        {
            gameStateManager.SetGameMode(gameMode);
            gameStateManager.SetGameState(GameStateManager.GameState.ThemeSelect);
        });
    }
}
