using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IftriggerUntrigger : MonoBehaviour
{
    private BoxCollider2D _myCollider;
    private void Start()
    {
        _myCollider = GetComponent<BoxCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        _myCollider.enabled = false;  
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        _myCollider.enabled = true;
    }
}
