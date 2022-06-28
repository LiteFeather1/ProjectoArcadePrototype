using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class AlternatingBlocks : MonoBehaviour, IButtonable
{
    [SerializeField] private BrainAlternatingBlock _brain;

    [SerializeField] private WhatNumberAmI _whatNumberAmI;
    private bool _active = true;

    private SpriteRenderer _sr;
    private BoxCollider2D _bc;
    
    private enum WhatNumberAmI
    {
        One,
        Two
    }

    private void OnEnable()
    {
        _brain.Switch += Logic;
    }

    private void Awake()
    {
        _sr = GetComponent<SpriteRenderer>();
        _bc = GetComponent<BoxCollider2D>();
    }

    private void Start()
    {
        if (_whatNumberAmI == WhatNumberAmI.One)
        {
            _active = false;
        }
        ControlEnables(_active);
    }

    private void OnDisable()
    {
        _brain.Switch -= Logic;
    }

    private void Logic()
    {
        _active = !_active;
        ControlEnables(_active);
    }

    private void ControlEnables(bool state)
    {
        _sr.enabled = state;
        _bc.enabled = state;
    }

    public void ToInterract(bool state)
    {
        throw new System.NotImplementedException();
    }
}
