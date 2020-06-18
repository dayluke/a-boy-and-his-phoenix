using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuHandler : MonoBehaviour
{
    public UnityEngine.Object[] levelScenes;
    public UnityEngine.Object levelSelectScene;

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
        Debug.Log("Loading level " + currLevel);
        SceneManager.LoadScene(levelScenes[currLevel].name);
        
    }
    
    public void OnLevelsClick()
    {
        //SceneManager.LoadScene(levelSelectScene);
        throw new NotImplementedException();
    }

    public void OnExitClick()
    {
    #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
    #else
        Application.Quit();
    #endif
    }

}
