using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Detections))]
public class WallJump : MonoBehaviour
{
    [SerializeField] private Vector2 _jumpForce;
    private bool _jumping;
    private float _storedDirection;

    private Detections _detection;
    private HorizontalMoviment _hm;
    private Rigidbody2D _rb;
    private WallStamina _wS;

    private void Awake()
    {
        _detection = GetComponent<Detections>();
        _hm = GetComponent<HorizontalMoviment>();
        _rb = GetComponent<Rigidbody2D>();
        _wS = GetComponent<WallStamina>();
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
        else if(_jumping)
        {
            return;
        }
        else if (Input.GetButtonDown("Jump"))
        {
            _jumping = true;
        }
    }

    private void WallJumpAction()
    {
        if(_jumping)
        {
            _hm.enabled = false;
            StartCoroutine(Co_ReactivateHorizontalMoviment());
            _jumping = false;
            float direction = Input.GetAxisRaw("Horizontal");
            if (_wS.Stamina > 0)
            {
                if (direction == 0)
                {
                    direction = .25f;
                }
                else if (direction != 0)
                {
                    print("hello");
                    direction = Input.GetAxisRaw("Horizontal");
                    _wS.DemishFromWallJump();
                }
            }
            else
            {
                direction = StoredDirection();
            }
            _rb.AddForce(new Vector2 (_jumpForce.x * -direction  * _detection.GetPistonSideSpeed(), _jumpForce.y), ForceMode2D.Impulse);
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
}
