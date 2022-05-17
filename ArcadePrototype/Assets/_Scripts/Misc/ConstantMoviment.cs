using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ConstantMoviment : MonoBehaviour
{
    protected float _speed;
    [SerializeField] protected float _minSpeed;
    [SerializeField] protected float _maxSpeed;
    [SerializeField] protected Directions _directions;
    protected Vector2 _direction;
    protected enum Directions
    {
        Right,
        Left,
        Up,
        Down
    }
    protected virtual void Start()
    {
        PickDirection();
        RandomSpeed();
    }
    protected virtual void Update()
    {
        Moviment();
    }

    protected virtual void PickDirection()
    {
        switch (_directions)
        {
            case Directions.Right:
                _direction = Vector2.right;
                break;
            case Directions.Left:
                _direction = Vector2.left;
                break;
            case Directions.Up:
                _direction = Vector2.up;
                break;
            case Directions.Down:
                _direction = Vector2.down;
                break;
        }
    }
    protected virtual void Moviment()
    {
        var step = _speed * Time.deltaTime;
        transform.Translate(_direction * step);
    }

    protected virtual void RandomSpeed()
    {
        _speed = Random.Range(_minSpeed, _maxSpeed);
    }
}
