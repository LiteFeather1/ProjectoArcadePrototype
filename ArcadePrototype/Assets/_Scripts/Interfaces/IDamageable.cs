using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface IDamageable
{
    void TakeDamage(int hitAmount, float stunDuration);

    void Die();
}
