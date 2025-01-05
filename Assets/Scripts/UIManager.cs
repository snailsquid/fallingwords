using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AYellowpaper.SerializedCollections;
using UnityEngine.EventSystems;
using TMPro;
public class UIManager : MonoBehaviour
{
  [SerializedDictionary("UI Element", "UI Game Object")]
  public SerializedDictionary<string, UIBehaviour> uiElements;
  [SerializedDictionary("UI Element Group", "UI Element")]
  public SerializedDictionary<string, string[]> uiElementGroup;
  public void SetActive(string uiElement, bool active)
  {
    if (uiElements.ContainsKey(uiElement))
    {
      uiElements[uiElement].gameObject.SetActive(active);
    }
  }
  public void SetActiveGroup(string group, bool active)
  {
    if (uiElementGroup.ContainsKey(group))
    {
      foreach (string uiElement in uiElementGroup[group])
      {
        SetActive(uiElement, active);
      }
    }
  }
  public void NextUI(string currentUIElement, string nextUIElement)
  {
    SetActive(nextUIElement, true);
    SetActive(currentUIElement, false);
  }
  public void NextUIGroup(string currentUIGroup, string nextUIGroup)
  {
    SetActiveGroup(nextUIGroup, true);
    SetActiveGroup(currentUIGroup, false);
  }
  public void SetText(string uiElement, string text)
  {
    if (uiElements.ContainsKey(uiElement) && uiElements[uiElement] is TMP_Text)
    {
      uiElements[uiElement].GetComponent<TMP_Text>().text = text;
    }
  }
  public void SetGameModeUI(GameStateManager.GameMode gameMode)
  {
    switch (gameMode)
    {
      case GameStateManager.GameMode.Time:
        SetActive("TimeMode", true);
        break;
      case GameStateManager.GameMode.Word:
        SetActive("WordMode", true);
        break;
      case GameStateManager.GameMode.Endless:
        SetActive("EndlessMode", true);
        break;
    }
  }
}
