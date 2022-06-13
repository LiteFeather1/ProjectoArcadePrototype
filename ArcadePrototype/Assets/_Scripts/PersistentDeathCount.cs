using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistentDeathCount : ComplexSingleton<PersistentDeathCount>
{
    [SerializeField] private int _deathCount;

    public void AddDeath()
    {
        _deathCount++;

        Main_InGameUiManager.Instance?.DeathCountToDisplay(_deathCount);
    }

    public void UpdateDeathCount()
    {
        Main_InGameUiManager.Instance?.DeathCountToDisplay(_deathCount);
    }
}
