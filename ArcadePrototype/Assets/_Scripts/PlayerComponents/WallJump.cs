using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Detections))]
public class WallJump : MonoBehaviour
{
    [SerializeField] private Vector2 _jumpForce;
    private bool _jumping;
    private float _storedDirection;

    int _direction;

    private Detections _detection;
    private HorizontalMoviment _hm;
    private Rigidbody2D _rb;
    private WallStamina _wS;
    private Animator _ac;
    [SerializeField] private CustomAnimator _wallJump;

    private void Awake()
    {
        _detection = GetComponent<Detections>();
        _hm = GetComponent<HorizontalMoviment>();
        _rb = GetComponent<Rigidbody2D>();
        _wS = GetComponent<WallStamina>();
        _ac = GetComponent<Animator>();
    }

    private void Update()
    {
        WallJumpInput();
        Direction();
    }

    private void FixedUpdate()
    {
        WallJumpAction();
    }
    private void LateUpdate()
    {
        StoredDirection();
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
        if(_jumping && !_detection.IsGrounded())
        {
            _hm.enabled = false;
            StartCoroutine(Co_ReactivateHorizontalMoviment());

            _jumping = false;

            float xForce = Input.GetAxisRaw("Horizontal");
            float upForce = 0;
            if (_wS.Stamina > 0)
            {
                if (xForce == 0)
                {
                    xForce = .33f * _direction;
                    upForce = .75f;
                    _wS.DemishFromWallJump();
                }
                else if (xForce != 0)
                {
                    upForce = 1;
                    xForce = Input.GetAxisRaw("Horizontal");
                    _wS.DemishFromWallJump();
                }
            }
            else
            {
                xForce = StoredDirection();
                upForce = 1;
            }

            _rb.AddForce(new Vector2 (_jumpForce.x * -xForce  * _detection.GetPistonSideSpeed(), _jumpForce.y * upForce), ForceMode2D.Impulse);
            _ac.SetTrigger("WallJumped");
            _wallJump.PlayAnimation(transform);
            print("WallJumpin");
        }
    }
    private float StoredDirection()
    {
        if (Input.GetAxisRaw("Horizontal") != 0)
            _storedDirection = Input.GetAxisRaw("Horizontal");
        return _storedDirection;
    }

    IEnumerator Co_ReactivateHorizontalMoviment()
    {
        yield return new WaitForSeconds(0.125f);
        _hm.enabled = true;
    }

    private void Direction()
    {
        if ((Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.LeftArrow))) _direction = 1;
        else if ((Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.RightArrow))) _direction = -1;
    }
}
