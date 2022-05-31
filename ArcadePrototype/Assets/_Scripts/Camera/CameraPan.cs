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

    [SerializeField] private CinemachineCameraOffset _cineCamOffSet;
    [SerializeField] private Detections _playerDetection;
    private void Update()
    {
        float yInput = Input.GetAxisRaw("Vertical");

        if (yInput > 0 && CanPan())
        {
            _time += Time.deltaTime;
            _yLerp = Mathf.Lerp(_yLerp, _maxYOffSet, _time * _speed);
        }
        else if(yInput < 0 && CanPan())
        {
            _time += Time.deltaTime;
            _yLerp = Mathf.Lerp(_yLerp, -_maxYOffSet, _time * _speed);
        }

        else if (yInput == 0)
        {
            _time += Time.deltaTime;
            _yLerp = Mathf.Lerp(_yLerp, 0, _time * _speed);
        }
        if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.DownArrow) || Input.GetKeyUp(KeyCode.UpArrow)) _time = 0;

        _cineCamOffSet.m_Offset = new Vector3(_xLerp, _yLerp);
    }
     
    private bool CanPan()
    {
        return !_playerDetection.IsOnWall() && _playerDetection.IsGrounded();
    }
}
