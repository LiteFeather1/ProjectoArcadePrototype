using System.Collections;
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

    [Header ("Particles")]
    [SerializeField] private ParticleSystem _dashParticle;
    ParticleSystem.EmissionModule emission;

    private HorizontalMoviment _hm;
    private Detections _d;
    private Rigidbody2D _rb;
    private Animator _ac;

    private void Awake()
    {
        _hm = GetComponent<HorizontalMoviment>();
        _d = GetComponent<Detections>();
        _rb = GetComponent<Rigidbody2D>();
        _ac = GetComponent<Animator>();
        emission = _dashParticle.emission;
    }
    private void Update()
    {
        DashInput();
        ReplenishDashOnceGroundedAgain();
        ParticleHandler();
    }

    private void FixedUpdate()
    {
        DashAction();
    }
    
    private void DashInput()
    {
        if(Input.GetKeyDown(KeyCode.Mouse1) && _canDash && !_isDashing)
        {
            _dashTime = 0f;
            _dashSpeed = _dashSpeedCurve.Evaluate(_dashTime);
            _canDash = false;
            _isDashing = true;
            _dashDirection = MouseDirection();
            _ac.SetTrigger("DashAccel");
            _ac.SetBool("Dashing", _isDashing);
            DashFlip();
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
            _rb.velocity = _dashDirection.normalized * _dashSpeed;
            StartCoroutine(Co_ExitDashing());
        }
    }

    IEnumerator Co_ExitDashing()
    {
        yield return new WaitForSeconds(_dashSpeedCurve[_dashSpeedCurve.length - 1].time - 0.3f);
        _isDashing = false;
        _hm.enabled = true;
        _ac.SetBool("Dashing", _isDashing);
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

    //Flips the player to face the direction of the dash
    private void DashFlip()
    {
        float direction = _dashDirection.x;

        transform.rotation = Quaternion.Euler(0, direction > 0 ? 0 : 180, 0);
    }

    private void ParticleHandler()
    {
        if (_isDashing) emission.enabled = true;

        else emission.enabled = false;
    }
}
