using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXVolumeListener : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;


    private void Start()
    {
        UpdateVolume();
    }

    private void OnEnable()
    {
        Main_InGameUiManager.Instance.SfxVolumeChanged += UpdateVolume;
    }

    private void OnDisable()
    {
        Main_InGameUiManager.Instance.SfxVolumeChanged -= UpdateVolume;
    }

    private void UpdateVolume()
    {
        _audioSource.volume = PlayerPrefsHelper.GetSFXVolume();
    }
}
