using System;
using UnityEngine;

public class MenuHandler : MonoBehaviour
{
    public ScreenFader screenFader;

    public void OnPlayClick()
    {
        int currLevel = 0;
        if (PlayerPrefs.HasKey("CurrentLevel"))
        {
            currLevel = PlayerPrefs.GetInt("CurrentLevel");
        }
        else
        {
            PlayerPrefs.SetInt("CurrentLevel", currLevel);
        }

        try {
            Debug.Log("Loading level " + currLevel);
            screenFader.Fade(this, currLevel + 2);
        }
        catch (IndexOutOfRangeException e) {
            Debug.LogWarning(e);
            currLevel = 0;
            PlayerPrefs.SetInt("CurrentLevel", currLevel);
            screenFader.Fade(this, currLevel + 2);
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
