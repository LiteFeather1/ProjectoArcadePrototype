using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalTeleporter : MonoBehaviour
{
    [SerializeField] private Transform _whereToTeleport;
    [SerializeField] WhatTypeOfPortalTeleporter _whatType;
    [SerializeField] private GameObject _transfromUp, _transfromRight;

    private static bool _canTeleport = true;

    private enum WhatTypeOfPortalTeleporter
    {
        ConserveVelocty,
        InvertXVelocity,
        AddVelocityToY,
        MakeXEqualToYAndYEqualTo0,
        MakeYEqualXAndXEqual0,
        AddVelocityToYMakeXEqualYAndXEqual0
    }

    private void Start()
    {
        _transfromRight.SetActive(false);
        _transfromUp.SetActive(false);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color32(255, 165, 0, 255);
        Gizmos.DrawLine(transform.position, _whereToTeleport.position);
    }

    private void Main(Rigidbody2D rbToEffect)
    {
        switch (_whatType)
        {
            case WhatTypeOfPortalTeleporter.ConserveVelocty:
                ConserveVelocity(rbToEffect);
                break;
            case WhatTypeOfPortalTeleporter.AddVelocityToY:
                AddVelocityY(rbToEffect);
                break;
            case WhatTypeOfPortalTeleporter.MakeXEqualToYAndYEqualTo0:
                MakeXEqualToYAndYEqual0(rbToEffect);
                break;
            case WhatTypeOfPortalTeleporter.MakeYEqualXAndXEqual0:
                MakeYEqualXAndXEqual0(rbToEffect);
                break;
            case WhatTypeOfPortalTeleporter.AddVelocityToYMakeXEqualYAndXEqual0:
                AddVelocityToYMakeXEqualYAndXEqual0(rbToEffect);
                break;
            case WhatTypeOfPortalTeleporter.InvertXVelocity:
                InvertXVelocity(rbToEffect);
                break;
        }

        rbToEffect.transform.position = _whereToTeleport.position;
        StartCoroutine(CanTeleportDelay());
    }

    IEnumerator CanTeleportDelay()
    {
        yield return new WaitForSeconds(0.1f);
        _canTeleport = true;
    }

    #region Cases

    private void ConserveVelocity(Rigidbody2D rbToEffect)
    {
        rbToEffect.velocity = rbToEffect.velocity;
    }

    private void AddVelocityY(Rigidbody2D rbToEffect)
    {
        float yVelocity;
        if (rbToEffect.velocity.y < 15)
            yVelocity = 15;
        else
            yVelocity = rbToEffect.velocity.y;
        rbToEffect.velocity = new Vector2(rbToEffect.velocity.x, yVelocity * Mathf.Sign(_whereToTeleport.up.y));
    }

    private void MakeXEqualToYAndYEqual0(Rigidbody2D rbToEffect)
    {
        float newXVelocity = rbToEffect.velocity.y - 15;
        rbToEffect.velocity = new Vector2(newXVelocity * -Mathf.Sign(_whereToTeleport.right.x), 0f);
    }

    private void MakeYEqualXAndXEqual0(Rigidbody2D rbToEffect)
    {
        float newYVelocty = rbToEffect.velocity.x + 5;
        rbToEffect.velocity = new Vector2(rbToEffect.velocity.x, newYVelocty * Mathf.Sign(_whereToTeleport.up.y));
        rbToEffect.GetComponent<Jump>()?.StartDisableGravity(0.25f);
    }
    private void AddVelocityToYMakeXEqualYAndXEqual0(Rigidbody2D rbToEffect)
    {
        float yVelocity;
        if (rbToEffect.velocity.y < 30)
            yVelocity = 30;
        else
            yVelocity = rbToEffect.velocity.y;

        rbToEffect.velocity = new Vector2(yVelocity * Mathf.Sign(_whereToTeleport.right.x), 0f);
    }

    private void InvertXVelocity(Rigidbody2D rbToEffect)
    {
        rbToEffect.velocity = new Vector2(rbToEffect.velocity.x * -1, rbToEffect.velocity.y);
        rbToEffect.GetComponent<HorizontalMoviment>()?.StartDisableInput();
    }

    #endregion

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_canTeleport)
        {
            _canTeleport = false;
            Rigidbody2D rbToEffect = collision.GetComponent<Rigidbody2D>();
            if (rbToEffect != null)
            {
                Main(rbToEffect);
            }
        }
        print(transform.name);
    }
}
