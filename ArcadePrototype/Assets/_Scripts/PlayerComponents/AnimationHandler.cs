using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationHandler : MonoBehaviour
{
    private Animator _ac;
    private Rigidbody2D _rb;
    private void Awake()
    {
        _ac = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        _ac.SetFloat("HorizontalSpeed", Mathf.Abs(_rb.velocity.x));
    }
}
