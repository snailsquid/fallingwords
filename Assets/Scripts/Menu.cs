using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public static void ModetoTheme()
    {
        SceneManager.LoadSceneAsync(2);
    }
}
