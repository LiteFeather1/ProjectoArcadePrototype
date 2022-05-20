using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : MonoBehaviour
{
    [SerializeField] private AnimationCurve _dashSpeedCurve;
    [SerializeField] private Camera _cam;
    private float _dashSpeed;
    private float _dashTime;

    private bool _canDash = true;
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
        ReplenishDashOnceGroundedAgain();
    }
    private void FixedUpdate()
    {
        DashAction();
    }
    
    private void DashInput()
    {
        if(Input.GetKeyDown(KeyCode.Mouse1) && _canDash)
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
            //_rb.velocity = new Vector2(_dashSpeed * _direction, _dashSpeed * yInput);
            _rb.velocity = MouseDirection().normalized * _dashSpeed;
            StartCoroutine(Co_ExitDashing());
        }
    }

    IEnumerator Co_ExitDashing()
    {
        yield return new WaitForSeconds(_dashSpeedCurve[_dashSpeedCurve.length - 1].time - 0.2f);
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
}
