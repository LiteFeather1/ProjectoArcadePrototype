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
        if (IsGrounded())
            Gizmos.color = Color.green;
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
            RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right, .395f, _wallMask);

            Color rayColor;
            if (hit.collider != null)
            {
                rayColor = Color.green;
            }
            else
            {
                rayColor = Color.red;
            }
            Debug.DrawRay(transform.position, transform.right * .33f, rayColor, 0);

            return hit.collider != null;
        }
    }

    public bool WallNotFinished()
    {
        if (IsOnWall())
        {
            RaycastHit2D hit = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y + .5f), transform.right, .5f, _wallMask);

            Color rayColor;
            if (hit.collider != null)
            {
                rayColor = Color.green;
            }
            else
            {
                rayColor = Color.red;
            }

            Debug.DrawRay(new Vector2(transform.position.x, transform.position.y + .5f), transform.right * 1f, rayColor, 0);
            return hit.collider != null;
        }
        return true;
    }

    public Vector2 GetPistonSpeed()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 2f, _groundMask);

        if (hit.collider != null)
        {
            IIGiveSpeed iGiveSpeed = hit.collider.GetComponent<IIGiveSpeed>();
            if (iGiveSpeed != null)
            {
                Vector2 force = iGiveSpeed.GetMySpeed();
                print(force);
                if (force.y <= 1)
                    force.y = 1;
                if(force.y == 1)
                {
                    return new Vector2(force.x / 25, force.y);
                }
                else
                {
                    print(force / 25);
                    return force / 25;
                }
            }
            else
            {
                return Vector2.one;
            }
        }
        else
            return Vector2.one;
    }

    public Vector2 GetPistonSideSpeed()
    {
        RaycastHit2D hit2 = Physics2D.Raycast(transform.position, transform.right, 1f, _wallMask);
        if (hit2.collider != null)
        {
            IIGiveSpeed iGiveSpeed = hit2.collider.GetComponent<IIGiveSpeed>();
            if (iGiveSpeed != null)
            {
                Vector2 force = iGiveSpeed.GetMySpeed();
                if (force.y <= 1)
                    force.y = 1;
                if (force.x <= 1)
                    force.x = 1;
                return force / 20;
            }
            else
            {
                return Vector2.one;
            }
        }
        return Vector2.one;
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
        if (collectable != null)
            collectable.ToCollect();

        if (_isDashing)
        {
            IDashInteractable dashInteractable = collision.GetComponent<IDashInteractable>();
            if (dashInteractable != null)
                dashInteractable.DashInteract();
        }
    }
}

