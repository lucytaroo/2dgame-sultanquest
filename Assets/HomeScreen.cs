using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class HomeScreen : MonoBehaviour
{
    public static HomeScreen instance;

    public void PlayGame()
    {
        SceneManager.LoadScene("SampleScene");
    }

    

    public void SettingMenu()
    {
        SceneManager.LoadScene("SettingsMenu");
        Time.timeScale = 1f;
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Game Quit");
    }
    
}
