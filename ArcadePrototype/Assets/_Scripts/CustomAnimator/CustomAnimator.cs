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
    [SerializeField] private bool _startOnAwake = true;
    [SerializeField] private float _timeToWaitBetweenAnimation;
    private float _timeBetween;
    [SerializeField] private bool _randomizeTimeBetween;
    [SerializeField] private bool _disableSprite = true;


    private IEnumerator _animation;
    private SpriteRenderer _sR;

    private void Awake()
    {
        _sR = GetComponent<SpriteRenderer>();
    }
    private void Start()
    {
        if(_disableSprite)
            _sR.enabled = false;
        float sampleRate = 1 / _sample;
        _speedToPlay = sampleRate * _speed;
        if (!_looping) return;
        if(_startOnAwake) StartTheCo();
        _sR.enabled = true;
        _timeBetween = _timeToWaitBetweenAnimation;
    }

    public void PlayAnimation(Transform position)
    {
        _sR.enabled = true;
        float sampleRate = 1 / _sample;
        _speedToPlay = sampleRate / _speed;
        transform.SetPositionAndRotation(position.position, position.rotation);
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
        if (!_looping && _disableSprite) _sR.enabled = false;
        if (_looping)
        {
            if (_randomizeTimeBetween)
                RandomizeTimeBetween();
            yield return new WaitForSeconds(_timeToWaitBetweenAnimation);
            StartTheCo();
        }
    }

    private void RandomizeTimeBetween()
    {
        float time = _timeBetween / 2;
        float randomTime = Random.Range(-time, time);
        _timeToWaitBetweenAnimation = _timeBetween + randomTime;
    }
}
