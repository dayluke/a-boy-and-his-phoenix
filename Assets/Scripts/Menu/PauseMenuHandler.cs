using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenuHandler : MonoBehaviour
{
    public Object mainMenuScene;
    public InputHandler inputHandler;
    public GameObject pauseMenu;
    public GameObject optionsMenu;

    [Header("Audio Settings")]
    public Image audioImage;
    public Sprite unmuteSymbol;
    public Sprite muteSymbol;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) OnPauseClick();
    }

    public void OnResetClick()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    
    #region settings-buttons

    public void OnPauseClick()
    {
        bool openPauseMenu = inputHandler.inputEnabled;
        pauseMenu.SetActive(openPauseMenu);
        if (!openPauseMenu) optionsMenu.SetActive(openPauseMenu);

        inputHandler.inputEnabled = !openPauseMenu;
    }

    public void OnOptionsClick()
    {
        bool openOptionsMenu = pauseMenu.activeInHierarchy;
        optionsMenu.SetActive(openOptionsMenu);
        pauseMenu.SetActive(!openOptionsMenu);
    }

    public void OnExitClick()
    {
        SceneManager.LoadScene(mainMenuScene.name);
    }

    #endregion

    #region options-buttons

    public void OnAudioClick()
    {
        GameObject music = GameObject.FindGameObjectWithTag("Music");
        music.GetComponent<MusicHandler>().TogglePause();

        // Change sprite icon
        if (music.GetComponent<AudioSource>().isPlaying)
        {
            // change to playing
            audioImage.sprite = unmuteSymbol;
        }
        else
        {
            // change to mute
            audioImage.sprite = muteSymbol;
        }
    }

    #endregion
}
