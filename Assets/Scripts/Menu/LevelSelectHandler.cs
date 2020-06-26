using UnityEngine;

public class LevelSelectHandler : MonoBehaviour
{
    public ScreenFader screenFader;
    
    public void OnLevelClick(int index)
    {
        screenFader.Fade(this, index + 2);
    }

    public void OnBackClick()
    {
        screenFader.Fade(this, 0);
    }
}
