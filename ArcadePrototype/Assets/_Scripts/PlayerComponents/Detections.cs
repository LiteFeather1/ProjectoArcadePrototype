using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detections : MonoBehaviour
{
    [SerializeField] private LayerMask _groundMask;
    [SerializeField] private LayerMask _wallMask;
    private CapsuleCollider2D _collider;

    private void Awake()
    {
        _collider = GetComponent<CapsuleCollider2D>();
    }
    private void Update()
    {

    }

    public bool IsGrounded()
    {
        float extraHeightText = .125f;
        RaycastHit2D hit = Physics2D.BoxCast(_collider.bounds.center, _collider.bounds.size, 0f, Vector2.down, extraHeightText, _groundMask);
        //RaycastHit2D hit = Physics2D.CapsuleCast(_collider.bounds.center, _collider.bounds.size, CapsuleDirection2D.Vertical, 0f, Vector2.down, extraHeightText, _groundMask);
        Color rayColor;
        if(hit.collider != null)
        {
            rayColor = Color.green;
        }
        else
        {
            rayColor = Color.red;
        }
        Debug.DrawRay(_collider.bounds.center + new Vector3(_collider.bounds.extents.x, 0), Vector2.down * (_collider.bounds.extents.y + extraHeightText), rayColor);
        return hit.collider != null;
    }

    public bool IsOnWall()
    {
        if (!IsGrounded())
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right, .25f, _wallMask);

            Color rayColor;
            if (hit.collider != null)
            {
                rayColor = Color.green;
            }
            else
            {
                rayColor = Color.red;
            }
            Debug.DrawRay(transform.position, transform.right * .25f, rayColor, 0);

            return hit.collider != null;
        }

        return false;
    }

}

