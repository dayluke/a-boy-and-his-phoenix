using UnityEngine;

public class GoalTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.tag == "Player") Debug.Log("Player completed the level.");
        Time.timeScale = 0.1f;
    }
}
