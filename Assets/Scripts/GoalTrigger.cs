using System;
using UnityEngine;

public class GoalTrigger : MonoBehaviour
{
    public UnityEngine.Object menuScene;

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.tag == "Player") Debug.Log("Player completed the level.");

        int currLevel = PlayerPrefs.GetInt("CurrentLevel");
        
        MenuHandler menu = null;
        
        try {
            menu = GameObject.FindGameObjectWithTag("Menu").GetComponent<MenuHandler>();
        }
        catch (NullReferenceException e) {
            Debug.LogWarning(e);
            GameObject.FindGameObjectWithTag("Fader").GetComponent<ScreenFader>().Fade(this, menuScene.name);
            return;
        }

        if (currLevel + 1 > menu.levelScenes.Length - 1)
        {
            GameObject.FindGameObjectWithTag("Fader").GetComponent<ScreenFader>().Fade(this, menuScene.name);
        }
        else 
        {
            PlayerPrefs.SetInt("CurrentLevel", currLevel + 1);
            menu.OnPlayClick();
        }
    }
}
