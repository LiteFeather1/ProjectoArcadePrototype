using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MathHelper
{
    public static float AngleBetweenTwoPoints(Vector3 a, Vector3 b)
    {
        return Mathf.Atan2(a.y - b.y, a.x - b.x) * Mathf.Rad2Deg;
    }
    public static float Spread(int i, float spreadFactor, int howManyBullets)
    {
        float totalSpread = spreadFactor * 2;
        float divisor = howManyBullets - 1;
        if (divisor != 0)
        {
            float pointsSeparations = totalSpread / divisor;
            float rotations;
            rotations = spreadFactor - (pointsSeparations * i);
            return rotations;
        }
        return 0;
    }
}
