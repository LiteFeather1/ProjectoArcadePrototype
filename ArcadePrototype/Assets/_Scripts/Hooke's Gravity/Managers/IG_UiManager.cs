using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class IG_UiManager : MonoBehaviour
{
    [Header("Displayer")]
    [SerializeField] private TMP_Text _lifes;
    [SerializeField] private TMP_Text _coins, _egCoins, _time, _egTime;

    [Header("")]
    [SerializeField] private GameObject _optionsScreen;
    [SerializeField] private GameObject _pauseScreen, _levelCompleted;
    private bool _pauseScreenSwitch, _optionsSwitch;
    private bool _levelEnded;

    private static IG_UiManager _instance;
    public static IG_UiManager Instance { get => _instance; set => _instance = value; }
    public bool PauseScreenSwitch { get => _pauseScreenSwitch;}

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
        _pauseScreenSwitch = false;
    }

    private void Update()
    {
        if (!_levelEnded)
        {
            KeyPauseAndUnpause();
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            ButtonRestart();
        }
    }

    #region Displays
    public void LifesToDisplay(int lifes)
    {
        _lifes.text = lifes.ToString();
    }

    public void CoinsToDisplay(int coins, int totalCoins)
    {
        _coins.text = coins.ToString("00") + "/" + totalCoins.ToString("00");
        _egCoins.text = coins.ToString("00") + "/" + totalCoins.ToString("00");

        if (coins == totalCoins)
        {
            Color32 lightGreen = Pico8Colours.Green;
            _coins.color = lightGreen;
            _egCoins.color = lightGreen;
            //EndGameLogic();
        }
    }

    public void TxtTimeToDisplay(float time, float trophyTime)
    {
        float minutes = Mathf.FloorToInt(time / 60);
        float seconds = (time % 60);

        _time.text = string.Format("{0:00}:{1:00.00}", minutes, seconds);
        _egTime.text = string.Format("{0:00}:{1:00.00}", minutes, seconds);

        if (time > trophyTime)
        {
            Color32 red = Pico8Colours.Red;
            _time.color = red;
            _egTime.color = red;
        }
    }
    #endregion

    public void EndGameLogic()
    {
        if( IG_GameManager.Instance.TimeSecs < IG_GameManager.Instance.TrophyTime)
        {
            Color32 lightGreen = new Color32(0, 228, 54, 255);
            _time.color = lightGreen;
            _egTime.color = lightGreen;
        }

        _pauseScreenSwitch = true;
        _levelEnded = true;

        _levelCompleted.SetActive(true);
    }
    public void LoseGameLogic()
    {

        _pauseScreenSwitch = true;
        _levelEnded = true;
    }

    private void KeyPauseAndUnpause()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            _pauseScreenSwitch = !_pauseScreenSwitch;
            _pauseScreen.SetActive(_pauseScreenSwitch);
            if (_optionsSwitch)
            {
                ButtonOptions();
            }
        }
    }
    #region Buttons
    public void ButtonResume()
    {
        _pauseScreenSwitch = !PauseScreenSwitch;
        _pauseScreen.SetActive(PauseScreenSwitch);
    }

    public void ButtonRestart()
    {
        DestroyInstances();
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }

    public void ButtonOptions()
    {
        _optionsSwitch = !_optionsSwitch;
        _optionsScreen.SetActive(_optionsSwitch);
    }

    public void ButtonExit()
    {
        DestroyInstances();
        SceneManager.LoadScene("MainMenu");
    }

    #endregion

    private void DestroyInstances()
    {
        Destroy(gameObject);
        Destroy(IG_GameManager.Instance.gameObject);
    }
}
