using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraPan : MonoBehaviour
{
    [SerializeField] private float _maxYOffSet;
    [SerializeField] private float _speed;
    private float _yLerp;
    private float _xLerp;
    private float _time;
    private float _yInput;
    private float _lastFrameInput;

    [SerializeField] private CinemachineCameraOffset _cineCamOffSet;
    [SerializeField] private Detections _playerDetection;
    private void Update()
    {
        _yInput = Input.GetAxisRaw("Vertical");

        if (Time.timeScale < 0)
            return;
        if (_yInput > 0 && CanPan())
        {
            _time += Time.deltaTime;
            _yLerp = Mathf.Lerp(_yLerp, _maxYOffSet, _time * _speed);
        }
        else if (_yInput < 0 && CanPan())
        {
            _time += Time.deltaTime;
            _yLerp = Mathf.Lerp(_yLerp, -_maxYOffSet, _time * _speed);
        }

        else if (_yInput == 0)
        {
            _time += Time.deltaTime;
            _yLerp = Mathf.Lerp(_yLerp, 0, _time * _speed);
        }
        _cineCamOffSet.m_Offset = new Vector3(_xLerp, _yLerp);
        LastFramInput();

    }
     
    private bool CanPan()
    {
        return !_playerDetection.IsOnWall() && _playerDetection.IsGrounded();
    }

    private void LastFramInput()
    {
        if(_lastFrameInput != _yInput)
        {
            _time = 0;
        }
        _lastFrameInput = _yInput;
    }
}
