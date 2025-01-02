using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServiceLocator : MonoBehaviour
{
    public static ServiceLocator Instance { get; private set; }
    public GameStateManager gameStateManager;
    public FallingWordManager fallingWordManager;

    public WordBasedGamemode wordBasedGamemode;

    void Awake()
    {
        if (Instance == null || Instance == this)
        {
            Instance = this;
            gameStateManager = FindObjectOfType<GameStateManager>();
            fallingWordManager = FindObjectOfType<FallingWordManager>();
            wordBasedGamemode = FindObjectOfType<WordBasedGamemode>();
        }
        else
        {
            Destroy(this);
            return;
        }
    }
}
