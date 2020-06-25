using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScreenFader : MonoBehaviour
{
    public float fadeTime = 1;
    public Animator animator;

    public void Fade() => animator.SetTrigger("fade");
    public void Fade(MonoBehaviour instance, string sceneToLoad) => instance.StartCoroutine(Fade(sceneToLoad));

    private IEnumerator Fade(string sceneToLoad)
    {
        animator.SetTrigger("fade");    
        yield return new WaitForSeconds(fadeTime);
        SceneManager.LoadScene(sceneToLoad);
    }
}
