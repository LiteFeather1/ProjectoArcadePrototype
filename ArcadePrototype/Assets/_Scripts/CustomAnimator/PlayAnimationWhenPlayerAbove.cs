using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAnimationWhenPlayerAbove : MonoBehaviour
{
    [SerializeField] private Animator _ac;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        _ac.SetBool("PlayerAbove", true);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        _ac.SetBool("PlayerAbove", false);
    }
}
