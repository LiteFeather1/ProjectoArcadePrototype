using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Main_LevelSelectionManager : MonoBehaviour
{
    [SerializeField] private Button[] _levelButtons;
    [SerializeField] private TMP_Text[] _buttonText;

    private void Awake()
    {
        GetTexts();
    }

    private void Start()
    {
        HandleIfButtonInteractble();
    }

    private void GetTexts()
    {
        _buttonText = new TMP_Text[_levelButtons.Length];
        for (int i = 0; i < _levelButtons.Length; i++)
        {
            _buttonText[i] = _levelButtons[i].GetComponentInChildren<TMP_Text>();
        }
    }

    private void HandleIfButtonInteractble()
    {
        for (int i = 0; i < _levelButtons.Length; i++)
        {
            _levelButtons[i].interactable = false;
            _buttonText[i].text = "LOCKED";
        }

        int levelsCompleted = PlayerPrefs.GetInt(PlayerPrefsHelper.LEVELSCOMPLETED);
        for (int i = 0; i < levelsCompleted; i++)
        {
            _levelButtons[i].interactable = true;
            _buttonText[i].text = "Level " + (i + 1).ToString();
        }
    }
}
