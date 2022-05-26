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
    private ParticleSystem.EmissionModule _sliddingEmission;

    // Start is called before the first frame update
    void Awake()
    {
        _d = GetComponent<Detections>();
        _rb = GetComponent<Rigidbody2D>();
        _wallStamina = GetComponent<WallStamina>();
        _ac = GetComponent<Animator>();
        _sliddingEmission = _slidingParticle.emission;
    }

    private void FixedUpdate()
    {
        WallSlidingAction();
        SliddingEmissionHandler();
    }

    private void WallSlidingAction()
    {
        float yInput = Input.GetAxisRaw("Vertical");
        if (_d.IsOnWall() && !Input.GetButton("Jump") && (Input.GetKey(KeyCode.Mouse0) || Input.GetKey(KeyCode.J)) && _wallStamina.Stamina > 0)
        {
            _ac.SetBool("Gripping", true);
            if (yInput == 0)
            {
                _rb.velocity = new Vector2(_rb.velocity.x, 0);
                _wallStamina.DemishFromWallGripping();
                _rb.gravityScale = 0;
                _ac.SetBool("WallClimbing", false);
            }
            else if (yInput < 0)
            {
                Vector2 down = new Vector2(_rb.velocity.x, -_slidingSpeed);
                _rb.velocity = down;
            }
            else if (yInput > 0)
            {
                _wallStamina.DemishFromWallClimbing();
                Vector2 up = new Vector2(_rb.velocity.x, _climbingSpeed);
                _rb.velocity = up;
                _ac.SetBool("WallClimbing", true);
            }
        }
        else
        {
            _rb.gravityScale = 1;
            _ac.SetBool("WallClimbing", false);
            _ac.SetBool("Gripping", false);
        }
    }

    private void SliddingEmissionHandler()
    {
        if (_rb.velocity.y < -0.1f && _d.IsOnWall() && Input.GetKey(KeyCode.Mouse0)) _slidingParticle.Play();
        else _slidingParticle.Stop();
    }
}
