using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDisableMyCollider : MonoBehaviour
{
    [SerializeField] private Collider2D _myColl;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        _myColl.enabled = false;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        _myColl.enabled = true;
    }
}
