using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScreenFader : MonoBehaviour
{
    public float fadeTime = 1;
    public Animator animator;

    public void Fade() => animator.SetTrigger("fade");
    public void Fade(MonoBehaviour instance, int buildIndex) => instance.StartCoroutine(Fade(buildIndex));

    private IEnumerator Fade(int buildIndex)
    {
        animator.SetTrigger("fade");    
        yield return new WaitForSeconds(fadeTime);
        SceneManager.LoadScene(buildIndex);
    }
}
