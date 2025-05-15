using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathHandler : MonoBehaviour
{
    public CharacterMovement player;

    private bool hasTriggered = false;

    void Update()
    {
        if (player.IsDead && !hasTriggered)
        {
            hasTriggered = true;
            StartCoroutine(ReturnToTitleAfterDelay());
        }
    }

    IEnumerator ReturnToTitleAfterDelay()
    {
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("Title");
    }
}