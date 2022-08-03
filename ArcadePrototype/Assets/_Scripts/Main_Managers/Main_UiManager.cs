using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class Main_UiManager : MonoBehaviour
{
    [SerializeField] private GameObject _areYouSureScreen;

    [SerializeField] private Button button_NewgameContinue;
    [SerializeField] private Button button_Levels;
    [SerializeField] private Selectable _options;

    [SerializeField] private Button[] _levelButtons;
    [SerializeField] private TMP_Text[] _levelText;

    [SerializeField] private Sprite _button9Slice;
    [SerializeField] private Sprite _buttonLocked9Slice;

    private bool GetHasGameBeenPlayed => PlayerPrefs.GetInt(PlayerPrefsHelper.LEVELSCOMPLETED) > 0;

    private void Awake()
    {
        GetTexts();
    }

    private void Start()
    {
        HandleNewGameContinueLevels();

        HandleIfButtonInteractble();

        HandleLevelButtonNavigation();
    }

    private void HandleNewGameContinueLevels()
    {
        if (GetHasGameBeenPlayed)
        {
            button_Levels.interactable = true;
            Navigation newNav = button_NewgameContinue.navigation;
            newNav.selectOnDown = button_Levels;
            button_NewgameContinue.navigation = newNav;
            SwitchNewGameToContinue();
        }
        else
        {
            button_Levels.interactable = false;
            Navigation newNav = button_NewgameContinue.navigation;
            newNav.mode = Navigation.Mode.Explicit;
            newNav.selectOnDown = _options;
            button_NewgameContinue.navigation = newNav;
            SwitchContinueToNewGame();
        }
    }

    private void SwitchNewGameToContinue()
    {
        TMP_Text continueTXT = button_NewgameContinue.GetComponentInChildren<TMP_Text>();
        continueTXT.text = "Continue";
    }

    private void SwitchContinueToNewGame()
    {
        TMP_Text continueTXT = button_NewgameContinue.GetComponentInChildren<TMP_Text>();
        continueTXT.text = "New Game";
    }

    private void GetTexts()
    {
        _levelText = new TMP_Text[_levelButtons.Length];
        for (int i = 0; i < _levelButtons.Length; i++)
        {
            _levelText[i] = _levelButtons[i].GetComponentInChildren<TMP_Text>();
        }
    }

    private void HandleIfButtonInteractble()
    {
        for (int i = 0; i < _levelButtons.Length; i++)
        {
            _levelButtons[i].interactable = false;
            _levelButtons[i].image.sprite = _buttonLocked9Slice;
            _levelText[i].text = "";
        }

        int levelsCompleted = PlayerPrefs.GetInt(PlayerPrefsHelper.LEVELSCOMPLETED);
        for (int i = 0; i < levelsCompleted; i++)
        {
            _levelButtons[i].interactable = true;
            _levelButtons[i].image.sprite = _button9Slice;
            _levelText[i].text = "Level " + (i + 1).ToString();
        }
    }

    private void HandleLevelButtonNavigation()
    {
        for (int i = 0; i < _levelButtons.Length; i++)
        {
            Navigation newNav = _levelButtons[i].navigation;
            if (i == 0 || i == 5 || i == 10)
            {
                newNav.selectOnLeft = button_Levels;
            }
            else
            {
                if (IPlus1(i - 1))
                    newNav.selectOnLeft = _levelButtons[i - 1];
            }
            if (IPlus1(i))
                newNav.selectOnRight = _levelButtons[i + 1];
            if (IPlus5(i))
                newNav.selectOnDown = _levelButtons[i + 5];
            if (i >= 5)
                if (IPlus5(i - 5))
                    newNav.selectOnUp = _levelButtons[i - 5];

            _levelButtons[i].navigation = newNav;

        }
    }

    private bool IPlus1(int i)
    {
        return PlayerPrefs.GetInt(PlayerPrefsHelper.LEVELSCOMPLETED) > i + 1;
    }

    private bool IPlus5(int i)
    {
        return PlayerPrefs.GetInt(PlayerPrefsHelper.LEVELSCOMPLETED) > i + 5;
    }

    public void ButtonNewGameContinue()
    {
        //NewGame
        if (!GetHasGameBeenPlayed)
        {
            string sceneIndex = Main_Levels.World11.ToString();
            SceneManager.LoadScene(sceneIndex);
        }
        //Continue
        else
        {
            int currentLevel = PlayerPrefs.GetInt(PlayerPrefsHelper.LEVELSCOMPLETED);
            Main_Levels castToEnum = (Main_Levels)currentLevel;
            string sceneIndex = castToEnum.ToString();
            SceneManager.LoadScene(sceneIndex);
        }
    }

    public void ButtonShowAreYouSure(Button button)
    {
        _areYouSureScreen.SetActive(true);
        button.Select();
    }

    public void ButtonUNShowAreYouSure(Button button)
    {
        _areYouSureScreen.SetActive(false);
        button.Select();
    }

    public void ButtonSwitchObjectActive(GameObject gameObject)
    {
        bool state = gameObject.activeInHierarchy;
        gameObject.SetActive(!state);
    }

    public void ButtonLoadLevel(Main_Levels levels)
    {

    }

    public void ButtonDeleteSave()
    {
        PlayerPrefsHelper.DeleteSave();
        HandleNewGameContinueLevels();
    }

    public void ButtonRestoreDefaultSettings()
    {
        PlayerPrefsHelper.RestoreDefaultSettings();
    }
}
