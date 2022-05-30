using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Main_UiManager : MonoBehaviour
{
    [SerializeField] private Image _staminaBar;
    [SerializeField] private Image _healthBar;


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

    public void StaminaBarToDisplay(float currentStamina, float maxStamina)
    {
        _staminaBar.fillAmount = currentStamina / maxStamina;
    }

    public void HealthToDisplay(float currentHp, float maxHp)
    {
        _healthBar.fillAmount = currentHp / maxHp;
    }
}
