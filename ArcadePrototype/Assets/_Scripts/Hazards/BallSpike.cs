using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSpike : MonoBehaviour
{
    [SerializeField] private float _speed;
    private float _velocity;
    protected Rigidbody2D _rb;

    protected void OnEnable()
    {
        AddStartingForce();
        StartCoroutine(Co_WaitToHaveVelocity());
    }

    protected void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        //if (_rb.velocity.magnitude < _velocity)
        //{
        //    float _forceToAdd = _velocity / _rb.velocity.magnitude * 10;
        //    _rb.AddForce(Vector2.one * _forceToAdd);
        //}
    }

    IEnumerator Co_WaitToHaveVelocity()
    {
        yield return new WaitForSeconds(.1f);
        _velocity = _rb.velocity.magnitude;
    }

    protected void AddStartingForce()
    {
        float x = Random.value < 0.5f ? -1f : 1f;
        float y = Random.value < 0.5f ? Random.Range(-1, -.5f) : Random.Range(-1, -.5f);

        _rb.AddForce(new Vector2(x , y).normalized * _speed, ForceMode2D.Force);
    }
}
