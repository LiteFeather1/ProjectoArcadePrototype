using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddForceOnCollision : MonoBehaviour
{
    [SerializeField] private float _bounceForce = 250;
    [SerializeField] private float _disableTime = 1;
    [SerializeField] private bool _onlyFromAbove;
    [SerializeField] private bool _onlyUp;

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        Jump playerJump = collision.gameObject.GetComponent<Jump>();
        if(playerJump != null)
        {
            if (_onlyFromAbove)
                if (transform.position.y +.55f >= collision.transform.position.y) 
                    return;

            if (!_onlyUp)
            {
                Vector2 normal = collision.GetContact(0).normal;
                playerJump.AddForceOnCollision(-normal, _bounceForce, _disableTime);
            }
            else 
                playerJump.AddForceOnCollision(Vector2.up, _bounceForce, _disableTime);
        }
    }
}
