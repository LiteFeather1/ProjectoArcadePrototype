using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class IG_GameManager : MonoBehaviour
{ 
    private int _lifes = 3;
    private int _coins;
    [SerializeField] private int _maxCoins;
    private float _timeSecs;
    [SerializeField] private int _trophyTimeSecs;

    [SerializeField] private int _nextLevel; 


    public float TimeSecs { get => _timeSecs; }
    public float TrophyTime { get => _trophyTimeSecs; }
    #region Events

    #endregion

    private static IG_GameManager _instance;

    public static IG_GameManager Instance { get => _instance; set => _instance = value; }
    public int NextLevel { get => _nextLevel;}

    #region MonoBehavioursMethods
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
    }
    private void Start()
    {
        IG_UiManager.Instance.CoinsToDisplay(_coins, _maxCoins);
    }

    private void Update()
    {
        Timer();
    }

    #endregion

    private void Timer()
    {
        if (!IG_UiManager.Instance.PauseScreenSwitch)
        {
            _timeSecs += Time.deltaTime;

            IG_UiManager.Instance.TxtTimeToDisplay(_timeSecs, _trophyTimeSecs);
        }
    }

    public void ColletingCoins()
    {
        _coins++;
        IG_UiManager.Instance.CoinsToDisplay(_coins, _maxCoins);
    }

    public void HarryGotSpiked()
    {
        _lifes--;
        IG_UiManager.Instance.LifesToDisplay(_lifes);

        if(_lifes == 0)
        {

        }
    }
}
