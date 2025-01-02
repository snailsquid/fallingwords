using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine;

public class ModeSelect : MonoBehaviour
{
    [SerializeField] GameStateManager.GameMode gameMode;
    Button button;
    public void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(() =>
        {
            ServiceLocator.Instance.gameStateManager.SetGameMode(gameMode);
        });
    }
}
