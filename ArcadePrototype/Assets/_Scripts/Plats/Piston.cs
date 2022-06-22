using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piston : MoveWhenPlayerAbove, IIGiveSpeed
{
    [SerializeField] private float _delayToMove;
    [SerializeField] private AnimationCurve _speedCurve;
    private float _speedTime;

    [Header("Sprites")]
    [SerializeField] private Sprite _stop;
    [SerializeField] private Sprite _go;
    [SerializeField] private Sprite _backing;

    private IEnumerator _movingToWhere;
    private IEnumerator _movingBack;
    private bool _once;
    private int _moving;
    private SpriteRenderer _sr;

    protected override void Awake()
    {
        base.Awake();
        _sr = GetComponent<SpriteRenderer>();
    }

    IEnumerator MoveToWhere()
    {
        _moving = 1;
        _sr.sprite = _go;
        _speed = 0;
        yield return new WaitForSeconds(_delayToMove);
        while (_moveToWhere && transform.position != _realWhereTo)
        {
            _speedTime += Time.deltaTime;
            _speed = _speedCurve.Evaluate(_speedTime);
            transform.position = Vector2.MoveTowards(transform.position, _realWhereTo, _speed * Time.deltaTime);
            yield return null;
        }
        _speedTime = 0;
        yield return new WaitForSeconds(_delayToMove);
        _moveToWhere = false;
        _movingToWhere = null;
        if (_movingBack == null)
            _movingBack = MoveBack();
        if (_movingBack != null)
            StartCoroutine(_movingBack);
    }

    IEnumerator MoveBack()
    {
        _sr.sprite = _backing;
        _speed = 0;
        _moving = -1;
        yield return new WaitForSeconds(_delayToMoveback);
        while (!_moveToWhere && transform.position != _startPos)
        {
            _speedTime += Time.deltaTime;
            _speed = _speedCurve.Evaluate(_speedTime);
            transform.position = Vector2.MoveTowards(transform.position, _startPos, _speed/2 * Time.deltaTime);
            yield return null;
        }
        _speedTime = 0;
        _movingBack = null;
        _once = false;
        _sr.sprite = _stop;
    }

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (!_once)
            {         
                _once = true;
                _moveToWhere = true;
                if (_movingToWhere == null && _movingBack == null)
                    _movingToWhere = MoveToWhere();
                if (_movingToWhere != null)
                    StartCoroutine(_movingToWhere);
            }
        }
    }

    public Vector2 GetMySpeed()
    {
        if(_moveToWhere)
        {
            Vector2 speed = _moving * _speed * _whereToMove.normalized;
            return speed;
        }

        return Vector2.one;
    }
}
