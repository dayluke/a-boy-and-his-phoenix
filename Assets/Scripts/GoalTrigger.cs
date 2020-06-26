using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoalTrigger : MonoBehaviour
{
    public AudioClip winSound;
    public ScreenFader screenFader;

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.tag == "Player") Debug.Log("Player completed the level.");

        int currLevel = PlayerPrefs.GetInt("CurrentLevel");
        GameObject.FindGameObjectWithTag("Music").GetComponent<AudioSource>().PlayOneShot(winSound);

        if (currLevel + 2 > SceneManager.sceneCountInBuildSettings - 2)
        {
            screenFader.Fade(this, 0);
        }
        else 
        {
            currLevel++;
            PlayerPrefs.SetInt("CurrentLevel", currLevel);
            
            try {
                Debug.Log("Loading level " + currLevel);
                screenFader.Fade(this, currLevel + 2);
            }
            catch (IndexOutOfRangeException e) {
                Debug.LogWarning(e);
                currLevel = 0;
                PlayerPrefs.SetInt("CurrentLevel", currLevel);
                screenFader.Fade(this, 0);
            }
        }
    }
}
