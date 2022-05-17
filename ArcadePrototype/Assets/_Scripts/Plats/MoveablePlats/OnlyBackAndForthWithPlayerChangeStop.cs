using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnlyBackAndForthWithPlayerChangeStop : MoveablePlats
{
    [SerializeField] protected Vector3 _whereToMove;
    protected Vector3 _realWhereTo;
    private Vector3 _startPos;
    private bool _canMove;
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
    private IEnumerator MoveToWhere()
    {
        if (_moveToWhere)
        {
            while (transform.position != _realWhereTo && _canMove)
            {
                transform.position = Vector2.MoveTowards(transform.position, _realWhereTo, _speed * Time.deltaTime);
                yield return null;
            }
        }

        else
        {
            while (transform.position != _startPos && _canMove)
            {
                transform.position = Vector2.MoveTowards(transform.position, _startPos, _speed * Time.deltaTime);
                yield return null;
            }
        }
    }

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);
        _canMove = true;
        _moveToWhere = !_moveToWhere;
        StartCoroutine(MoveToWhere());
    }

    protected override void OnCollisionExit2D(Collision2D collision)
    {
        base.OnCollisionExit2D(collision);
        _canMove = false;
    }
}
