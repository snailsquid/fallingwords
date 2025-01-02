using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetStateButton : MonoBehaviour
{
    public GameStateManager.GameState state;
    GameStateManager gameStateManager;
    void Start()
    {
        gameStateManager = ServiceLocator.Instance.gameStateManager;
        GetComponent<Button>().onClick.AddListener(() =>
        {
            gameStateManager.SetGameState(state);
        });
    }
}
