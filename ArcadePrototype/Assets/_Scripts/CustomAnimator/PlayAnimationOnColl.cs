using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAnimationOnColl : MonoBehaviour
{
    private CustomAnimator _customAnimator;

    private void Awake()
    {
        _customAnimator = GetComponent<CustomAnimator>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            _customAnimator.StopTheCo();
            _customAnimator.PlayAnimation(transform);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            _customAnimator.StopTheCo();
            _customAnimator.PlayAnimation(transform);
        }
    }
}
