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


[System.Serializable]
public enum Main_Levels
{
    None,
    World11,
    Main_Leve_l2,
    Main_Leve_l3,
    DoubleJump,
    Main_Level_4,
    Main_Level_5,
    WallJump,
    Main_Level_7,
    Main_Level_8,
    WallClimb,
    Main_Level_9,
    Main_Level_10,
    Dash,
    Main_Level_11,
    Main_Level_12
}
