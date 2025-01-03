using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServiceLocator : MonoBehaviour
{
    public static ServiceLocator Instance { get; private set; }
    public GameStateManager gameStateManager;
    public FallingWordManager fallingWordManager;

    public WordBasedGamemode wordBasedGamemode;
    public UIManager uiManager;

    void Awake()
    {
        if (Instance == null || Instance == this)
        {
            Instance = this;
            gameStateManager = FindObjectOfType<GameStateManager>();
            fallingWordManager = FindObjectOfType<FallingWordManager>();
            wordBasedGamemode = FindObjectOfType<WordBasedGamemode>();
            uiManager = FindObjectOfType<UIManager>();
        }
        else
        {
            Destroy(this);
            return;
        }
    }
}
