using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(RotateObject))]
public class BallSpike : MonoBehaviour
{
    [SerializeField] private float _speed;

    protected Rigidbody2D _rb;

    private RotateObject _rotateComponent;

    protected void OnEnable()
    {
        AddStartingForce();
    }

    protected void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _rotateComponent = GetComponent<RotateObject>();
    }

    //BasicallyPong
    //Adds a force in a random direction
    protected void AddStartingForce()
    {
        float x = Random.value < 0.5f ? -1f : 1f;
        float y = Random.value < 0.5f ? Random.Range(-1, -.5f) : Random.Range(-1, -.5f);

        _rb.velocity = new Vector2(x , y).normalized * _speed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        _rotateComponent.InverseRotation();
    }
}
