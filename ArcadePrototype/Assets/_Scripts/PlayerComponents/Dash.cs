using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class Dash : MonoBehaviour
{
    [SerializeField] private AnimationCurve _dashSpeedCurve;
    [SerializeField] private Camera _cam;
    private float _dashSpeed;
    private Vector2 _dashDirection;
    private float _dashTime;

    private bool _canDash = true;
    private bool _isDashing;
    private bool _wasOnGroundLastFrame;

    private Vector2 _gravity;

    [Header ("Particles")]
    [SerializeField] private ParticleSystem _dashParticle;
    ParticleSystem.EmissionModule emission;

    private HorizontalMoviment _hm;
    private Detections _d;
    private Rigidbody2D _rb;
    private Animator _ac;

    private Action _dashedAction;

    private void Awake()
    {
        _hm = GetComponent<HorizontalMoviment>();
        _d = GetComponent<Detections>();
        _rb = GetComponent<Rigidbody2D>();
        _ac = GetComponent<Animator>();
        emission = _dashParticle.emission;
        _gravity = Physics2D.gravity;
    }

    private void Update()
    {
        MouseDashInput();
        KeyboardControllerDashInput();
        ReplenishDashOnceGroundedAgain();
        _d.SetDashing(_isDashing);
    }

    private void FixedUpdate()
    {
        DashVerb();
    }
    
    private void DashLogic()
    {
        Physics2D.gravity = Vector2.zero;
        _dashTime = 0f;
        _dashSpeed = _dashSpeedCurve.Evaluate(_dashTime);
        _canDash = false;
        _isDashing = true; 
        _ac.SetTrigger("DashAccel");
        _ac.SetBool("Dashing", _isDashing);
        DashFlip();
    }

    private void MouseDashInput()
    {
        if(_canDash && !_isDashing)
        {
            if (Input.GetKeyDown(KeyCode.Mouse1) && Time.timeScale > 0)
            {
                DashedEvent();
                _dashDirection = MouseDirection();
                DashLogic();
            }
        }
    }

    private void KeyboardControllerDashInput()
    {
        if (_canDash && !_isDashing)
        {
            if (Input.GetButtonDown("Dash") && Time.timeScale > 0)
            {
                DashedEvent();
                float xInput = Input.GetAxisRaw("Horizontal");
                float yInput = Input.GetAxisRaw("Vertical");
                if (Input.GetAxisRaw("Horizontal") == 0 && yInput == 0)
                {
                    if (_hm.FacingRight)
                        xInput = 1;
                    else
                        xInput = -1;
                }

                Vector2 inputDirection = new Vector2(xInput, yInput);

                _dashDirection = inputDirection;
                DashLogic();
            }
        }
    }

    private void DashVerb()
    {
        if(_isDashing)
        {
            _dashTime += Time.deltaTime;
            _dashSpeed = _dashSpeedCurve.Evaluate(_dashTime);
            _hm.enabled = false;
            _rb.velocity = _dashDirection.normalized * _dashSpeed;
            _rb.gravityScale = 0;
            StartCoroutine(Co_ExitDashing());
        }
        else
        {
            _rb.gravityScale = 1;
        }
    }

    IEnumerator Co_ExitDashing()
    {
        emission.enabled = true;
        yield return new WaitForSeconds(_dashSpeedCurve[_dashSpeedCurve.length - 1].time - 0.3f);
        Physics2D.gravity = _gravity;
        _isDashing = false;
        _hm.enabled = true;
        _ac.SetBool("Dashing", _isDashing);
        ReplenishDashAfterDashin();
        yield return new WaitForSeconds(0.2f);
        emission.enabled = false;
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
        _wasOnGroundLastFrame = _d.IsGrounded();
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

    private Vector2 MouseDirection()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition = _cam.ScreenToWorldPoint(mousePosition);
        Vector2 directionToMouse = new Vector2(mousePosition.x - transform.position.x, mousePosition.y - transform.position.y);
        return directionToMouse;
    }

    //Flips the player to face the direction of the dash
    private void DashFlip()
    {
        float direction = _dashDirection.x;

        transform.rotation = Quaternion.Euler(0, direction > 0 ? 0 : 180, 0);
    }

    #region AnimationEvents
    private void DashSquash(float squash)
    {
        transform.localScale = new Vector3(1f, squash);
    }
    private void DashStrecht(float strecht)
    {
        transform.localScale = new Vector3(strecht, 1f);
    }

    private void DashNormal()
    {
        transform.localScale = new Vector3(1f, 1f);
    }
    #endregion

    private void DashedEvent()
    {
        _dashedAction?.Invoke();
    }

    public void AddMethodToDashEvent(Action action)
    {
        _dashedAction += action;
    }

    public void RemoveMethodFromDashEvent(Action action)
    {
        _dashedAction -= action;
    }
}
