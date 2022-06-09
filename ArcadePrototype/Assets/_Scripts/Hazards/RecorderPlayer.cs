using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecorderPlayer : MonoBehaviour
{
    [Header ("What To Record")]
    [SerializeField] private Transform _whatToRecord;
    private Rigidbody2D _playerRb;
    private PlayerHitBox _playerHitbox;

    [Header ("What Im Recording")]
    [SerializeField] private List<Vector3> _positions = new List<Vector3>();
    [SerializeField] private List<float> _speeds = new List<float>();

    [Header ("Frame Rate")]
    [SerializeField] private float _howOftenToRecord;
    private float _frameCount;
    [SerializeField] int _maxListSize;

    private Vector2 _startPos;

    private void Awake()
    {
        _playerRb = _whatToRecord.GetComponent<Rigidbody2D>();
        _playerHitbox = _whatToRecord.GetComponent<PlayerHitBox>();
    }

    private void Start()
    {
        _startPos = transform.position;
        _playerHitbox.Death.AddListener(Reset);
    }


    void Update()
    {
        FrameCounter();
    }

    private void FrameCounter()
    {
        _frameCount++;
        if (_frameCount == _howOftenToRecord)
        {
            _frameCount = 0;
            RecordPosition();
            RecordSpeed();
            Eraser();
            Moviment();
        }
    }

    private void RecordPosition()
    {
        if(_positions.Count <= _maxListSize)
            _positions.Add(_whatToRecord.transform.position);
    }

    private void RecordSpeed()
    {
        if (_speeds.Count > _maxListSize)
            return;
        float speedToAdd = _playerRb.velocity.magnitude;

        if(speedToAdd <= 5)
        {
            speedToAdd = 5;
        }

        _speeds.Add(speedToAdd);
    }

    private void Eraser()
    {
        float distance = Vector2.Distance(transform.position, _positions[0]);
        if (distance <= 0.33f)
        {
            _positions.RemoveAt(0);
            _speeds.RemoveAt(0);
        }
    }

    private void Moviment()
    {
        transform.position = Vector2.MoveTowards(transform.position, _positions[0], _speeds[0] * Time.deltaTime);
    }

    private void Reset()
    {
        _positions.Clear();
        _speeds.Clear();  
        transform.position = _startPos;
    }
}
