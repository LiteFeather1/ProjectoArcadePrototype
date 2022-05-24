using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallStamina : MonoBehaviour
{
    [SerializeField] private float _stamina = 100;
    private float _maxStamina = 100;

    [Header ("Demishers By Time")]
    [SerializeField] private float _wallGrippingDemish;
    [SerializeField] private float _wallClimbingDemish;

    [Header ("Demishers By Input")]
    [SerializeField] private float _wallJumpDeminsh;

    private bool _wasOnGroundLastFrame;

    [Header("Components")]
    Detections _d;

    public float Stamina { get => _stamina;}

    private void Awake()
    {
        _d = GetComponent<Detections>();
    }

    private void Update()
    {
        Main_UiManager.Instance.StaminaBarToDisplay(_stamina, _maxStamina);
        ReplenishDashOnceGroundedAgain();
    }

    public void DemishFromWallJump()
    {
        _stamina -= _wallJumpDeminsh;
    }

    public void DemishFromWallGripping()
    {
        _stamina -= Time.deltaTime * _wallGrippingDemish;
    }

    public void DemishFromWallClimbing()
    {
        _stamina -= Time.deltaTime * _wallClimbingDemish;
    }

    public void ReplenishStamina()
    {
        _stamina = _maxStamina;
    }

    private void ReplenishDashOnceGroundedAgain()
    {
        if (_wasOnGroundLastFrame != _d.IsGrounded())
        {
            _stamina = _maxStamina;
        }
        _wasOnGroundLastFrame = _d.IsGrounded();
    }
}
