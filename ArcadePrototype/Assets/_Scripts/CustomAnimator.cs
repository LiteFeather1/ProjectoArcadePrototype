using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomAnimator : MonoBehaviour
{
    [SerializeField] private Sprite[] _sprites;
    [SerializeField] private bool _looping;
    [SerializeField] private float _sample;
    [SerializeField] private float _speed = 1;
    private float _speedToPlay;

    private IEnumerator _animation;
    private SpriteRenderer _sR;

    private void Awake()
    {
        _sR = GetComponent<SpriteRenderer>();
    }
    private void Start()
    {
        _sR.enabled = false;
        float sampleRate = 1 / _sample;
        _speedToPlay = sampleRate * _speed;
        if (!_looping) return;
        StartTheCo();
        _sR.enabled = true;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) StopTheCo();
    }
    public void PlayAnimation(Transform position)
    {
        _sR.enabled = true;
        float sampleRate = 1 / _sample;
        _speedToPlay = sampleRate / _speed;
        transform.position = position.position;
        transform.rotation = position.rotation;
        StartTheCo();
    }
    private void StartTheCo()
    {
        _animation = Animation();
        StartCoroutine(_animation);
    }

    public void StopTheCo()
    {
        if (_animation != null) StopCoroutine(_animation);
    }

    IEnumerator Animation()
    {
        foreach (var sprite in _sprites)
        {
            _sR.sprite = sprite;
            yield return new WaitForSeconds(_speedToPlay);
        }
        if (!_looping) _sR.enabled = false;
        if (_looping)
            StartTheCo();
    }
}
