using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Isaac : MonoBehaviour
{
    [Header("HorizontalMoviment")]
    [SerializeField] private int _speed;
    private bool _facingRight = true;
    private float _direction;
    [SerializeField] private Transform _groundDetection;
    [SerializeField] private LayerMask _groundMask;
    private bool _grounded;
    private bool _lightning;    
    private bool _wasOnGroundLastFrame;

    [Header("Jump")]
    [SerializeField] private AnimationCurve _jumpCurve;
    private Vector2 _startPosition;
    private float _timeElapsed;
    private bool _jumping;

    [Header("LadderMoviment")]
    [SerializeField] private LayerMask _ladderMask;
    private bool _onLadder;

    [Header("Lighting")]
    [SerializeField] private GameObject _firstLighting;
    private GameObject _currentLightining;
    [SerializeField] private float _pullSpeed;
    private float _currentSpeed;
    [SerializeField] LayerMask _stopShooting;
    [SerializeField] private IEnumerator _currentMoveToPoint;
    [SerializeField] private Vector2 _lightningDir;

    [Header("Misc")]
    [SerializeField] private float _rotateSpeed;
    private int _randomRotDirection = 1;
    [SerializeField] private GameObject _wand;
    private bool _spiked;

    [Header("Reset Position")]
    private Vector2 resetPos;

    [Header("Components")]
    [SerializeField] private Animator _ac;
    [SerializeField] private Rigidbody2D _rb;

    public IEnumerator CurrentMoveToPoint { get => _currentMoveToPoint; set => _currentMoveToPoint = value; }

    private void Start()
    {
        Keyframe kf = new Keyframe(0.15f, 0f);
        _jumpCurve = new AnimationCurve(new Keyframe(0, 0), kf, new Keyframe(.3f, 1));
        resetPos = transform.position;
    }

    void Update()
    {
        if (!IG_UiManager.Instance.PauseScreenSwitch)
        {
            JumpInput();
            DetectGround();          
            LightingInput();
            DetectLadder();
            StopLightableBlock(_lightningDir);
            SnapToGround();
            _wasOnGroundLastFrame = _grounded;         
        }
    }

    private void FixedUpdate()
    {
        if (!IG_UiManager.Instance.PauseScreenSwitch)
        {
            JumpWithCurve();
            HorizontalMoviment();
            LadderMoviment();
            Rotate();
        }
        else if (IG_UiManager.Instance.PauseScreenSwitch)
        {
            _rb.velocity = Vector2.zero;
        }
    }

    private void HorizontalMoviment()
    {
        if (!_lightning)
        {
            _direction = Input.GetAxisRaw("Horizontal");
            float moviment = _direction * _speed;
            Vector2 velocity = Vector2.right * moviment;
            _rb.velocity = velocity;
            _ac.SetFloat("Speed", Mathf.Abs(velocity.x));

            if ((_direction < 0 && _facingRight) || (_direction > 0 && !_facingRight))
            {
                Flip();
            }
        }
    }

    private void Flip()
    {
        _facingRight = !_facingRight;
        transform.localScale = new Vector3(_facingRight ? 1 : -1, 1, 1);
    }

    //Rotate Isaac when not touching Ladder or ground
    private void Rotate()
    {
        if (!_grounded && !_onLadder)
        {
            _rb.freezeRotation = false;
            _rb.MoveRotation(_rb.rotation + _rotateSpeed * Time.fixedDeltaTime);
        }
        else
        {
            _rb.freezeRotation = true;
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }

    private void JumpInput()
    {
        if (Input.GetKeyDown(KeyCode.W) && _grounded && !_onLadder && !_jumping)
        {
            _jumping = true;
            _timeElapsed = 0;
            _startPosition = transform.position;
            _randomRotDirection = _randomRotDirection * -1;
            _ac.SetTrigger("Jump");
            StartCoroutine(Co_Jump());
        }
    }
    private void JumpWithCurve()
    {
        if (_jumping)
        {
            _timeElapsed += Time.deltaTime;
            _rb.MovePosition(new Vector2(_startPosition.x, _startPosition.y + _jumpCurve.Evaluate(_timeElapsed)));
        }
    }

    IEnumerator Co_Jump()
    {
        yield return new WaitForSeconds(.3f);
        _jumping = false;
    }

    private void DetectGround()
    {
        Vector2 point = new Vector2(transform.position.x + .25f, transform.position.y - .35f);
        Vector2 point2 = new Vector2(transform.position.x - .25f, transform.position.y - .35f);
        _grounded = (Physics2D.OverlapCircle(point, .125f, _groundMask)) || Physics2D.OverlapCircle(point2, .125f, _groundMask); 
        _ac.SetBool("Grounded", _grounded);
        DetectGroundOnAir();
    }

    private void DetectGroundOnAir()
    {
        if (!_grounded)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 1f, _groundMask);
            Debug.DrawRay(transform.position, new Vector2(0, -1f), Color.red);
            _grounded = hit;
            _ac.SetBool("Grounded", _grounded);
        }
    }
    //Helps harry stick to ground after aa lighting
    private void SnapToGround()
    {
        if (_wasOnGroundLastFrame != _grounded && !_onLadder && !_lightning)
        {
            Vector2 clampPos = new Vector2(transform.position.x, Mathf.RoundToInt(transform.position.y));
            transform.position = clampPos;
        }
    }

    private void LightingInput()
    {
        if (!_lightning && !_jumping)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow) && CheckIfCanShootInDireciton(Vector2.up))
            {
                LightningDirection(0, 0, -.25f);
                _lightningDir = Vector2.up;
            }

            if (Input.GetKeyDown(KeyCode.RightArrow) && CheckIfCanShootInDireciton(Vector2.right))
            {
                LightningDirection(-90, -.25f, 0);
                _lightningDir = Vector2.right;
            }

            if (Input.GetKeyDown(KeyCode.DownArrow) && CheckIfCanShootInDireciton(Vector2.down))
            {
                LightningDirection(180, 0, +.25f);
                _lightningDir = Vector2.down;
            }
            if (Input.GetKeyDown(KeyCode.LeftArrow) && CheckIfCanShootInDireciton(Vector2.left))
            {
                LightningDirection(90, .25f, 0);
                _lightningDir = Vector2.left;
            }
        }
    }

    private void LightningDirection(float rotation, float startPosX, float startPosY)
    {
        AudioManager.PlaySound("Lightning1");
        _rb.velocity = Vector2.zero;
        _lightning = true;

        Vector2 position = new Vector2(transform.position.x + startPosX, transform.position.y + startPosY);
        _currentLightining = Instantiate(_firstLighting, position, Quaternion.Euler(0, 0, rotation));
        Lightning newLightning = _currentLightining.GetComponent<Lightning>();
        newLightning.HarryComponent(this);

        if (_grounded || _onLadder)
        {
            newLightning.IsHarrySolid(true);
        }
        else
        {
            newLightning.IsHarrySolid(false);
        }
    }

    //Checks if there is no block in one tile in the direction
    private bool CheckIfCanShootInDireciton(Vector2 direction)
    {
        Physics2D.queriesStartInColliders = false;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, 1f, _stopShooting);

        if(hit.collider != null)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    //Moves harry into the position the lighting has colidded
    public IEnumerator Co_MoveToPoint(Vector3 whereTo)
    {
        float currentDistance = Vector2.Distance(transform.position, whereTo);
        while (currentDistance > .2f && !_spiked && _lightning)
        {

            if (currentDistance > 1)
            {
                _currentSpeed = _pullSpeed / 2 * currentDistance / 5;
            }
            else
            {
                _currentSpeed = _pullSpeed;
            }

            transform.position = Vector2.MoveTowards(transform.position, whereTo, _currentSpeed);
            currentDistance = Vector2.Distance(transform.position, whereTo);
            yield return new WaitForFixedUpdate();
        }

        int posY = Mathf.RoundToInt(transform.position.y);
        int posX = Mathf.RoundToInt(transform.position.x);
        transform.position = new Vector2(posX, posY);
        _lightning = false;
    }

    public IEnumerator Co_LightingReturnFalse(float time)
    {
        yield return new WaitForSeconds(time);
        _lightning = false;
    }

    public bool ReturnLightningFalse()
    {
        return _lightning = false;
    }

    private void StopLightableBlock(Vector2 direction)
    {
        if (_lightning)
        {
            RaycastHit2D rayInfo = Physics2D.Raycast(transform.position, direction, .25f, _groundMask);
            if (rayInfo)
            {
                Ilightnable ilightnable = rayInfo.collider.GetComponent<Ilightnable>();

                if (ilightnable != null)
                {
                    ilightnable.ReturnLightningFalse();
                }
            }
        }
        else if(!_lightning)
        {
            _lightningDir = Vector2.zero;
        }
    }
    private void LadderMoviment()
    {
        if (_onLadder)
        {
            float yInput = Input.GetAxisRaw("Vertical");
            float moviment = yInput * _speed;
            Vector2 velocity = Vector2.up * moviment;
            velocity.x = _rb.velocity.x;
            _rb.velocity = velocity;
            _rb.gravityScale = 0;
        }
    }

    private void DetectLadder()
    {
        _onLadder = (Physics2D.OverlapCircle(transform.position, .25f, _ladderMask));
        _ac.SetBool("OnLadder", _onLadder);

        if (_onLadder)
        {
            if (_rb.velocity.y == 0)
            {
                _ac.speed = 0;
            }
            else
            {
                _ac.speed = 1;
            }
            _wand.SetActive(true);
        }
        else
        {
            _ac.speed = 1;
            _wand.SetActive(false);
        }
    }

    private void Spiked()
    {
        _spiked = true;
        transform.position = resetPos;
        IG_GameManager.Instance.HarryGotSpiked();
        StartCoroutine(Co_WaitToReturnSpiked());
    }

    private IEnumerator Co_WaitToReturnSpiked()
    {
        yield return new WaitForSeconds(.1f);
        _spiked = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        ICollectable collectable = collision.gameObject.GetComponent<ICollectable>();
        if (collectable != null)
        {
            collectable.ToCollect();
        }

        if (collision.gameObject.tag == "Door" && _grounded)
        {
            IG_UiManager.Instance.EndGameLogic();
            Destroy(gameObject, .5f);
        }

        if (collision.gameObject.tag == "Spike")
        {
            Spiked();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == Tags.SteelBlock || collision.gameObject.tag == Tags.Grid)
        {
            _lightning = false;
        }
    }
}
