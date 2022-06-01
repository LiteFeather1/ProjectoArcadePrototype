using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObject : MonoBehaviour
{
    [SerializeField] protected Vector3 _rotation;

    protected virtual void Update()
    {
        transform.Rotate(_rotation * Time.deltaTime);
    }

    public void InverseRotation()
    {
        _rotation = _rotation * -1;
    }
}
