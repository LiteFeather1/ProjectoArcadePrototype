using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("AudioClips")]
    [SerializeField] static AudioSource _effects;
    [SerializeField] static AudioClip _coins, _lighting;

    public static AudioManager Instance;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
        DontDestroyOnLoad(gameObject);
    }
    private void Start()
    {
        _effects = GetComponent<AudioSource>();
        _coins = Resources.Load<AudioClip>("Coins");
        _lighting = Resources.Load<AudioClip>("Lightning1");
    }

    public static void PlaySound(string clip)
    {
        switch (clip)
        {
            case "Coins":
                _effects.PlayOneShot(_coins);
                break;
            case "Lightning1":
                _effects.PlayOneShot(_lighting);
                break;
        }
    }
}
