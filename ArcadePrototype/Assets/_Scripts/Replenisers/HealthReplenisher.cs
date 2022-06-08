using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthReplenisher : MonoBehaviour
{
    [SerializeField] private int _amountToRestore;
    private bool _canBeCollected;

    private SpriteRenderer _sr;
    private ParticleSystem _pS;

    [SerializeField] private PlayerHitBox _player;

    private void OnEnable()
    {
        _player.Death.AddListener(Reset);
    }

    private void Awake()
    {
        _pS = GetComponent<ParticleSystem>();
        _sr = GetComponent<SpriteRenderer>();
    }

    private void Reset()
    {
        _canBeCollected = true;
        _sr.enabled = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player" && _canBeCollected)
        {
            _canBeCollected = false;
            collision.GetComponent<PlayerHitBox>().RestoreHealth(_amountToRestore);
            _pS.Play();
            _sr.enabled = false;
        }
    }
}
