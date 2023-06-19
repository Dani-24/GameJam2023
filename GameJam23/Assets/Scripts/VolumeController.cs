using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeController : MonoBehaviour
{
    [SerializeField]
    AudioMixer audioMixer;

    [SerializeField]
    Slider volumeSlider;

    [SerializeField]
    string volumeName;

    AudioSource sfxWhenChangingVolum;

    // Actualizar el valor del Slider al iniciar
    private void Start()
    {
        float outValue;
        audioMixer.GetFloat(volumeName, out outValue);
        volumeSlider.value = Mathf.Pow(10, outValue / 20);

        sfxWhenChangingVolum = GetComponent<AudioSource>();
    }

    public void SetMusicVolume()
    {
        audioMixer.SetFloat(volumeName, Mathf.Log10(volumeSlider.value) * 20);

        if(sfxWhenChangingVolum != null )
        {
            sfxWhenChangingVolum.Play();
        }
    }
}
