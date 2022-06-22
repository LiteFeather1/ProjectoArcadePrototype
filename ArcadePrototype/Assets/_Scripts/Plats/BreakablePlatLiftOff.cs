using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakablePlatLiftOff : ShakingPlat
{
    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if(Input.GetKey(KeyCode.Space))
            {
                StartCoroutine(Co_Logic());
            }    
        }
    }
}
