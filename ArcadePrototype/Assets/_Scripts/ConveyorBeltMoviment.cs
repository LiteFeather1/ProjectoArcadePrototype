using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorBeltMoviment : MonoBehaviour
{
    [Header("X Axis")]
    [SerializeField] private float _normalSpeedX;
    [SerializeField] private float _inDirectionX;
    [SerializeField] private float _inverseX;

    [Header("Y Axis")]
    [SerializeField] private float _normalSpeedY;
    [SerializeField] private float _inDirectionY;
    [SerializeField] private float _inverseY;

    private HorizontalMoviment _horizontalMoviment;
    private WallSlidingNClimbing _wallSlidingNClimbing;

    private void Awake()
    {
        _horizontalMoviment = GetComponent<HorizontalMoviment>();
        _wallSlidingNClimbing= GetComponent<WallSlidingNClimbing>();
    }

}
