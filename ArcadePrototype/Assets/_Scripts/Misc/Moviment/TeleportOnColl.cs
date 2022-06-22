using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportOnColl : MonoBehaviour
{
    [SerializeField] private Transform _pointToTeleport;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            collision.transform.position = _pointToTeleport.position;
        }
    }
}
