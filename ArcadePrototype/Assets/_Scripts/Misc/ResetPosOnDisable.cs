using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetPosOnDisable : MonoBehaviour
{
    private Vector3 _startPos;
    private Quaternion _startRotation;

    private void Awake()
    {
        _startPos = transform.position;
        _startRotation = transform.rotation;
    }

    private void OnDisable()
    {
        transform.position = _startPos;
        transform.rotation = _startRotation;
        
    }
}
