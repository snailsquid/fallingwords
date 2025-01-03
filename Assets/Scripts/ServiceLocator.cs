using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServiceLocator : MonoBehaviour
{
    public static ServiceLocator Instance { get; private set; }
    public GameStateManager gameStateManager;
    public FallingWordManager fallingWordManager;

    public UIManager uiManager;
    public GameModeManager gameModeManager;

    void Awake()
    {
        if (Instance == null || Instance == this)
        {
            Instance = this;
            gameStateManager = FindObjectOfType<GameStateManager>();
            fallingWordManager = FindObjectOfType<FallingWordManager>();
            uiManager = FindObjectOfType<UIManager>();
            gameModeManager = FindObjectOfType<GameModeManager>();
        }
        else
        {
            Destroy(this);
            return;
        }
    }
}
