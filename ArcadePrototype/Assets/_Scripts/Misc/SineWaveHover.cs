using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SineWaveHover : MonoBehaviour
{
    [SerializeField] private float _distanceY;
    [SerializeField] private float _distanceX;
    private Vector3 _newPosition;

    private bool _canMove = true;

    private void Start()
    {
        _distanceY = Random.Range(_distanceY / 3, _distanceY);
        _distanceX = Random.Range(-_distanceX, _distanceX);
    }
    private void Update()
    {
        if (_canMove)
        {
            _newPosition = transform.position;
            _newPosition.y += Mathf.Sin(Time.time) * Time.deltaTime * _distanceY;
            _newPosition.x += Mathf.Sin(Time.time) * Time.deltaTime * _distanceX;
            transform.position = _newPosition;
        }
    }
}
