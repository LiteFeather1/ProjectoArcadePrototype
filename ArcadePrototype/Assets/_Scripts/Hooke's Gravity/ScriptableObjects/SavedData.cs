using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

//This was a test that I never finished
[CreateAssetMenu(fileName = "SaveData", menuName = "ScriptableObjects/SavedData", order = 1)]
public class SavedData : ScriptableObject
{
    [SerializeField] private int _levelCompleted;
    [SerializeField] private Test _test;
    public Test NewTest => _test;
}


[Serializable]
public struct Test
{
    public int Lifes;
    public int Coins;
    public float Time;
}