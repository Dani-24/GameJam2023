using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSfxScript : MonoBehaviour
{
    AudioSource source;

    public AudioClip clip;

    private void Start()
    {
        source = GetComponent<AudioSource>();
        source.clip = clip;
    }

    public void PlaySfx()
    {
        source.Play();
    }
}
