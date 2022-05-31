using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SineWaveScale : MonoBehaviour
{
    [SerializeField] private float _scaleX;
    [SerializeField] private float _scaleY;
    private Vector3 _newScale;


    private void Start()
    {
    }
    private void Update()
    {
        _newScale = transform.localScale;
        _newScale.y += Mathf.Sin(Time.time) * Time.deltaTime * _scaleX;
        _newScale.x += Mathf.Sin(Time.time) * Time.deltaTime * _scaleY;
        transform.localScale = _newScale;
    }
}
