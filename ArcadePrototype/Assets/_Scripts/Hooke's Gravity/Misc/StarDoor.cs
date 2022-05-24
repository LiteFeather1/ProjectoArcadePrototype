using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarDoor : MonoBehaviour
{
    [SerializeField] private BoxCollider2D _collider;
    [SerializeField] private LayerMask _groundMask;

    private void Update()
    {
        Grounded();
    }

    private void Grounded()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 1.5f, _groundMask);
        //Debug.DrawRay(transform.position, new Vector2(0, -1.5f), Color.red);
        if(hit)
        {
            _collider.enabled = true;
        }
        else
        {
            _collider.enabled = false;
        }
    }
}
