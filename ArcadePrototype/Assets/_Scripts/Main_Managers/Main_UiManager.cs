using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Main_UiManager : MonoBehaviour
{
    [SerializeField] private Image _staminaBar;
    [SerializeField] private Image _healthBar;

    [SerializeField] private TMP_Text _fps;


    public static Main_UiManager Instance;

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

    private void Update()
    {
        FpsCounter();
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
}
