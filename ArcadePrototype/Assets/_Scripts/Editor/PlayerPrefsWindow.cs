using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class PlayerPrefsWindow : EditorWindow
{
    private bool _showNewPlayerPrefsToSet = false;

    private int _newLevelsCompleted;

    private float _newMusicVolume;
    private float _newSFXVolume;

    private bool _newShowTime;
    private bool _newShowDeaths;
    private bool _newShowScore;
    private bool _newAssistMode;

    string _levelsCompleted = PlayerPrefsHelper.LEVELSCOMPLETED;

    string _musicVolume  = PlayerPrefsHelper.MUSICVOLUME;
    string _sfxVolume = PlayerPrefsHelper.SFXVOLUME;

    string _showTime = PlayerPrefsHelper.SHOWTIME;
    string _showDeaths = PlayerPrefsHelper.SHOWDEATHS;
    string _showScore = PlayerPrefsHelper.SHOWSCORE;
    string _assistMode = PlayerPrefsHelper.ASSISTMODE;

    private void OnGUI()
    {
        ShowMainPlayPrefabs();
        DisplayButtons();
        EditorGUILayout.Space();

        if (!_showNewPlayerPrefsToSet)
        {
            ShowNewPlayerPrefsToSet();
        }
        else
        {
            SetNewPlayerPrefs();
        }
    }

    [MenuItem("Window/PlayerPrefs Window")]
    public static void ShowWindow()
    {
        GetWindow<PlayerPrefsWindow>("Playerprefs Window");
    }
    private void ShowMainPlayPrefabs()
    {
        EditorGUILayout.Space(50);
        EditorGUILayout.IntField(_levelsCompleted, PlayerPrefs.GetInt(_levelsCompleted));
        EditorGUILayout.Space();

        EditorGUILayout.FloatField(_musicVolume, PlayerPrefs.GetFloat(_musicVolume));
        EditorGUILayout.Space();

        EditorGUILayout.FloatField(_sfxVolume, PlayerPrefs.GetFloat(_sfxVolume));
        EditorGUILayout.Space();

        EditorGUILayout.Toggle(_showTime, PlayerPrefsHelper.GetBool(_showTime));
        EditorGUILayout.Space();

        EditorGUILayout.Toggle(_showDeaths, PlayerPrefsHelper.GetBool(_showDeaths));
        EditorGUILayout.Space();

        EditorGUILayout.Toggle(_showScore, PlayerPrefsHelper.GetBool(_showScore));
        EditorGUILayout.Space();

        EditorGUILayout.Toggle(_assistMode, PlayerPrefsHelper.GetBool(_assistMode));
        EditorGUILayout.Space();
    }

    private void DisplayButtons()
    {
        RestoreAllDefaults();
    }

    private void RestoreAllDefaults()
    {
        if (GUILayout.Button(StringHelper.SpaceBeforeCapitalLetters(nameof(PlayerPrefsHelper.RestoreAllDefaults))))
        {
            PlayerPrefsHelper.RestoreAllDefaults();
        }
    }

    private void ShowNewPlayerPrefsToSet()
    {
        if (GUILayout.Button(StringHelper.SpaceBeforeCapitalLetters(nameof(ShowNewPlayerPrefsToSet))))
        {
            _showNewPlayerPrefsToSet = true;

            _newLevelsCompleted = PlayerPrefs.GetInt(_levelsCompleted);

            _newMusicVolume = PlayerPrefs.GetFloat(_musicVolume);
            _newSFXVolume = PlayerPrefs.GetFloat(_sfxVolume);

            _newShowTime = PlayerPrefsHelper.GetBool(_showTime);
            _newShowDeaths = PlayerPrefsHelper.GetBool(_showDeaths);
            _newShowScore = PlayerPrefsHelper.GetBool(_showScore);
            _newAssistMode = PlayerPrefsHelper.GetBool(_assistMode);
        }
    }

    private void SetNewPlayerPrefs()
    {
        if (GUILayout.Button("Set new PlayerPrefs"))
        {
            _showNewPlayerPrefsToSet = false;

            PlayerPrefs.SetInt(_levelsCompleted, _newLevelsCompleted);

            PlayerPrefsHelper.SetMusicVolume(_newMusicVolume);
            PlayerPrefsHelper.SetSFXVolume(_newSFXVolume);

            PlayerPrefsHelper.SetBool(_showTime, _newShowTime);
            PlayerPrefsHelper.SetBool(_showDeaths, _newShowDeaths);
            PlayerPrefsHelper.SetBool(_showScore, _newShowScore);
            PlayerPrefsHelper.SetBool(_assistMode, _newAssistMode);

            Debug.Log("New PlayerPrefs has been set");
        }

        EditorGUILayout.Space();

        _newLevelsCompleted = EditorGUILayout.IntField("New " + _levelsCompleted, _newLevelsCompleted);
        EditorGUILayout.Space();

        _newMusicVolume = EditorGUILayout.FloatField("New " +_musicVolume, _newSFXVolume);
        EditorGUILayout.Space();

        _newSFXVolume = EditorGUILayout.FloatField("New " + _sfxVolume, _newSFXVolume);
        EditorGUILayout.Space();

        _newShowTime = EditorGUILayout.Toggle("New " + _showTime, _newShowTime);
        EditorGUILayout.Space();

        _newShowDeaths = EditorGUILayout.Toggle("New " + _showDeaths, _newShowDeaths);
        EditorGUILayout.Space();

        _newShowScore = EditorGUILayout.Toggle("New " + _showScore, _newShowScore);
        EditorGUILayout.Space();

        _newAssistMode = EditorGUILayout.Toggle("New " + _assistMode, _newAssistMode);
        EditorGUILayout.Space();
    }
}
