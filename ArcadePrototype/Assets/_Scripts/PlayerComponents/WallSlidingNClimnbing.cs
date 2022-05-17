using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Detections))]
public class WallSlidingNClimnbing : MonoBehaviour
{
    [SerializeField] private float _slidingSpeed;
    [SerializeField] private float _climbingSpeed;
    private Detections _d;
    private Rigidbody2D _rb;
    // Start is called before the first frame update
    void Awake()
    {
        _d = GetComponent<Detections>();
        _rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        WallSlidingAction();
    }
    private void WallSlidingAction()
    {
        float xInput = Input.GetAxisRaw("Horizontal");
        float yInput = Input.GetAxisRaw("Vertical");
        if (xInput != 0 && _d.IsOnWall() && !Input.GetButton("Jump"))
        {
            if (yInput == 0)
            {
                _rb.velocity = new Vector2(_rb.velocity.x, 0);
                _rb.gravityScale = 0;
            }
            else if (yInput < 0)
            {
                Vector2 down = new Vector2(_rb.velocity.x, -_slidingSpeed);
                _rb.velocity = down;
            }
            else if (yInput > 0)
            {
                Vector2 up = new Vector2(_rb.velocity.x, _climbingSpeed);
                _rb.velocity = up;
            }
        }
        else
        {
            _rb.gravityScale = 1;
        }
        
    }
}
