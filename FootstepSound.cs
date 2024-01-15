using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootstepSound : MonoBehaviour
{
    public AudioSource audioSource;

    [Header("Footstep sources")]
    [SerializeField] private AudioClip[] footStepSound;

    private void Awake()
    {
        audioSource.GetComponent<AudioSource>();
    }

    private AudioClip GetRandomFootStep()
    {
        return footStepSound[UnityEngine.Random.Range(0, footStepSound.Length)];
    }
    private void Step()
    {
        AudioClip clip = GetRandomFootStep();
        audioSource.PlayOneShot(clip);
    }
}
