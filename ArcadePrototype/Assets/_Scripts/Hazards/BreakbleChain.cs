using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakbleChain : MonoBehaviour, IDashInteractable
{
    [SerializeField] private BallSpike _myBall;
    [SerializeField] private RotateObject _ballRotation;
    [SerializeField] private Transform _myContent;

    private SpriteRenderer _sr;

    private void Awake()
    {
        _sr = GetComponent<SpriteRenderer>();
    }

    public void DashInteract()
    {
        _sr.enabled = false;
        _ballRotation.SetRotation(new Vector3(0, 0, 180));
        _myBall.AddForce(_myContent);
    }
}
