using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddForceToPlayerOnCollision : AddForceOnCollision
{

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            HorizontalMoviment hm = collision.gameObject.GetComponent<HorizontalMoviment>();
            hm.enabled = false;
            base.OnCollisionEnter2D(collision);
        }
    }
}
