using System;
using UnityEngine;

public class MenuHandler : MonoBehaviour
{
    public UnityEngine.Object[] levelScenes;
    public UnityEngine.Object levelSelectScene;
    private static MenuHandler instance = null;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            instance = this;
        }

        DontDestroyOnLoad(this.gameObject);
    }

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
            GameObject.FindGameObjectWithTag("Fader").GetComponent<ScreenFader>().Fade(this, levelScenes[currLevel].name);
        }
        catch (IndexOutOfRangeException e) {
            Debug.LogWarning(e);
            currLevel = 0;
            PlayerPrefs.SetInt("CurrentLevel", currLevel);
            GameObject.FindGameObjectWithTag("Fader").GetComponent<ScreenFader>().Fade(this, levelScenes[0].name);
        }
        
    }
    
    public void OnLevelsClick()
    {
        GameObject.FindGameObjectWithTag("Fader").GetComponent<ScreenFader>().Fade(this, levelSelectScene.name);
    }

    public void OnExitClick()
    {
        GameObject.FindGameObjectWithTag("Fader").GetComponent<ScreenFader>().Fade();
    #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
    #else
        Application.Quit();
    #endif
    }

}
