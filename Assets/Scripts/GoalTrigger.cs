using System;
using UnityEngine;

public class GoalTrigger : MonoBehaviour
{
    public AudioClip winSound;
    public ScreenFader screenFader;

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.tag == "Player") Debug.Log("Player completed the level.");
        GameObject.FindGameObjectWithTag("Music").GetComponent<AudioSource>().PlayOneShot(winSound);

        int currLevel = PlayerPrefs.GetInt("CurrentLevel");
        currLevel++;
        PlayerPrefs.SetInt("CurrentLevel", currLevel);
        
        try {
            Debug.Log("Loading level " + currLevel);
            screenFader.Fade(this, currLevel);
        }
        catch (Exception e) {
            Debug.LogWarning(e);
            currLevel = 2;
            PlayerPrefs.SetInt("CurrentLevel", currLevel);
            screenFader.Fade(this, 0);
        }
    }
}
