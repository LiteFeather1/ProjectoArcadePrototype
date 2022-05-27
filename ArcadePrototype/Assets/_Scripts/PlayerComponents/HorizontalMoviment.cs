using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Detections))]
public class HorizontalMoviment : MonoBehaviour
{
    [Header("HorizontalMoviment")]
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float acceleration = 1f;
    [SerializeField] private float decceleration = 2f;
    [SerializeField] private float velPower = 1f;
    [SerializeField] private float _frictionAmount;

    private float _direction;
    private bool _facingRight = true;

    [Header ("Components")]
    private Rigidbody2D _rb;
    private Animator _ac;
    private Detections _gd;

    [Header ("Dust")]
    [SerializeField] private ParticleSystem _dust;


    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _ac = GetComponent<Animator>();
        _gd = GetComponent<Detections>();
    }

    private void Update()
    {
        if ((_direction < 0 && _facingRight) || (_direction > 0 && !_facingRight))
        {
            Flip();
            if (_gd.IsGrounded()) _dust.Play();
        }
        if ((_direction < 0 && transform.localRotation.y == 0) || (_direction > 0 && transform.localRotation.y == 180) && !_gd.IsDashing()) Flip();
        _ac.SetFloat("HorizontalSpeed", Mathf.Abs(_rb.velocity.x));
    }
    private void FixedUpdate()
    {
        HorizontalMovimentLogic();
        Friction();
    }

    private void HorizontalMovimentLogic()
    {
        _direction = Input.GetAxisRaw("Horizontal");
        float targetSpeed = _moveSpeed * _direction;
        float speedDifference = targetSpeed - _rb.velocity.x;
        float accelerartionRate = (Mathf.Abs(targetSpeed) > 0.01f) ? acceleration : decceleration;
        float movement = Mathf.Pow(Mathf.Abs(speedDifference) * accelerartionRate, velPower) * Mathf.Sign(speedDifference);
        _rb.AddForce(movement * Vector2.right);

        _ac.SetFloat("HorizontalSpeed", Mathf.Abs(_rb.velocity.x));
    }

    public void Flip()
    {
        if (_direction > 0) _facingRight = true;
        else _facingRight = false;
        transform.rotation = Quaternion.Euler(0, _facingRight ? 0 : 180, 0);
    }

    private void Friction()
    {
        if (_gd.IsGrounded() && Mathf.Abs(_direction) < 0.01f)
        {
            float amount = Mathf.Min(Mathf.Abs(_rb.velocity.x), Mathf.Abs(_frictionAmount));
            amount *= Mathf.Sign(_rb.velocity.x);

            _rb.AddForce(Vector2.right * -amount, ForceMode2D.Impulse);
        }
    }

    private void OnDisable()
    {
        _rb.velocity = Vector2.zero;
    }
}
