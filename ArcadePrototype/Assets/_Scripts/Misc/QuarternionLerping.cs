using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuarternionLerping : MonoBehaviour
{
    [Header("Axis")]
    [SerializeField] private Vector3 _maxRotation;
    [SerializeField] private Vector3 _minRotation;

    [Header("Times")]
    [SerializeField] private float _timeToPass;
    private float _timePassed;
    private float _t;
    [SerializeField] private float _speed;
    private int _sign = 1;

    private void Update()
    {
        _timePassed += Time.deltaTime * _speed * _sign;
        _t = _timePassed / _timeToPass;

        if (_timePassed >= _timeToPass) _sign = _sign * -1;
        else if (_timePassed <= 0) _sign = _sign * -1;

        transform.rotation =  Quaternion.Lerp(Quaternion.Euler(_minRotation), Quaternion.Euler(_maxRotation), _t);
    }
}
