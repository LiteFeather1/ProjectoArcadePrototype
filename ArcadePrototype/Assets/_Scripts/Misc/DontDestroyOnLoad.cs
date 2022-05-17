using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyOnLoad : ComplexSingleton<DontDestroyOnLoad>
{
    protected override void Awake()
    {
        base.Awake();
    }
}
