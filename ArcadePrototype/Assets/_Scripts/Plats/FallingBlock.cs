using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingBlock : MonoBehaviour
{
    private float _fallSpeed;
    [SerializeField] private AnimationCurve _aceleration;
    private float _acellerationTime;

    [SerializeField] private Vector3 _whereToMove;
    private Vector3 _realWhereTo;
    private bool _first = true;
    private bool _canMove;

    protected Vector3 _gizmosStartPos => transform.position;
    protected Vector3 _gizmosWhereTo => transform.position + _whereToMove;
    private bool _gameStarted;


    private void Start()
    {
        _realWhereTo = transform.position + _whereToMove;
    }

    private void Update()
    {
        if (_canMove)
        {
            _acellerationTime += Time.deltaTime;
            _fallSpeed = _aceleration.Evaluate(_acellerationTime);
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x , transform.position.y  -1), _fallSpeed * Time.deltaTime);
        }

        if (transform.position.y <= _realWhereTo.y)
            this.enabled = false;
    }

    protected virtual void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        if(!_gameStarted)
        Gizmos.DrawLine(transform.position, _gizmosWhereTo);
        else
        Gizmos.DrawLine(_gizmosStartPos, _realWhereTo); 
    }

private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (_first == true)
            {
                _canMove = true;
                _first = false;
            }
        }
    }
}
