using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistentScore : ComplexSingleton<PersistentScore>
{
    [SerializeField] private float _score;
    [SerializeField] private float _loseMultiplierPerSec;
    [SerializeField] private float _loseOnDeath;

    public float Score { get => _score; }

    private void Update()
    {
        float howMuchToLose = _loseMultiplierPerSec * Time.deltaTime;
        _score -= howMuchToLose;
    }

    public void LoseOnDeath()
    {
        _score -= _loseOnDeath;
    }

    public void DisableMe()
    {
        this.enabled = false;
    }
}
