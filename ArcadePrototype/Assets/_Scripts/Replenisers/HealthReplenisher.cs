using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthReplenisher : MonoBehaviour
{
    [SerializeField] private int _amountToRestore;

    private ParticleSystem _pS;
    private void Awake()
    {
        _pS = GetComponent<ParticleSystem>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            collision.GetComponent<PlayerHitBox>().RestoreHealth(_amountToRestore);
            _pS.Play();
            GetComponent<SpriteRenderer>().enabled = false;
            Destroy(gameObject,1f);
        }
    }
}
