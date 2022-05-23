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
    private float _autoJumpReseter = .1f;
    private float _autoJump;
    private bool _canJump = true;

    [Header("Secondary Jumps")]
    [SerializeField] private float _secondaryJumpForce;
    [SerializeField] private int _howManySecondaryJumps = 1;
    private bool _secondaryJumping;
    private int _secondaryJumpAmount;
    private bool _wasOnGroundLastFrame;
    private bool _disableGravity;

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
        if (_autoJump > 0 && _gd.IsGrounded() && _canJump)
        {
            _autoJump--;
            _rb.velocity = new Vector2(_rb.velocity.x, _jumpForce * _gd.GetPistonSpeed());
            //_rb.AddForce(Vector2.up * _jumpForce * _gd.GetPistonSpeed(), ForceMode2D.Impulse);
            StartCoroutine(JumpCoolDown_Co());
        }
    }

    IEnumerator JumpCoolDown_Co()
    {
        _canJump = false;
        while (_rb.velocity.y > 0)
        {
            yield return null;
        }
        _canJump = true;
    }

    private void SecondJumpAction()
    {
        if (_secondaryJumpAmount > 0 && _secondaryJumping && !_gd.IsOnWall())
        {
            _secondaryJumping = false;
            _rb.velocity = new Vector2(_rb.velocity.x, 0);
            _rb.AddForce(Vector2.up * (_secondaryJumpForce), ForceMode2D.Impulse);
            _secondaryJumpAmount--;
            _ac.SetTrigger("SecondJump");
        }
    }

    private void Gravity()
    {
        if (!_disableGravity)
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

    public void AddForceOnCollision(Vector2 normal, float force, float timeToWait)
    {
        _rb.AddForce(normal * force);
        StartCoroutine(DisableGravity(timeToWait));
    }

    IEnumerator DisableGravity(float timeToWait)
    {
        _disableGravity = true;
        yield return new WaitForSeconds(timeToWait);
        _disableGravity = false;
    }
}
