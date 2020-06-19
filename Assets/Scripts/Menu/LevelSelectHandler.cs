using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelectHandler : MonoBehaviour
{
    public Object menuScene;
    
    public void OnLevelClick(int index)
    {
        MenuHandler menu = GameObject.FindGameObjectWithTag("Menu").GetComponent<MenuHandler>();
        string sceneToLoad = menu.levelScenes[index].name;
        SceneManager.LoadScene(sceneToLoad);
    }

    public void OnBackClick()
    {
        SceneManager.LoadScene(menuScene.name);
    }
}
