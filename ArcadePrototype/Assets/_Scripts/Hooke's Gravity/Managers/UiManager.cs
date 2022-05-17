using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UiManager : MonoBehaviour
{
    [SerializeField] private GameObject _cover, _popUp;
    [SerializeField] private GameObject[] _pages;
    private int _currentPage;

    private static UiManager _instance;

    public static UiManager Instance { get => _instance; set => _instance = value; }

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

    private void Update()
    {
        ChangePageKey();
    }

    #region Buttons
    private void ButtonBase(int page)
    {
        _cover.SetActive(false);
        foreach (var item in _pages)
        {
            item.SetActive(false);
        }
        _currentPage = page;
        _pages[_currentPage].SetActive(true);
    }
    public void ButtonOpenBook()
    {
        ButtonBase(1);
    }

    public void ButtonCloseBook()
    {
        foreach (var item in _pages)
        {
            item.SetActive(false);
        }
        _cover.SetActive(true);
    }

    public void ButtonGoToOptions()
    {
        ButtonBase(0);
    }

    public void ButtonAddPage()
    {
        _cover.SetActive(false);
        _pages[_currentPage].SetActive(false);
        _currentPage++;
        _pages[_currentPage].SetActive(true);
    }

    public void ButtonSubPage()
    {
        _cover.SetActive(false);
        _pages[_currentPage].SetActive(false);
        _currentPage--;
        _pages[_currentPage].SetActive(true);
    }

    private void ChangePageKey()
    {
        if(Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if(_currentPage > 0)
            {
                ButtonSubPage();        
            }
        }

        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (_currentPage < _pages.Length - 1)
            {
                ButtonAddPage();
            }
        }
    }
    public void ButtonGoToCredits()
    {
        ButtonBase(_pages.Length - 1);
    }

    public void ButtonGoToContents()
    {
        ButtonBase(1);
    }

    public void ButtonGoToPage1()
    {
        ButtonBase(2);
    }
    public void ButtonGoToPage2()
    {
        ButtonBase(3);
    }
    public void ButtonGoToPage3()
    {
        ButtonBase(4);
    }
    public void ButtonGoToPage4()
    {
        ButtonBase(5);
    }
    public void ButtonGoToPage5()
    {
        ButtonBase(6);
    }
    public void ButtonGoToPage6()
    {
        ButtonBase(7);
    }

    public void ButtonNewGame()
    {
        _popUp.SetActive(true);
    }

    public void ButtonClosePopUpYes()
    {
        ButtonBase(2);
        _popUp.SetActive(false);
    }

    public void ButtonClosePopUpNo()
    {
        _popUp.SetActive(false);
    }

    public void ButtonQuit()
    {
        Application.Quit();
        print("SeeYah");
    }
    #endregion
}
