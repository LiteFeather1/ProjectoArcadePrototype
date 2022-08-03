using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public static class PlayerPrefsHelper
{
    public const string LEVELSCOMPLETED = "Levels Completed";

    public const string MUSICVOLUME = "Music Volume";
    public const string SFXVOLUME = "SFX Volume";

    public const string SHOWTIME = "Show Time";
    public const string SHOWDEATHS = "Show Deaths";
    public const string SHOWSCORE = "Show Score";
    public const string ASSISTMODE = "Assist Mode";

    public static int MaxVolume { get => 1; }
    public static int MinVolume { get => 0; }

    #region Inspector Buttons

    public static void DeleteSave()
    {
        PlayerPrefs.SetInt(LEVELSCOMPLETED, 0);
    }

    public static void RestoreDefaultSettings()
    {
        SetBool(SHOWTIME, true);
        SetBool(SHOWDEATHS, true);
        SetBool(SHOWSCORE, true);
        SetBool(ASSISTMODE, false);
    }

    public static void RestoreDefaultVolume()
    {
        PlayerPrefs.SetFloat(MUSICVOLUME, 0.1f);
        PlayerPrefs.SetFloat(SFXVOLUME, 0.1f);
    }

    public static void RestoreDefault()
    {
        RestoreDefaultSettings();
        RestoreDefaultVolume();
    }

    public static void RestoreAllDefaults()
    {
        DeleteSave();

        RestoreDefaultVolume();

        RestoreDefaultSettings();

        Debug.Log("PlayerPrefs has been restored");
    }

    #endregion

    #region Levels Completed
    public static void SaveLevelsCompleted(Main_Levels main_Levels)
    {
        if (PlayerPrefs.GetInt(LEVELSCOMPLETED) < (int)main_Levels)
        {
            PlayerPrefs.SetInt(LEVELSCOMPLETED, (int)main_Levels);
        }
    }
    #endregion

    #region Music Volume
    public static void SetMusicVolume(float volume)
    {
        PlayerPrefs.SetFloat(MUSICVOLUME, volume);
    }

    public static float GetMusicVolume()
    {
        return PlayerPrefs.GetFloat(MUSICVOLUME);
    }
    #endregion

    #region SFX Volume
    public static void SetSFXVolume(float volume)
    {
        PlayerPrefs.SetFloat(SFXVOLUME, volume);
    }

    public static float GetSFXVolume()
    {
        return PlayerPrefs.GetFloat(SFXVOLUME);
    }
    #endregion

    public static void ToggleTime()
    {
        bool toggle = !GetBool(SHOWTIME);
        SetBool(SHOWTIME, toggle);
        Debug.Log(GetBool(SHOWTIME));
    }

    public static void ToggleDeath()
    {
        bool toggle = !GetBool(SHOWDEATHS);
        SetBool(SHOWDEATHS, toggle);
        Debug.Log(GetBool(SHOWDEATHS));
    }

    public static void ToggleScore()
    {
        bool toggle = !GetBool(SHOWSCORE);
        SetBool(SHOWSCORE, toggle);
        Debug.Log(GetBool(SHOWSCORE));
    }

    public static void ToggleAssistMode()
    {
        bool toggle = !GetBool(ASSISTMODE);
        SetBool(ASSISTMODE, toggle);
        Debug.Log(GetBool(ASSISTMODE));
    }

    public static void SetBool(string key, bool state)
    {
        PlayerPrefs.SetInt(key, state ? 1 : 0);
    }

    public static bool GetBool(string key)
    {        
        return PlayerPrefs.GetInt(key) == 1;
    }
}
