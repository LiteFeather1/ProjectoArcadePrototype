using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MachineLoadLevel : MonoBehaviour, IIteractable
{
    [SerializeField] private Main_Levels _levels;
    [SerializeField] private string _levelToLoad;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
            ToInteract();
        if (Input.GetKeyDown(KeyCode.M))
            LevelsCompleted();
    }

    public void ToInteract()
    {
        LoadLevel();
    }

    private void LoadLevel()
    {
        if (_levels != Main_Levels.None)
        {
            SceneManager.LoadScene(_levels.ToString());
            LevelsCompleted();
        }
        else
        {
            SceneManager.LoadScene(_levelToLoad);
        }
    }

    private void LevelsCompleted()
    {
        PlayerPrefsHelper.SaveLevelsCompleted(_levels);  
    }
}



public enum Main_Levels
{
    None,
    World11,
    DoubleJump,
    WallJump,
    WallClimb,
    Dash
}
