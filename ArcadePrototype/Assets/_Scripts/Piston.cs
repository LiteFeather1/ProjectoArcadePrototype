using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piston : MoveWhenPlayerAbove
{
    [SerializeField] private float _delayToMove;
    [SerializeField] private AnimationCurve _speedCurve;
    private float _speedTime;
    IEnumerator MoveToWhere()
    {
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
        StartCoroutine(MoveBack());
    }

    IEnumerator MoveBack()
    {
        _speed = 0;
        while (!_moveToWhere && transform.position != _startPos)
        {
            _speedTime += Time.deltaTime;
            _speed = _speedCurve.Evaluate(_speedTime);
            transform.position = Vector2.MoveTowards(transform.position, _startPos, _speed * Time.deltaTime);
            yield return null;
        }
        _speedTime = 0;
    }

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            _moveToWhere = true;
            StartCoroutine(MoveToWhere());
        }
    }

    public float GetMySpeed()
    {
        if(_moveToWhere)
        {
            return _speed;
        }

        return 1;
    }
}
