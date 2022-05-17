using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveWhenPlayerAbove : MoveablePlats
{
    [SerializeField] private Vector3 _whereToMove;
    private Vector3 _realWhereTo;
    private Vector3 _startPos;
    private bool _moveToWhere;

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
        Gizmos.DrawLine(_startPos, _realWhereTo);
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
        while (!_moveToWhere && transform.position != _startPos)
        {
            transform.position = Vector2.MoveTowards(transform.position, _startPos, _speed * Time.deltaTime);
            yield return null;
        }
    }

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);
        if(collision.gameObject.tag == "Player")
        {
            _moveToWhere = true;
            StartCoroutine(MoveToWhere());
        }
    }
    protected override void OnCollisionExit2D(Collision2D collision)
    {
        base.OnCollisionExit2D(collision);
        if (collision.gameObject.tag == "Player")
        {
            _moveToWhere = false;
            StartCoroutine(MoveBack());
        }
    }
}
