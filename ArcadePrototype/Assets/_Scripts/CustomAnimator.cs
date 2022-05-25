using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomAnimator : MonoBehaviour
{
    [SerializeField] private Sprite[] _sprites;
    [SerializeField] private float _sample;
    [SerializeField] private float _speed = 1;
    private float _speedToPlay;

    private SpriteRenderer _sR;

    private void Awake()
    {
        _sR = GetComponent<SpriteRenderer>();
    }
    private void Start()
    {
        _sR.enabled = false;
    }

    public void PlayAnimation(Transform position)
    {
        _sR.enabled = true;
        float sampleRate = 1 / _sample;
        _speedToPlay = sampleRate / _speed;
        transform.position =  position.position;
        transform.rotation = position.rotation;
        StartCoroutine(Animation());
    }

    IEnumerator Animation()
    {
        foreach (var sprite in _sprites)
        {
            _sR.sprite = sprite;
            yield return new WaitForSeconds(_speedToPlay);
        }
        _sR.enabled = false;
    }
}
