using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveryorBelt : MonoBehaviour
{
    [Header("X Axis")]
    [SerializeField] private float _normalSpeedX;
    [SerializeField] private float _inDirectionX;
    [SerializeField] private float _inverseX;

    [Header("Y Axis")]
    [SerializeField] private float _normalSpeedY;
    [SerializeField] private float _inDirectionY;
    [SerializeField] private float _inverseY;

    public float NormalSpeedX { get => _normalSpeedX; }
    public float InDirectionX { get => _inDirectionX; }
    public float InverseX { get => _inverseX; }
    public float NormalSpeedY { get => _normalSpeedY; }
    public float InDirectionY { get => _inDirectionY; }
    public float InverseY { get => _inverseY; }
}
