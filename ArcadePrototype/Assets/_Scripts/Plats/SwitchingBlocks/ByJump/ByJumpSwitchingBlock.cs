using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ByJumpSwitchingBlock : MonoBehaviour
{
    [SerializeField] private WhatBlockAmI _whatBlockAmI;
    private bool _active = true;

    [SerializeField] private Jump _playerJump;
    [SerializeField] private WallJump _wallJump;
    private SpriteRenderer _sr;
    private BoxCollider2D _bc;

    private enum WhatBlockAmI
    {
        One,
        Two
    }

    private void OnEnable()
    {
        _playerJump.JumpedEvent += Switch;
        _playerJump.DoubledJumpEvent += Switch;
        _wallJump.WallJumpEvent += Switch;
    }

    private void Awake()
    {
        _sr = GetComponent<SpriteRenderer>();
        _bc = GetComponent<BoxCollider2D>();
    }

    private void Reset()
    {
        _playerJump = FindObjectOfType<Jump>();
    }

    private void Start()
    {
        if (_whatBlockAmI == WhatBlockAmI.Two)
        {
            _active = false;
        }
        Switch();
    }

    private void OnDisable()
    {
        _playerJump.JumpedEvent -= Switch;
        _playerJump.DoubledJumpEvent -= Switch;
        _wallJump.WallJumpEvent -= Switch;
    }

    private void Switch()
    {
        _active = !_active;
        ControlEnable(_active);
    }

    private void ControlEnable(bool state)
    {
        _sr.enabled = state;
        _bc.enabled = state;
    }
}