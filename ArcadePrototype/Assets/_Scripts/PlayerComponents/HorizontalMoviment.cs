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
    private bool _canInput = true;

    [Header ("Components")]
    private Rigidbody2D _rb;
    private Animator _ac;
    private Detections _gd;

    [Header ("Dust")]
    [SerializeField] private ParticleSystem _dust;

    public bool FacingRight { get => _facingRight; }
    public float Direction { get => _direction; }

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
            if (!_gd.IsOnWall())
                Flip(false);
            if (_gd.IsGrounded())
            {
                _dust.Play();
            }
        }
        if ((_direction < 0 && transform.localRotation.y == 0) || (_direction > 0 && transform.localEulerAngles.y == 180) && !_gd.IsDashing())
        {
            if (!_gd.IsOnWall())
                Flip(false);
        }

        _ac.SetFloat("HorizontalSpeed", Mathf.Abs(_rb.velocity.x));
        _direction = Input.GetAxisRaw("Horizontal");
        CheckTopSpeed();
    }

    private void FixedUpdate()
    {
        if (Input.GetButton("GripWall") && _gd.IsOnWall())
        { }
        else
            if(_canInput)
                HorizontalMovimentLogic();
        Friction();
        LimitHorizontalSpeed();
    }

    private void HorizontalMovimentLogic()
    {
        float targetSpeed = _moveSpeed * _direction;
        float speedDifference = targetSpeed - _rb.velocity.x;
        float accelerartionRate = (Mathf.Abs(targetSpeed) > 0.01f) ? acceleration : decceleration;
        float movement = Mathf.Pow(Mathf.Abs(speedDifference) * accelerartionRate, velPower) * Mathf.Sign(speedDifference);
        _rb.AddForce(movement * Vector2.right);
    }

    public void Flip(bool flipSwitch)
    {
        if (Time.timeScale < 1)
            return;
        if (!flipSwitch)
        {
            if (_direction > 0)
                _facingRight = true;
            else
                _facingRight = false;
        }
        else
        {
            _facingRight = !_facingRight;
            _direction *= -1f;
        }

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

    private void LimitHorizontalSpeed()
    {
        if (_rb.velocity.x > 30)
            _rb.velocity = new Vector2(30, _rb.velocity.y);
        if (_rb.velocity.x < -30)
            _rb.velocity = new Vector2(-30, _rb.velocity.y);
    }

    private float _topXSpeed;
    private void CheckTopSpeed()
    {
        if (_topXSpeed <= _rb.velocity.x)
            _topXSpeed = _rb.velocity.x;
    }

    private void OnDisable()
    {
        _rb.velocity = Vector2.zero;
    }

    public void StartDisableInput()
    {
        StartCoroutine(DisableInput());
    }

    IEnumerator DisableInput()
    {
        _canInput = false;
        yield return new WaitForSeconds(0.125f);
        _canInput = true;
    }
}
