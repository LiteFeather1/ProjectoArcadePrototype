using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddForceOnCollision : MonoBehaviour
{
    [SerializeField] private float _bounceForce = 250;
    [SerializeField] private float _disableTime = 1;

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        Jump playerJump = collision.gameObject.GetComponent<Jump>();
        if(playerJump != null)
        {
            Vector2 normal = collision.GetContact(0).normal;
            playerJump.AddForceOnCollision(-normal, _bounceForce, _disableTime);
        }
    }
}
