using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System;

public class Main_InGameUiManager : MonoBehaviour
{
    [Header ("UI Frames")]
    [SerializeField] private GameObject _pauseScreen;

    [Header ("Player HUD")]
    [SerializeField] private Image _staminaBar;
    [SerializeField] private Image _healthBar;

    [Header ("TMP Texts")]
    [SerializeField] private TMP_Text _fps;
    [SerializeField] private TMP_Text txt_time;
    [SerializeField] private TMP_Text txt_DeathCount;
    [SerializeField] private TMP_Text txt_Score;

    [SerializeField] private TMP_Text txt_MusicVolume;
    [SerializeField] private TMP_Text txt_SFXVolume;

    [Header ("Toggles")]
    [SerializeField] private Toggle toggle_ShowTime;
    [SerializeField] private Toggle toggle_ShowDeaths;
    [SerializeField] private Toggle toggle_ShowScore;
    [SerializeField] private Toggle toggle_AssistMode;
    private bool _antiToggleOnStart;

    [Header ("Player")]
    [SerializeField] private PlayerHitBox _player;

    private Action _musicVolumeChanged;
    private Action _sfxVolumeChanged;

    public static Main_InGameUiManager Instance;

    private PersistentTimer _persistentTimer;
    private PersistentDeathCount _persistentDeathCount;
    private PersistentScore _persistentScore;

    public Action MusicVolumeChanged { get => _musicVolumeChanged; set => _musicVolumeChanged = value; }
    public Action SfxVolumeChanged { get => _sfxVolumeChanged; set => _sfxVolumeChanged = value; }

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        _persistentTimer = PersistentTimer.Instance;
        if (_persistentTimer != null)
            _persistentTimer.EnableMe();

        _persistentDeathCount = PersistentDeathCount.Instance;
        if(_persistentDeathCount != null)
         _persistentDeathCount.UpdateDeathCount();

        _persistentScore = PersistentScore.Instance;
        Time.timeScale = 1;


        UpdateToggles();
        UpdateAllTXTs();
    }

    private void Update()
    {
        FpsCounter();
        TimeToDisplay();
        ScoreToDisplay();

        PauseGameInput();
    }

    #region Displays
    public void StaminaBarToDisplay(float currentStamina, float maxStamina)
    {
        _staminaBar.fillAmount = currentStamina / maxStamina;
    }

    public void HealthToDisplay(float currentHp, float maxHp)
    {
        _healthBar.fillAmount = currentHp / maxHp;
    }

    private void FpsCounter()
    {
        _fps.text = (1 / Time.unscaledDeltaTime).ToString("00");
    }

    private void TimeToDisplay()
    {
        if (_persistentTimer == null)
            return;
        float minutes = Mathf.FloorToInt(_persistentTimer.Timer / 60);
        float seconds = (_persistentTimer.Timer % 60);

        txt_time.text = "Timer : " + string.Format("{0:00}:{1:00.000}", minutes, seconds);
    }

    public void DeathCountToDisplay(int deathCount)
    {
        txt_DeathCount.text = "Death Count : " + deathCount.ToString();
    }

    private void DisplayPauseScreen()
    {
        if (Time.timeScale == 1)
            _pauseScreen.SetActive(false);
        else
            _pauseScreen.SetActive(true);

    }

    private void ScoreToDisplay()
    {
        if (_persistentScore == null)
            return;
        txt_Score.text = "Score : " + _persistentScore.Score.ToString("0000");
    }
    #endregion

    #region Inputs

    public void PauseGame()
    {
        if (Time.timeScale == 1)
            Time.timeScale = 0;
        else
            Time.timeScale = 1;
        DisplayPauseScreen();
    }

    private void PauseGameInput()
    {
        if(Input.GetButtonDown("PauseGame"))
        {
            PauseGame();
        }
    }
    #endregion

    #region Buttons

    public void ButtonRestart()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }

    public void ButtonLastCheckPoint()
    {
        _player.GoToLastCheckPoint();
        PauseGame();
    }

    public void ButtonExit()
    {
        SceneManager.LoadScene("MainStart");
    }

    public void ButtonQuit()
    {
        Application.Quit();
        Debug.Log("Bye");
    }

    public void ButtonChangeMusicVolume(float changeAmount)
    {
        float f = changeAmount;
        float realChangeAmount = PlayerPrefsHelper.GetMusicVolume() + f;
        realChangeAmount = Mathf.Clamp(realChangeAmount, PlayerPrefsHelper.MinVolume, PlayerPrefsHelper.MaxVolume);
        PlayerPrefsHelper.SetMusicVolume(realChangeAmount);
        UpdateMusicVolumeTXT();
        _musicVolumeChanged?.Invoke();
    }

    public void ButtonChangeSFXVolume(float changeAmount)
    {
        float f = changeAmount;
        float realChangeAmount = PlayerPrefsHelper.GetSFXVolume() + f;
        realChangeAmount = Mathf.Clamp(realChangeAmount, PlayerPrefsHelper.MinVolume, PlayerPrefsHelper.MaxVolume);
        PlayerPrefsHelper.SetSFXVolume(realChangeAmount);
        UpdateSFXVolumeTXT();
        _sfxVolumeChanged?.Invoke();
    }

    public void ToggleTime()
    {
        if(_antiToggleOnStart)
            PlayerPrefsHelper.ToggleTime();
        ShowTime();
    }

    public void ToggleDeaths()
    {
        if(_antiToggleOnStart)
            PlayerPrefsHelper.ToggleDeath();
        ShowDeaths();
    }

    public void ToggleScore()
    {
        if(_antiToggleOnStart)
        PlayerPrefsHelper.ToggleScore();
        ShowScore();
    }

    public void ToggleAssistMode()
    {
        if(_antiToggleOnStart)
            PlayerPrefsHelper.ToggleAssistMode();
    }

    private void UpdateToggles()
    {
        toggle_ShowTime.isOn = PlayerPrefsHelper.GetBool(PlayerPrefsHelper.SHOWTIME);
        toggle_ShowDeaths.isOn = PlayerPrefsHelper.GetBool(PlayerPrefsHelper.SHOWDEATHS);
        toggle_ShowScore.isOn = PlayerPrefsHelper.GetBool(PlayerPrefsHelper.SHOWSCORE);
        toggle_AssistMode.isOn = PlayerPrefsHelper.GetBool(PlayerPrefsHelper.ASSISTMODE);
        _antiToggleOnStart = true;
    }

    private void UpdateAllTXTs()
    {
        UpdateMusicVolumeTXT();
        UpdateSFXVolumeTXT();
        ShowTime();
        ShowDeaths();
        ShowScore();
    }

    private void UpdateMusicVolumeTXT()
    {
        float v = PlayerPrefsHelper.GetMusicVolume() * 100;

        string format;
        if (v < 2)
            format = "0";
        else if (v == 100)
            format = "000";
        else
            format = "00";

        txt_MusicVolume.text = v.ToString(format) + "%";
    }
    
    private void UpdateSFXVolumeTXT()
    {
        float v = PlayerPrefsHelper.GetSFXVolume() * 100;

        string format;
        if (v < 2)
            format = "0";
        else if (v == 100)
            format = "000";
        else
            format = "00";

        txt_SFXVolume.text = v.ToString(format) + "%";
    }

    private void ShowTime()
    {
        txt_time.enabled = PlayerPrefsHelper.GetBool(PlayerPrefsHelper.SHOWTIME);
    }

    private void ShowDeaths()
    {
        txt_DeathCount.enabled = PlayerPrefsHelper.GetBool(PlayerPrefsHelper.SHOWDEATHS);
    }

    private void ShowScore()
    {
        txt_Score.enabled = PlayerPrefsHelper.GetBool(PlayerPrefsHelper.SHOWSCORE);
    }
    #endregion
}
