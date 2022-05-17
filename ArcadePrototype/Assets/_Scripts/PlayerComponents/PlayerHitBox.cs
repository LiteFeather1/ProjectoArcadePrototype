using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHitBox : MonoBehaviour, IDamageable
{
    [SerializeField] private int _hitsToReset = 3;
    [SerializeField] private float _invulnerabilityTime = 1f;
    private bool _canGetHit = true;
    private Vector2 _resetPos;

    private void Awake()
    {
        _resetPos = transform.position;
    }

    public void TakeDamage(int hitAmount)
    {
        if (_canGetHit)
        {
            _canGetHit = false;
            _hitsToReset -= hitAmount;
            StartCoroutine(Co_invulnerability());
            if (_hitsToReset <= 0)
            {
                Die();
            }
        }
    }

    public void Die()
    {
        transform.position = _resetPos;
        _hitsToReset = 3;
    }

    IEnumerator Co_invulnerability()
    {
        yield return new WaitForSeconds(_invulnerabilityTime);
        _canGetHit = true;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "CheckPoint")
        {
            _resetPos = collision.transform.position;
        }
    }
}
