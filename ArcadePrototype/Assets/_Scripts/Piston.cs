using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piston : MoveWhenPlayerAbove
{
    [SerializeField] private float _delayToMove;
    IEnumerator MoveToWhere()
    {
        yield return new WaitForSeconds(_delayToMove);
        while (_moveToWhere && transform.position != _realWhereTo)
        {
            transform.position = Vector2.MoveTowards(transform.position, _realWhereTo, _speed * Time.deltaTime);
            yield return null;
        }
        yield return new WaitForSeconds(_delayToMove);
        _moveToWhere = false;
        StartCoroutine(MoveBack());
    }

    IEnumerator MoveBack()
    {
        while (!_moveToWhere && transform.position != _startPos)
        {
            transform.position = Vector2.MoveTowards(transform.position, _startPos, _speed * Time.deltaTime);
            yield return null;
        }
    }

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            _moveToWhere = true;
            StartCoroutine(MoveToWhere());
        }
    }
    protected override void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {

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
