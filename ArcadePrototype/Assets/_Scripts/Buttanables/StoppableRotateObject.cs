using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoppableRotateObject : RotateObject, IButtonable
{
    private bool _canRotate = true;

    protected override void Update()
    {
        if (_canRotate)
        {
            base.Update();
        }
    }
    public void ToInterract(bool state)
    {
        _canRotate = state;
    }
}
