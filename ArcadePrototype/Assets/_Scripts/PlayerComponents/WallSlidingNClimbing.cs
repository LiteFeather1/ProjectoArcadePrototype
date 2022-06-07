using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Detections))]
public class WallSlidingNClimbing : MonoBehaviour
{
    [SerializeField] private float _slidingSpeed;
    [SerializeField] private float _climbingSpeed;

    private Detections _d;
    private Rigidbody2D _rb;
    private WallStamina _wallStamina;
    private Animator _ac;

    [Header("Particles")]
    [SerializeField] private ParticleSystem _slidingParticle;

    // Start is called before the first frame update
    void Awake()
    {
        _d = GetComponent<Detections>();
        _rb = GetComponent<Rigidbody2D>();
        _wallStamina = GetComponent<WallStamina>();
        _ac = GetComponent<Animator>();
    }
    private void Update()
    {
        if (Input.GetAxisRaw("Vertical") >= 1 && !_d.WallFinished() && !Input.GetButton("Jump"))
        {
            _rb.velocity = Vector2.zero;
            _ac.SetBool("WallClimbing", false);
        }
    }
    private void FixedUpdate()
    {
        WallSlidingAction();
        SliddingEmissionHandler();
    }

    private void WallSlidingAction()
    {
        float yInput = Input.GetAxisRaw("Vertical");
        if (_d.IsOnWall() && !Input.GetButton("Jump") && _wallStamina.Stamina >= 0)
        {
            _ac.SetBool("Gripping", true);
            if (Input.GetButton("GripWall"))
            {
                if (yInput == 0)
                {
                    _rb.velocity = new Vector2(_rb.velocity.x, 0);
                    _wallStamina.DemishFromWallGripping();
                    _ac.SetBool("WallClimbing", false);
                }
                else if (yInput < 0)
                {
                    Vector2 down = new Vector2(_rb.velocity.x, -_slidingSpeed);
                    _rb.velocity = down;
                }
                else if (yInput > 0 && _d.WallFinished())
                {
                    if(_rb.velocity.y >= 0.2f)
                    _wallStamina.DemishFromWallClimbing();
                    else
                        _wallStamina.DemishFromWallGripping();

                    Vector2 up = new Vector2(_rb.velocity.x, _climbingSpeed);
                    _rb.velocity = up;

                    if(_rb.velocity.y >= 0.1f)
                    {
                        _ac.SetBool("WallClimbing", true);
                    }
                }
                _rb.gravityScale = 0;
            }
            else 
                if(!_d.IsDashing())
                    _rb.gravityScale = 1;
        }
        else
        {
            if(!_d.IsDashing())
                _rb.gravityScale = 1;   
            _ac.SetBool("WallClimbing", false);
            _ac.SetBool("Gripping", false);
        }
    }

    private void SliddingEmissionHandler()
    {
        if (_rb.velocity.y < -0.2f && _d.IsOnWall()) _slidingParticle.Play();
        else _slidingParticle.Stop();
    }
}
