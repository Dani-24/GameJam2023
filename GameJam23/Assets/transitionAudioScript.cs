using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class transitionAudioScript : MonoBehaviour
{
    [SerializeField] AudioSource source;

    [SerializeField] AudioClip fadeIn;
    [SerializeField] AudioClip fadeOut;

    public float minPitch = 1.0f;
    public float maxPitch = 1.05f;

    public void FadeInAudio()
    {
        float randomPitch = Random.Range(minPitch, maxPitch);
        source.pitch = randomPitch;

        source.clip = fadeIn;
        source.Play();
    }

    public void FadeOutAudio()
    {
        float randomPitch = Random.Range(minPitch, maxPitch);
        source.pitch = randomPitch;

        source.clip = fadeOut;
        source.Play();
    }
}
