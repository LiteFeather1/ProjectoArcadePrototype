using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class BrainSwitchingBlock : MonoBehaviour
{
    [SerializeField] private float _blockTime;

    private float _timePassed;

    private Action _switch;

    public Action Switch { get => _switch; set => _switch = value; }

    private void Update()
    {
        _timePassed += Time.deltaTime;

        if(_timePassed >= _blockTime)
        {
            _timePassed = 0;
            _switch?.Invoke();
        }
    }
}
