using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZeroVelocity : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Rigidbody2D rbToStop = collision.gameObject.GetComponent<Rigidbody2D>();
        if(rbToStop != null)
            rbToStop.velocity = Vector2.zero;
    }
}
