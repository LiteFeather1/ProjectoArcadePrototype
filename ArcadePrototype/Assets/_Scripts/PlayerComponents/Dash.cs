using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : MonoBehaviour
{
    [SerializeField] private AnimationCurve _dashSpeedCurve;
    private float _dashSpeed;
    [SerializeField] private float _dashDuration;
    private float _dashTime;

    private int _direction = 1;
    public bool _canDash = true;
    private bool _isDashing;
    private bool _wasOnGroundLastFrame;

    private HorizontalMoviment _hm;
    private Detections _d;
    private Rigidbody2D _rb;

    private void Awake()
    {
        _hm = GetComponent<HorizontalMoviment>();
        _d = GetComponent<Detections>();
        _rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        DashInput();
        ChangeDirection();
        ReplenishDashOnceGroundedAgain();
    }
    private void FixedUpdate()
    {
        DashAction();
    }
    
    private void ChangeDirection()
    {
        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            _direction = 1;
        }
        else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            _direction = -1;
        }
    }
    private void DashInput()
    {
        if(Input.GetKeyDown(KeyCode.LeftShift) && _canDash)
        {
            _dashTime = 0f;
            _dashSpeed = _dashSpeedCurve.Evaluate(_dashTime);
            _canDash = false;
            _isDashing = true;
        }
    }

    private void DashAction()
    {
        if(_isDashing)
        {
            _dashTime += Time.deltaTime;
            _dashSpeed = _dashSpeedCurve.Evaluate(_dashTime);
            float yInput = Input.GetAxisRaw("Vertical");
            _hm.enabled = false;
            _rb.velocity = new Vector2(_dashSpeed * _direction, _dashSpeed * yInput);
            print("Dashed");
            StartCoroutine(Co_ExitDashing());
        }
    }

    IEnumerator Co_ExitDashing()
    {
        yield return new WaitForSeconds(_dashSpeedCurve[_dashSpeedCurve.length - 1].time - 0.02f);
        _isDashing = false;
        _hm.enabled = true;
        ReplenishDashAfterDashin();
    }

    /// <summary>
    /// Needs to be on the end of Update.
    /// </summary>
    private void ReplenishDashOnceGroundedAgain()
    {
        if (_wasOnGroundLastFrame != _d.IsGrounded())
        {
            if(!_isDashing)
            { 
                _canDash = true;
            }
        }
        _wasOnGroundLastFrame = _d.IsGrounded() || _d.IsOnWall();
    }

    private void ReplenishDashAfterDashin()
    {
        if(_d.IsGrounded())
        {
            _canDash = true;
        }
    }

    public void ReplenishDash()
    {
        _canDash = true;
    }
}
