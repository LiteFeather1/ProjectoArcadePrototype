using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSpikeRandomizeDirection : BallSpike
{
    private bool _canAddForce = true;
    private float _coolDown = 0.5f;

    IEnumerator CoolDown()
    {
        yield return new WaitForSeconds(_coolDown);
        _canAddForce = true;
    }
    //Adds a random force 
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (_canAddForce)
        {
            _canAddForce = false;
            _rb.velocity = Vector2.zero;
            AddStartingForce();
            if(this.enabled)
            StartCoroutine(CoolDown());
        }
    }

    private void OnDisable()
    {
        _canAddForce = true;
    }
}
