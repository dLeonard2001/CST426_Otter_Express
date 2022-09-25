using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MusicVolumeController : MonoBehaviour
{
    [SerializeField] private AudioMixer myMusicMixer;

    public void SetVolume(float sliderValue)
    {
        myMusicMixer.SetFloat("MusicVolume", Mathf.Log10(sliderValue) * 20);
    }
}
