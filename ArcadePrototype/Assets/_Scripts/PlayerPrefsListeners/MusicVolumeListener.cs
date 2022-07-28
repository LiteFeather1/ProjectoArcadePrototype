using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicVolumeListener : MonoBehaviour
{
    [SerializeField] private AudioSource _mySource;

    private void Start()
    {
        UpdateVolume();
    }

    private void OnEnable()
    {
        Main_InGameUiManager.Instance.MusicVolumeChanged += UpdateVolume;
    }

    private void OnDisable()
    {
        Main_InGameUiManager.Instance.MusicVolumeChanged -= UpdateVolume;
    }

    private void UpdateVolume()
    {
        _mySource.volume = PlayerPrefsHelper.GetMusicVolume();
    }
}
