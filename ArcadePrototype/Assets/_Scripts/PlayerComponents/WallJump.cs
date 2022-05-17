using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Detections))]
public class WallJump : MonoBehaviour
{
    [SerializeField] private Vector2 _jumpForce;
    private bool _jumping;

    private Detections _detection;
    private HorizontalMoviment _hm;
    private Rigidbody2D _rb;

    private void Awake()
    {
        _detection = GetComponent<Detections>();
        _hm = GetComponent<HorizontalMoviment>();
        _rb = GetComponent<Rigidbody2D>();
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
            int direction = Mathf.RoundToInt(Input.GetAxisRaw("Horizontal"));
            float up = Input.GetKey(KeyCode.W) ? .25f : 1;
            _rb.AddForce(new Vector2 (_jumpForce.x * -direction * up, _jumpForce.y), ForceMode2D.Impulse);
        }
    }

    IEnumerator Co_ReactivateHorizontalMoviment()
    {
        yield return new WaitForSeconds(0.125f);
        _hm.enabled = true;

    }
}
