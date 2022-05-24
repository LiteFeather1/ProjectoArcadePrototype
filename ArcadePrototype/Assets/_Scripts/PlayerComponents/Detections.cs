using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detections : MonoBehaviour
{
    [SerializeField] private LayerMask _groundMask;
    [SerializeField] private LayerMask _wallMask;

    private CapsuleCollider2D _collider;
    private Animator _ac;
    private void Awake()
    {
        _collider = GetComponent<CapsuleCollider2D>();
        _ac = GetComponent<Animator>();
    }
    private void Update()
    {
        _ac.SetBool("Grounded",IsGrounded());
    }

    public bool IsGrounded()
    {
        float extraHeightText = .125f;
        Vector2 size = new Vector2(_collider.bounds.size.x - .2f, _collider.bounds.size.y);
        RaycastHit2D hit = Physics2D.BoxCast(_collider.bounds.center, size, 0f, Vector2.down, extraHeightText, _groundMask);
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
            RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right, .33f, _wallMask);

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
            print(hit.collider);
            return hit.collider != null;
        }

        return false;
    }

    public float GetPistonSpeed()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 1f, _groundMask);
    

        if (hit.collider != null)
        {
            Piston piston = hit.collider.GetComponent<Piston>();
            if (piston != null)
            {
                if (piston.GetMySpeed() <= 1)
                {
                    return 1;
                }
                else
                {
                    print(piston.GetMySpeed() / 10);
                    return piston.GetMySpeed() / 10;
                }
            }
            else
            {
                return 1;
            }
        }
        return 1;
    }

    public float GetPistonSideSpeed()
    {
        RaycastHit2D hit2 = Physics2D.Raycast(transform.position, transform.right, 1f, _wallMask);
        if (hit2.collider != null)
        {
            Piston piston = hit2.collider.GetComponent<Piston>();
            if (piston != null)
            {
                if (piston.GetMySpeed() == 1)
                {
                    return 1;
                }
                else
                {
                    print(piston.GetMySpeed() / 5);
                    return piston.GetMySpeed() / 5;
                }
            }
            else
            {
                return 1;
            }
        }
        return 1;
    }

}

