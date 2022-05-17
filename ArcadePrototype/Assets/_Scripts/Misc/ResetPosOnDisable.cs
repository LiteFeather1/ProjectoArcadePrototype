using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetPosOnDisable : MonoBehaviour
{
    private Vector3 _startPos;

    private void Awake()
    {
        _startPos = transform.position;
    }

    private void OnDisable()
    {
        transform.position = _startPos;
    }
}
