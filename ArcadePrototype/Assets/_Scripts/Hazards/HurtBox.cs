using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtBox : MonoBehaviour
{
    [SerializeField] private int _hitAmount = 1;
    [SerializeField] private float _stunDuration;
    [SerializeField] private bool _onlyColl;
    private bool _canDamage  = true;
    //[SerializeField] private bool _onlyTrigger;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        IDamageable damageable = collision.gameObject.GetComponent<IDamageable>();

        if(damageable != null)
        {
            if(_canDamage)
                damageable.TakeDamage(_hitAmount, _stunDuration);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_onlyColl) return;
        IDamageable damageable = collision.gameObject.GetComponent<IDamageable>();

        if (damageable != null)
        {
            print(_canDamage);
            if (_canDamage)
            {
                damageable.TakeDamage(_hitAmount, _stunDuration);
                print(_canDamage);
            }
        }
    }

    public void SetMyDamage(int damage)
    {
        _hitAmount = damage;
    }

    public void SetCanDoDamage(bool boolean)
    {
        _canDamage = boolean;
    }
}

