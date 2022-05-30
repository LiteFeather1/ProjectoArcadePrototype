using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthReplenisher : MonoBehaviour
{
    [SerializeField] private int _amountToRestore;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            collision.GetComponent<PlayerHitBox>().RestoreHealth(_amountToRestore);
            Destroy(gameObject);
        }
    }
}
