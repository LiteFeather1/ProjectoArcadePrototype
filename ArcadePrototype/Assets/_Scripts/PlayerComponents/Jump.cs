using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Detections))]
public class Jump : MonoBehaviour
{
    [Header("Jump")]
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _fallMultiplier = 2.5f;
    [SerializeField] private float _lowJumpMultiplier = 3f;
    private float _coyoteReseter = .15f;
    private float _coyoteTimer;
    private float _autoJumpReseter = .2f;
    private float _autoJump;
    private bool _canJump = true;

    private bool _canAddForce = true;

    [Header("Secondary Jumps")]
    [SerializeField] private float _secondaryJumpForce;
    [SerializeField] private int _howManySecondaryJumps = 1;
    private bool _secondaryJumping;
    private int _secondaryJumpAmount;
    private bool _wasOnGroundLastFrame;
    private bool _disableGravity;

    [Header("Particles")]
    [SerializeField] private Transform _feetPos;
    [SerializeField] private CustomAnimator _firstJumpParticle;
    [SerializeField] private CustomAnimator _secondJumpParticle;
    [SerializeField] private CustomAnimator _groundContackParticle;

    [Header("VisualAid")]
    [SerializeField] private SpriteRenderer _visualAidSR;
    [SerializeField] private Sprite _nullSprite;
    [SerializeField] private CustomAnimator _customAnimator;
    [SerializeField] private Transform _visualAidTransform;

    private Detections _gd;
    private Rigidbody2D _rb;
    private Animator _ac;
    private WallStamina _wallstamina;

    private void Awake()
    {
        _gd = GetComponent<Detections>();
        _rb = GetComponent<Rigidbody2D>();
        _ac = GetComponent<Animator>();
        _wallstamina = GetComponent<WallStamina>();
        _secondaryJumpAmount = _howManySecondaryJumps;
    }

    private void Update()
    {
        JumpInput();
        ReplenishSecondaryJumOnceGroundedAgain();
        CheckTopSpeed();
    }

    private void FixedUpdate()
    {
        JumpAction();
        Gravity();
        SecondJumpAction();
        LimitYVelocity();
    }

    private void JumpInput()
    {
        CoyoteTime();
        if (Input.GetButtonDown("Jump"))
        {
            _autoJump = _autoJumpReseter;
            if (_coyoteTimer < -.1f && !_gd.IsOnWall() && !_secondaryJumping)
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
    private float CoyoteTime()
    {
        if (_gd.IsGrounded() && _canJump)
            _coyoteTimer = _coyoteReseter;
        else
            _coyoteTimer -= Time.deltaTime;
        return _coyoteTimer;
    }
    private void JumpAction()
    {
        if (_autoJump > 0 && _coyoteTimer > 0f)
        {
            _coyoteTimer--;
            StartCoroutine(JumpSqueeze(0.75f, 1.3f, 0.15f));
            _firstJumpParticle.PlayAnimation(transform);
            _autoJump--;

            if (_gd.GetPistonSpeed().x == 1 || _gd.GetPistonSpeed().x == 0)
            {
                _rb.velocity = new Vector2(_rb.velocity.x, _jumpForce * _gd.GetPistonSpeed().y);
            }
            else
            {
                float xVelocity = Mathf.Abs(_rb.velocity.x);
                if (Mathf.Abs(xVelocity) < 5)
                    xVelocity = 5;

                bool velocityEqual = Mathf.Sign(_rb.velocity.x) == Mathf.Sign(_gd.GetPistonSpeed().x);
                if (Input.GetAxisRaw("Horizontal") == 0)
                    velocityEqual = true;

                if (velocityEqual)
                {
                    _rb.velocity = new Vector2(_gd.GetPistonSpeed().x * _jumpForce * xVelocity, _jumpForce * _gd.GetPistonSpeed().y);
                }
                else
                {
                    float velocityDIff = _gd.GetPistonSpeed().x - xVelocity;
                    _rb.velocity = new Vector2(_jumpForce * -velocityDIff / 10, _jumpForce * _gd.GetPistonSpeed().y);
                }
            }
            //print(_gd.GetPistonSpeed());
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
        if (_secondaryJumpAmount > 0 && _secondaryJumping)
        {
            _secondaryJumping = false;
            _rb.velocity = new Vector2(_rb.velocity.x, 0);
            _rb.AddForce(Vector2.up * (_secondaryJumpForce), ForceMode2D.Impulse);
            _secondaryJumpAmount--;
            _ac.SetTrigger("SecondJump");
            _secondJumpParticle.PlayAnimation(transform);
            StopVisualAidAnimation();
            //print("Doubled");
        }
    }

    private void Gravity()
    {
        //if (_gd.IsGrounded()) Physics2D.gravity = Vector2.zero;
        //else Physics2D.gravity = _gravityStored;
        if (!_disableGravity)
        {
            if (_rb.velocity.y < 0)
            {
                _rb.velocity += _fallMultiplier * Time.deltaTime * Physics2D.gravity * Vector2.up;
            }
            if (_rb.velocity.y > 0 && !Input.GetButton("Jump"))
            {
                _rb.velocity += _lowJumpMultiplier * Time.deltaTime * Physics2D.gravity * Vector2.up;
            }
        }
    }

    /// <summary>
    /// Needs to be on the end of Update.
    /// </summary>
    private void ReplenishSecondaryJumOnceGroundedAgain()
    {
        if (_wasOnGroundLastFrame != _gd.IsGrounded() && !_gd.IsOnWall())
        {
            _secondaryJumpAmount = _howManySecondaryJumps;
            //_customAnimator.PlayAnimation(_visualAidTransform);
            if(_rb.velocity.y < 1f) _groundContackParticle.PlayAnimation(_feetPos);
            StartCoroutine(JumpSqueeze(1.3f, 1f, 0.05f));
        }
        _wasOnGroundLastFrame = _gd.IsGrounded() || (_gd.IsOnWall() && _wallstamina.Stamina > 0) ;
    }

    public void ReplenishSecondaryJump()
    {
        _secondaryJumpAmount = _howManySecondaryJumps;
        //_customAnimator.PlayAnimation(_visualAidTransform);
    }

    public void AddForceOnCollision(Vector2 normal, float force, float timeToWait)
    {
        _rb.velocity = new Vector2(_rb.velocity.x, 0);
        _rb.AddForce(normal * force);
        StartCoroutine(DisableGravity(timeToWait));
    }

    public void AddForceOnCollisionTimed(Vector2 normal, float force, float timeToWait)
    {
        if (!_canAddForce) return;
        StartCoroutine(WaitToAddcollision());
        _rb.velocity = new Vector2(_rb.velocity.x, 0);
        _rb.AddForce(normal * force);
        StartCoroutine(DisableGravity(timeToWait));
    }

    IEnumerator WaitToAddcollision()
    {
        _canAddForce = false;
        yield return new WaitForSeconds(.25f);
        _canAddForce = true;
    }
    
    public void StartDisableGravity(float timeToWait)
    {
        StartCoroutine(DisableGravity(timeToWait));
    }
    //Disables gravity so the on add force can go higher without the player needing to press spaces
    IEnumerator DisableGravity(float timeToWait)
    {
        _disableGravity = true;
        yield return new WaitForSeconds(timeToWait);
        _disableGravity = false;
    }

    IEnumerator JumpSqueeze(float xSqueeze, float ySqueeze, float seconds)
    {
        Vector3 originalSize = Vector3.one;
        Vector3 newSize = new Vector3(xSqueeze, ySqueeze, originalSize.z);
        float t = 0f;
        while (t <= 1.0)
        {
            t += Time.deltaTime / seconds;
            transform.localScale = Vector3.Lerp(originalSize, newSize, t);
            yield return null;
        }
        t = 0f;
        while (t <= 1.0)
        {
            t += Time.deltaTime / seconds;
            transform.localScale = Vector3.Lerp(newSize, originalSize, t);
            yield return null;
        }
    }

    private void StopVisualAidAnimation()
    {
        _customAnimator.StopTheCo();
        _visualAidSR.sprite = _nullSprite;
    }

    private void LimitYVelocity()
    {
        if (_rb.velocity.y > 25)
            _rb.velocity = new Vector2(_rb.velocity.x, 25);
        if (_rb.velocity.y < -25)
            _rb.velocity = new Vector2(_rb.velocity.x, -25);
    }

    private float _topYSpeed;
    private void CheckTopSpeed()
    {
        if (_topYSpeed <= _rb.velocity.y)
            _topYSpeed = _rb.velocity.y;
    }
}
