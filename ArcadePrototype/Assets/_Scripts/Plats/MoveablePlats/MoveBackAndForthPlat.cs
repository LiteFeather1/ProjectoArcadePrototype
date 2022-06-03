using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBackAndForthPlat : MoveablePlats, IButtonable
{
    [SerializeField] protected Vector3 _whereToMove;
    [SerializeField] private float _delayToMove;
    [SerializeField] private float _delayToMoveBack;
    private bool _canMove = true;
    protected Vector3 _realWhereTo;
    private Vector3 _startPos;

    protected Vector3 _gizmosStartPos => transform.position;
    protected Vector3 _gizmosWhereTo => transform.position + _whereToMove;
    protected bool _gameStarted;

    private void OnEnable()
    {
        StartCoroutine(MoveToWhere());
    }

    protected override void Awake()
    {
        base.Awake();
        _startPos = transform.position;
        _realWhereTo = transform.position;
        _realWhereTo += _whereToMove;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        if (!_gameStarted)
            Gizmos.DrawLine(_gizmosStartPos, _gizmosWhereTo);
        else
            Gizmos.DrawLine(_startPos, _realWhereTo);
    }

    protected virtual IEnumerator MoveToWhere()
    {
        while (transform.position != _realWhereTo)
        {
            transform.position = Vector2.MoveTowards(transform.position, _realWhereTo, _speed * Time.deltaTime);
            yield return null;
        }
        yield return new WaitForSeconds(_delayToMoveBack);
        StartCoroutine(MoveBack());
    }

    protected virtual IEnumerator MoveBack()
    {
        while (transform.position != _startPos)
        {
            transform.position = Vector2.MoveTowards(transform.position, _startPos, _speed * Time.deltaTime);
            yield return null;
        }
        yield return new WaitForSeconds(_delayToMove);
        if (_canMove)
        {
            StartCoroutine(MoveToWhere());
        }
    }

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);
    }
    protected override void OnCollisionExit2D(Collision2D collision)
    {
        base.OnCollisionExit2D(collision);
    }

    public void ToInterract(bool state)
    {
        _canMove = state;
        if(_canMove)
        {
            StartCoroutine(MoveToWhere());
        }   
    }
}
