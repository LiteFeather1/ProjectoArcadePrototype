using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float _stunDuration;
    private int _damage;
    private float _speed;

    private Rigidbody2D _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    public void Spawn(int damage,float stunDuration, float speed, Vector2 direction, float lifeSpan)
    {
        _damage = damage;
        _speed = speed;
        _stunDuration = stunDuration;
        _rb.velocity = (direction * _speed);
        Destroy(gameObject, lifeSpan);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        IDamageable damageble = collision.gameObject.GetComponent<IDamageable>();

        if (damageble != null)
        {
            damageble.TakeDamage(_damage, _stunDuration);
            Destroy(gameObject);
        }
        Destroy(gameObject);
    }
}
