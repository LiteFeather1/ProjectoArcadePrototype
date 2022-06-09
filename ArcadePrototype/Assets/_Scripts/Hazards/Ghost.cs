using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour
{
    [SerializeField] private Transform _playerTransform;
    [SerializeField] private float _speed;

    [Header("Distances")]
    private float _distance;
    [SerializeField] private float _distanceToChase;
    [SerializeField] private float _distanceToPatrol;
    private Vector3 _randomPosPatrol;
    private Vector2 _startPos;

    private bool _chassingPlayer;
    private bool _wasChassingPlayerLastFrame;

    [Header("Components")]
    private SpriteRenderer _sr;
    private HurtBox _hurtBox;

    [Header("Gizmos")]
    private bool _started;
    private Vector2 _startPosGizmos => transform.position;

    private void Awake()
    {
        _sr = GetComponent<SpriteRenderer>();
        _hurtBox = GetComponent<HurtBox>();
        _hurtBox.SetCanDoDamage(false);
    }

    private void Start()
    {
        _startPos = transform.position;
        RandomPointToPatrol();
        _started = true;
    }

    private void Update()
    {
        _distance = Vector2.Distance(transform.position, _playerTransform.position);

        if(_distance <= _distanceToChase && PlayerFacingMe())
        {
            Moviment(_playerTransform.position, _speed);
            _chassingPlayer = true;
        }
        else
        {
            _chassingPlayer = false;
            if(PlayerFacingMe())
                Moviment(_randomPosPatrol, _speed/2);
            if (transform.position == _randomPosPatrol)
                RandomPointToPatrol();
        }
        //Limit();
        HandleMyAlpha();
        HandleHurtBox();
        WasFacingPlayerLastFrame();
        WasChassingPlayerLastFrame();
    }

    private void OnDrawGizmos()
    {
        //Limit
        Gizmos.color = Color.green;
        if (!_started) Gizmos.DrawWireSphere(_startPosGizmos, _distanceToChase);
        else Gizmos.DrawWireSphere(_startPos, _distanceToChase);
        if (_started)
        {
            //RandomPoint to patroll
            Gizmos.DrawSphere(_randomPosPatrol, .25f);
        }

        //Distance That can Follow
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(_startPosGizmos, _distanceToChase);

        //Patrol
        Gizmos.color = Color.yellow;
        if (!_started) Gizmos.DrawWireSphere(_startPosGizmos, _distanceToPatrol);
        else Gizmos.DrawWireSphere(_startPos, _distanceToPatrol);
    }

    private bool PlayerFacingMe()
    {
        float myYRotation = transform.localEulerAngles.y;
        float playerYRotattion = _playerTransform.localEulerAngles.y;
        if (_distance <= _distanceToChase)
            return playerYRotattion != myYRotation;
        else
            return true;
    }
    /// <summary>
    /// Needs to be last method on Update
    /// </summary>
    private bool WasFacingPlayerLastFrame()
    {
        return  PlayerFacingMe();
    }

    private void HandleMyAlpha()
    {
        Color color = new Color(_sr.color.r, _sr.color.g, _sr.color.b, 1);
        Color colorHaflAlpha = new Color(_sr.color.r, _sr.color.g, _sr.color.b, .25f);
        _sr.color = PlayerFacingMe() ? color : colorHaflAlpha;
    }

    private void Moviment(Vector2 whatToChase, float speed)
    {
        HandleFaceDirection(whatToChase.x);
        transform.position = Vector2.MoveTowards(transform.position, whatToChase, speed * Time.deltaTime);
    }

    private void HandleFaceDirection(float whatImChassingX)
    {
        float directionX = transform.position.x - whatImChassingX;
        transform.rotation = Quaternion.Euler(0, directionX > 0 ? 0 : 180, 0);
    }

    private void Limit()
    {
        float radius = _distanceToChase; //radius from Start Position
        Vector3 centerPosition = _startPos; //center of from Start Position
        float distance = Vector3.Distance(transform.position, centerPosition); //distance between the object and the Start Position/ Center

        if (distance > radius) //If the distance is less than the radius, it is already within the circle.
        {
            Vector3 fromOriginToObject = transform.position - centerPosition;
            fromOriginToObject *= radius / distance; //Multiply by radius //Divide by Distance
            transform.position = centerPosition + fromOriginToObject; //Center + all that Math
        }
    }

    private void RandomPointToPatrol()
    {
        Vector2 randomPoint = Random.insideUnitCircle * (_distanceToPatrol - (_distanceToPatrol / 5));
        _randomPosPatrol = _startPos + randomPoint;
    }

    private void WasChassingPlayerLastFrame()
    {
        if (_wasChassingPlayerLastFrame != _chassingPlayer)
        {
            RandomPointToPatrol();
        }
        _wasChassingPlayerLastFrame = _chassingPlayer;
    }

    private void HandleHurtBox()
    {
        if (!WasFacingPlayerLastFrame())
            _hurtBox.SetCanDoDamage(false);
        else
            _hurtBox.SetCanDoDamage(true);
    }
}
