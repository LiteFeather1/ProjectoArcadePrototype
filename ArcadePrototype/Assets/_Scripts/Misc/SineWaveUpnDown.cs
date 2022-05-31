using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SineWaveUpnDown : MonoBehaviour
{
    [SerializeField] private float _distanceY;
    private Vector3 _newPosition;

    private bool _canMove = true;

    private void Start() => _distanceY = Random.Range(_distanceY / 3, _distanceY);

    private void Update()
    {
        if (_canMove)
        {
            _newPosition = transform.position;
            _newPosition.y += Mathf.Sin(Time.time) * Time.deltaTime * _distanceY;
            transform.position = _newPosition;
        }
    }
}
