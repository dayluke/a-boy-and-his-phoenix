using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuHandler : MonoBehaviour
{
    public ScreenFader screenFader;

    public void OnPlayClick()
    {
        int currLevel = 2;
        if (PlayerPrefs.HasKey("CurrentLevel"))
        {
            currLevel = PlayerPrefs.GetInt("CurrentLevel");
        }
        else
        {
            PlayerPrefs.SetInt("CurrentLevel", currLevel);
        }

        if (currLevel >= SceneManager.sceneCountInBuildSettings - 1)
        {
            currLevel = 2;
            PlayerPrefs.SetInt("CurrentLevel", currLevel);
        }

        try {
            Debug.Log("Loading level " + currLevel);
            screenFader.Fade(this, currLevel);
        }
        catch (Exception e) {
            Debug.LogWarning(e);
            currLevel = 2;
            PlayerPrefs.SetInt("CurrentLevel", currLevel);
            screenFader.Fade(this, currLevel);
        }
    }
    
    public void OnLevelsClick()
    {
        screenFader.Fade(this, 1);
    }

    public void OnExitClick()
    {
        screenFader.Fade();
    #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
    #else
        Application.Quit();
    #endif
    }

}
