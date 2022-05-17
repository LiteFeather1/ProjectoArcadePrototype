using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnlyBackAndForthMultiplePoints : MoveablePlats
{
    [SerializeField] private Transform[] _whereTos;
    private Vector3 _nextPos;
    private int _currentArrayItem;
    private bool _canMove;

    protected override void Awake()
    {
        _nextPos = _whereTos[_currentArrayItem].position;
    }
     
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, _whereTos[0].position);
        for (int i = 1; i <= _whereTos.Length - 1; i++)
        {
            Gizmos.DrawLine(_whereTos[i - 1].position, _whereTos[i].position);
        }
    }

    IEnumerator Movement()
    {
        while (transform.position != _nextPos && _canMove)
        {
            transform.position = Vector2.MoveTowards(transform.position, _nextPos, _speed * Time.deltaTime);
            yield return null;
        }
        if (_canMove)
        {
            if (_currentArrayItem < _whereTos.Length - 1 && _canMove)
            {
                _currentArrayItem++;
            }
            else if (_currentArrayItem == _whereTos.Length - 1 && _canMove)
            {
                _currentArrayItem = 0;
            }
            _nextPos = _whereTos[_currentArrayItem].position;
            StartCoroutine(Movement());
        }
        
    }
    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);
        _canMove = true;
        StartCoroutine(Movement());
    }

    protected override void OnCollisionExit2D(Collision2D collision)
    {
        base.OnCollisionExit2D(collision);
        _canMove = false;
    }
}
