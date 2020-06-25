using UnityEngine;

public class LevelSelectHandler : MonoBehaviour
{
    public Object menuScene;
    
    public void OnLevelClick(int index)
    {
        MenuHandler menu = GameObject.FindGameObjectWithTag("Menu").GetComponent<MenuHandler>();
        string sceneToLoad = menu.levelScenes[index].name;
        GameObject.FindGameObjectWithTag("Fader").GetComponent<ScreenFader>().Fade(this, sceneToLoad);
    }

    public void OnBackClick()
    {
        GameObject.FindGameObjectWithTag("Fader").GetComponent<ScreenFader>().Fade(this, menuScene.name);
    }
}
