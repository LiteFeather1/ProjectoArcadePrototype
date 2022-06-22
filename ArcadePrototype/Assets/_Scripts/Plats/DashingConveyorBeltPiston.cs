using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashingConveyorBeltPiston : MonoBehaviour, IIGiveSpeed
{
    [Header("Speed")]
    public float _speed;
    [SerializeField] private AnimationCurve _speedCurve;
    private float _evaluateSpeedTime;
    private int _moving = 0;
    [SerializeField] private AnimationCurve _movingBackSpeedCurve;

    [Header("Positions")]
    [SerializeField] Vector3 _whereToMove;
    private Vector3 _realWhereToMove;
    private Vector3 _startPos;

    protected Vector3 GizmosStartPos => transform.position;
    protected Vector3 GizmosWhereTo => transform.position + _whereToMove;

    [Header("Delays")]
    [SerializeField] private float _delayToMove;
    [SerializeField] private float _delayToMoveBack = 0.5f;

    private IEnumerator _movingToWhere;
    private IEnumerator _movingBack;

    private bool _moveToWhere;
    private bool _newDash;
    private bool _gameStarted;

    [SerializeField] private Dash _playerDash;

    private void OnEnable()
    {
        _playerDash.AddMethodToDashEvent(StartMoving);
    }

    private void Start()
    {
        _startPos = transform.position;
        _realWhereToMove = transform.position + _whereToMove;
    }

    protected virtual void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        if (!_gameStarted)
            Gizmos.DrawLine(GizmosStartPos, GizmosWhereTo);
        else
            Gizmos.DrawLine(GizmosStartPos, _realWhereToMove);
    }

    private void OnDisable()
    {
        _playerDash.RemoveMethodFromDashEvent(StartMoving);
    }


    public Vector2 GetMySpeed()
    {
        if (_moveToWhere)
        {
            Vector2 speed = _moving * _speed * _whereToMove.normalized;
            return speed;
        }
        return Vector2.one;
    }

    private void StartMoving()
    {
        print("I Moved");
        _newDash = true;
        _moveToWhere = true;
        StartCoroutine(DashMoviment());
    }

    IEnumerator DashMoviment()
    {
        _newDash = false;
        while (!_newDash)
        {
            _moving = 1;
            _speed = 0;
            while (_moveToWhere && transform.position != _realWhereToMove)
            {
                _evaluateSpeedTime += Time.deltaTime;
                _speed = _speedCurve.Evaluate(_evaluateSpeedTime);
                transform.position = Vector2.MoveTowards(transform.position, _realWhereToMove, _speed * Time.deltaTime);
                yield return null;
            }

            _evaluateSpeedTime = 0;
            yield return new WaitForSeconds(_delayToMoveBack);
            _moveToWhere = false;
            _newDash = true;
        }
            StartCoroutine(MoveBack());
    }

    IEnumerator MoveBack()
    {
        while (_newDash)
        {
            _speed = 0;
            while(!_moveToWhere && transform.position != _startPos)
            {
                _evaluateSpeedTime += Time.deltaTime;
                _speed = _movingBackSpeedCurve.Evaluate(_evaluateSpeedTime);
                transform.position = Vector2.MoveTowards(transform.position, _startPos, _speed * Time.deltaTime);
                yield return null;
            }
            _speed = 0;
            _newDash = false;
        }
    }
}
