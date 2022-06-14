using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Main_InGameUiManager : MonoBehaviour
{
    [SerializeField] private Image _staminaBar;
    [SerializeField] private Image _healthBar;

    [SerializeField] private TMP_Text _fps;
    [SerializeField] private TMP_Text txt_time;
    [SerializeField] private TMP_Text txt_DeathCount;

    public static Main_InGameUiManager Instance;

    private PersistentTimer _persistentTimer;

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
        PersistentDeathCount.Instance?.UpdateDeathCount();
    }

    private void Update()
    {
        FpsCounter();
        TimeToDisplay();
    }

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

        txt_time.text = "Timer : " + string.Format("{0:00}:{1:00.00}", minutes, seconds);
    }

    public void DeathCountToDisplay(int deathCount)
    {
        txt_DeathCount.text = "Death Count : " + deathCount.ToString();
    }
}
