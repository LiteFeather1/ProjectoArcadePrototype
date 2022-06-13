using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistentTimer : ComplexSingleton<PersistentTimer>
{
    [SerializeField] private float _time;

    public float Timer { get => _time; }

    private void Update()
    {
        _time += Time.deltaTime;
    }
    
    public void DisableMe()
    {
        this.enabled = false;
    }
}
