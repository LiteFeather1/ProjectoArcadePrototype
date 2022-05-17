using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Detections))]
public class Jump : MonoBehaviour
{
    [Header("Jump")]
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _fallMultiplier = 2.5f;
    [SerializeField] private float _lowJumpMultiplier = 3f;
    private float _autoJumpReseter = .2f;
    private float _autoJump;
    [Header("Secondary Jumps")]
    private bool _secondaryJumping;
    [SerializeField] private int _howManySecondaryJumps = 1;
    private int _secondaryJumpAmount;
    private bool _wasOnGroundLastFrame;

    private Detections _gd;
    private Rigidbody2D _rb;
    private Animator _ac;

    private void Awake()
    {
        _gd = GetComponent<Detections>();
        _rb = GetComponent<Rigidbody2D>();
        _ac = GetComponent<Animator>();
    }
    private void Update()
    {
        JumpInput();
        ReplenishSecondaryJumOnceGroundedAgain();
    }
    private void FixedUpdate()
    {
        JumpAction();
        Gravity();
        SecondJumpAction();
    }
    private void JumpInput()
    {
        if (Input.GetButtonDown("Jump"))
        {
            _autoJump = _autoJumpReseter;
            _ac.SetTrigger("Jumped");
            if (!_gd.IsGrounded())
            {
                _secondaryJumping = true;
            }
        }
        else
        {
            _autoJump -= Time.deltaTime;
        }

        _ac.SetFloat("VerticalSpeed", _rb.velocity.y);
    }

    private void JumpAction()
    {
        if (_autoJump > 0 && _gd.IsGrounded())
        {
            _rb.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
            _autoJump--;
        }
    }

    private void SecondJumpAction()
    {
        if (_secondaryJumpAmount > 0 && _secondaryJumping)
        {
            _secondaryJumping = false;
            _rb.velocity = new Vector2(_rb.velocity.x, 0);
            _rb.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
            _secondaryJumpAmount--;
        }
    }

    private void Gravity()
    {
        if (_rb.velocity.y < 0)
        {
            _rb.velocity += Vector2.up * Physics2D.gravity * _fallMultiplier * Time.deltaTime;
        }
        if (_rb.velocity.y > 0 && !Input.GetButton("Jump"))
        {
            _rb.velocity += Vector2.up * Physics2D.gravity * _lowJumpMultiplier * Time.deltaTime;
        }
    }

    /// <summary>
    /// Needs to be on the end of Update.
    /// </summary>
    private void ReplenishSecondaryJumOnceGroundedAgain()
    {
        if (_wasOnGroundLastFrame != _gd.IsGrounded())
        {
            _secondaryJumpAmount = _howManySecondaryJumps;
        }
        _wasOnGroundLastFrame = _gd.IsGrounded() || _gd.IsOnWall();
    }
    public void ReplenishSecondaryJump()
    {
        _secondaryJumpAmount = _howManySecondaryJumps;
    }
}
