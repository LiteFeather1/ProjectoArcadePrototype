using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakablePlatLiftOff : BreakablePlatTimed
{
    protected override void OnCollisionEnter2D(Collision2D collision)
    {

    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (collision.gameObject.GetComponent<Rigidbody2D>().velocity.y > 5)
            {
                StartCoroutine(Co_Logic());
            }
        }
    }
}
