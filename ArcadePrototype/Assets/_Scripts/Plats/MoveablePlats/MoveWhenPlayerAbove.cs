using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveWhenPlayerAbove : MoveablePlats
{
    [SerializeField] protected Vector3 _whereToMove;
    [SerializeField] protected float _delayToMoveback = 1 ;
    protected Vector3 _realWhereTo;
    protected Vector3 _startPos;
    protected Vector3 GizmosStartPos => transform.position;
    protected Vector3 GizmosWhereTo => transform.position + _whereToMove;
    protected bool _moveToWhere;
    protected bool _gameStarted;

    protected override void Awake()
    {
        base.Awake();
        _startPos = transform.position;
        _realWhereTo = transform.position;
        _realWhereTo += _whereToMove;
        _gameStarted = true;
    }

    protected virtual void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        if(!_gameStarted)
        Gizmos.DrawLine(GizmosStartPos, GizmosWhereTo);
        else
        Gizmos.DrawLine(GizmosStartPos, _realWhereTo);
    }

    IEnumerator MoveToWhere()
    {
        while(_moveToWhere && transform.position != _realWhereTo)
        {
            transform.position = Vector2.MoveTowards(transform.position, _realWhereTo, _speed * Time.deltaTime);
            yield return null;
        }
    }

    IEnumerator MoveBack()
    {
        yield return new WaitForSeconds(_delayToMoveback);
        while (!_moveToWhere && transform.position != _startPos)
        {
            transform.position = Vector2.MoveTowards(transform.position, _startPos, _speed * Time.deltaTime);
            yield return null;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            _moveToWhere = true;
            StartCoroutine(MoveToWhere());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _moveToWhere = false;
            StartCoroutine(MoveBack());
        }
    }
}
