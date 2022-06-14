using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Detections))]
public class WallJump : MonoBehaviour
{
    [SerializeField] private Vector2 _jumpForce;
    private bool _jumping;

    float _direction;

    private Detections _detection;
    private HorizontalMoviment _hm;
    private Rigidbody2D _rb;
    private WallStamina _wS;
    private Animator _ac;
    [SerializeField] private CustomAnimator _wallJump;

    private HorizontalMoviment _horizontal;

    private void Awake()
    {
        _detection = GetComponent<Detections>();
        _hm = GetComponent<HorizontalMoviment>();
        _rb = GetComponent<Rigidbody2D>();
        _wS = GetComponent<WallStamina>();
        _ac = GetComponent<Animator>();
        _horizontal = GetComponent<HorizontalMoviment>();
    }

    private void Update()
    {
        WallJumpInput();
    }

    private void FixedUpdate()
    {
        WallJumpAction();
    }

    private void WallJumpInput()
    {
        if (!_detection.IsOnWall())
        {
            return;
        }
        else if (_jumping)
        {
            return;
        }
        else if (Input.GetButtonDown("Jump") && !_detection.IsGrounded()) 
        {
            _jumping = true;
        }
    }

    private void WallJumpAction()
    {
        if (_jumping && !_detection.IsGrounded())
        {
            Vector2 pistonSideSpeed = _detection.GetPistonSideSpeed();
            if (pistonSideSpeed.x < 1)
                pistonSideSpeed.x = 1;
            _jumping = false;
            _hm.enabled = false;
            StartCoroutine(Co_ReactivateHorizontalMoviment());


            float xForce = Input.GetAxisRaw("Horizontal");
            float upForce = 0;
            if (_wS.Stamina > 0)
            {
                if (Input.GetAxisRaw("Vertical") >= .5f)
                {
                    xForce = 0f * _direction;
                    upForce = .8f;
                    _wS.DemishFromWallJumpStraight();
                }
                else 
                {
                    upForce = .8f;
                    if (_horizontal.FacingRight)
                        xForce = 1;
                    else
                        xForce = -1;
                    _horizontal.Flip(true);
                    _wS.DemishFromWallJump();
                }
            }
            else
            {
                if (_horizontal.FacingRight)
                    xForce = 1;
                else
                    xForce = -1;
                _horizontal.Flip(true);
                _direction = _direction * -1;
                upForce = .5f;
                print("WallJumpElse2");
            }
            _rb.velocity = Vector2.zero;
            _rb.velocity = new Vector2(_jumpForce.x * -xForce * pistonSideSpeed.x, _jumpForce.y * upForce * pistonSideSpeed.y);
            _ac.SetTrigger("WallJumped");
            _wallJump.PlayAnimation(transform);
        }
    }

    IEnumerator Co_ReactivateHorizontalMoviment()
    {
        yield return new WaitForSeconds(0.125f);
        _hm.enabled = true;
    }
}
