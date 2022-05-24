using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakablePlatLiftOff : BreakablePlatTimed
{
    private bool _canGetDestroyed;
    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            _canGetDestroyed = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if(Input.GetKey(KeyCode.Space))
            {
                StartCoroutine(Co_Logic());
            }    
        }
    }
}
