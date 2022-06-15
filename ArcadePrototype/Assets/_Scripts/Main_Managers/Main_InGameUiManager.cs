using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class Main_InGameUiManager : MonoBehaviour
{
    [SerializeField] private GameObject _pauseScreen;

    [SerializeField] private Image _staminaBar;
    [SerializeField] private Image _healthBar;

    [SerializeField] private TMP_Text _fps;
    [SerializeField] private TMP_Text txt_time;
    [SerializeField] private TMP_Text txt_DeathCount;

    [SerializeField] private PlayerHitBox _player;

    public static Main_InGameUiManager Instance;

    private PersistentTimer _persistentTimer;
    private PersistentDeathCount _persistentDeathCount;

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
        _persistentTimer?.EnableMe();
        _persistentDeathCount = PersistentDeathCount.Instance;
        _persistentDeathCount?.UpdateDeathCount();
        Time.timeScale = 1;
    }

    private void Update()
    {
        FpsCounter();
        TimeToDisplay();
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

    #endregion
}
