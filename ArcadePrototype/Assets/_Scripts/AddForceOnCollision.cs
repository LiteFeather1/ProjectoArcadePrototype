using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddForceOnCollision : MonoBehaviour
{
    [SerializeField] protected float _bounceForce;


    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();
        if(rb != null)
        {
            Vector2 normal = collision.GetContact(0).normal;
            rb.AddForce(-normal * _bounceForce);
        }
    }
}
