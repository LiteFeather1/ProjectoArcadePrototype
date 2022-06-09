using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour
{
    [SerializeField] private Transform _playerTransform;
    [SerializeField] private float _speed;

    [Header ("Distances")]
    [SerializeField] private float _distanceToChase;
    [SerializeField] private float _distanceToPatrol;
    private Vector3 _randomPosPatrol;
    private Vector2 _startPos;

    private bool _followingPlayer;
    private bool _followingPlayerLastFrame;

    //hurtBox disable

    [Header("Components")]
    private SpriteRenderer _sr;

    private void Awake()
    {
        _sr = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        _startPos = transform.position;
        RandomPointToPatrol();
    }

    private void Update()
    {
        float distance = Vector2.Distance(transform.position, _playerTransform.position);

        if(distance <= _distanceToChase && PlayerFacingMe())
        {
            Moviment(_playerTransform.position);
        }
        else
        {
            Moviment(_randomPosPatrol);
            if (transform.position == _randomPosPatrol)
                RandomPointToPatrol();
        }
        WasFollowinPlayerLastFrame();
        Limit();
        HandleMyAlpha();
    }

    private bool PlayerFacingMe()
    {
        float myYRotation = transform.localEulerAngles.y;
        float playerYRotattion = _playerTransform.localEulerAngles.y;
        return playerYRotattion == myYRotation;
    }

    private void HandleMyAlpha()
    {
        Color color = new Color(_sr.color.r, _sr.color.g, _sr.color.b, 1);
        Color colorHaflAlpha = new Color(_sr.color.r, _sr.color.g, _sr.color.b, .25f);
        _sr.color = PlayerFacingMe() ? color : colorHaflAlpha;
    }

    private void Moviment(Vector2 whatToChase)
    {
        transform.position = Vector2.MoveTowards(transform.position, whatToChase, _speed * Time.deltaTime);
        HandleFaceDirection(whatToChase.x);
    }

    private void HandleFaceDirection(float directionX)
    {
        transform.rotation = Quaternion.Euler(0, directionX >= 0 ? 0 : 180, 0);
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
        Vector2 randomPoint = Random.insideUnitCircle * (_distanceToChase - (_distanceToChase / 5));
        _randomPosPatrol = _startPos + randomPoint;
    }

    private void WasFollowinPlayerLastFrame()
    {
        if (_followingPlayer != _followingPlayerLastFrame)
        {
            RandomPointToPatrol();
        }
        _followingPlayerLastFrame = _followingPlayer;
    }
}
