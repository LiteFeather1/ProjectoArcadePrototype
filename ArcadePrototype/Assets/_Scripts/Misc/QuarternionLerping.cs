using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuarternionLerping : MonoBehaviour
{
    [Header ("Axis")]
    [SerializeField] Vector3 _newRotation;

    [Header("Times")]
    [SerializeField] private float _timeToPass;
    private float _timePassed;
    private float _t;
    [SerializeField] private float _speed;
    private int _sign = 1;

    private Vector3 _rotationOffset;

    private void Start()
    {
        _rotationOffset = _newRotation / 2;
        _rotationOffset.z = Random.Range(_rotationOffset.z / 2, _rotationOffset.z);
    }

    private void Update()
    {
        _timePassed += Time.deltaTime * _speed * _sign;
        _t = _timePassed / _timeToPass;

        if (_timePassed >= _timeToPass) _sign = _sign * -1;
        else if (_timePassed <= 0) _sign = _sign * -1;

        transform.rotation =  Quaternion.Lerp(Quaternion.Euler(-_newRotation - _rotationOffset), Quaternion.Euler(_newRotation + _rotationOffset), _t);
    }
}
