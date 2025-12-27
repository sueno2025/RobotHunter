using UnityEngine;

public class SimpleFootStep : MonoBehaviour
{
    public AudioClip footstepClip;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = footstepClip;
    }

    public void FootStep()
    {
        audioSource.Play();
    }

    public void EndOfWalk()
    {
        Debug.Log("[SimpleFootStep] EndOfWalk triggered");
    }
}
