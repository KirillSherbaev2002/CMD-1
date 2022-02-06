using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class UISetVolume : MonoBehaviour
{
    [SerializeField] AudioMixer mixer;
    

    public void SetLevel(float sliderValue)
    {
        mixer.SetFloat("MenuMusic", Mathf.Log10(sliderValue)*20);
    }

    public void MuteAudio()
    {
        SetLevel(0.0001f);
    }
}