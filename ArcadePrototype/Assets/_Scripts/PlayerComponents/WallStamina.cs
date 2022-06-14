using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallStamina : MonoBehaviour
{
    [SerializeField] private float _stamina = 100;
    private float _maxStamina;

    [Header ("Demishers By Time")]
    [SerializeField] private float _wallGrippingDemish;
    [SerializeField] private float _wallClimbingDemish;

    [Header ("Demishers By Input")]
    [SerializeField] private float _wallJumpStraight;
    [SerializeField] private float _wallJump    ;

    private bool _wasOnGroundLastFrame;

    [Header("Components")]
    Detections _d;

    private Main_InGameUiManager _igUiGameInstance;

    public float Stamina { get => _stamina;}

    private void Awake()
    {
        _d = GetComponent<Detections>();
        _maxStamina = _stamina;
    }

    private void Update()
    {
        _igUiGameInstance = Main_InGameUiManager.Instance;
        _igUiGameInstance?.StaminaBarToDisplay(_stamina, _maxStamina);
        ReplenishDashOnceGroundedAgain();
    }

    public void DemishFromWallJumpStraight()
    {
        _stamina -= _wallJumpStraight;
    }

    public void DemishFromWallJump()
    {
        _stamina -= _wallJump;
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
