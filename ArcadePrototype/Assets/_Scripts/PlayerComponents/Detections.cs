using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detections : MonoBehaviour
{
    [SerializeField] private LayerMask _groundMask;
    [SerializeField] private LayerMask _wallMask;
    [SerializeField] private Transform _rightFoot;
    [SerializeField] private Transform _leftFoot;
    private bool _isDashing;

    private Animator _ac;

    private void Awake()
    {
        _ac = GetComponent<Animator>();
    }
    private void Update()
    {
        _ac.SetBool("Grounded",IsGrounded());
    }

    private void OnDrawGizmos()
    {
        if (IsGrounded()) Gizmos.color = Color.green;
        else Gizmos.color = Color.red;

        Gizmos.DrawWireSphere(_leftFoot.position, .0675f);
        Gizmos.DrawWireSphere(_rightFoot.position, .0675f);
    }
    public bool IsGrounded()
    {
        Collider2D hit1 = Physics2D.OverlapCircle(_rightFoot.position, .0675f, _groundMask);
        Collider2D hit2 = Physics2D.OverlapCircle(_leftFoot.position, .0675f, _groundMask);

        return hit1 != null || hit2 != null;
    }

    public bool IsOnWall()
    {
        if (true)
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
            return hit.collider != null;
        }
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

    public bool IsDashing()
    {
        return _isDashing;
    }

    public bool SetDashing(bool dashing)
    {
        _isDashing = dashing;
        return dashing;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        ICollectable collectable = collision.GetComponent<ICollectable>();
        if (collectable != null) collectable.ToCollect();
    }
}

