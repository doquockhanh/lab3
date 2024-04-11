using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadSceneAsync(2);
        Time.timeScale = 1f;
        PlayerPrefs.SetInt("Hp", 10);
    }

    public void Quit() {
        Application.Quit();
    }
}
