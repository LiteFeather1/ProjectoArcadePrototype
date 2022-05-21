using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireTrap : MonoBehaviour, IButtonable
{
    [SerializeField] private int _damage;
    [SerializeField] private GameObject _myFire;
    [SerializeField] private float _timeActivated;
    [SerializeField] private float _timeDeactivated;
    [SerializeField] private bool _startActivated;
    private bool _activated;
    private bool _canContinue = true;

    protected virtual void Start()
    {
        if (_startActivated)
        {
            StartCoroutine(Activated_Co());
        }
        else StartCoroutine(Deactivated_Co());
        _myFire.GetComponent<HurtBox>().SetMyDamage(_damage);
    }

    protected IEnumerator Activated_Co()
    {
        _activated = true;
        yield return new WaitForSeconds(_timeActivated);
        _myFire.SetActive(false);
        if(_canContinue)
        StartCoroutine(Deactivated_Co());
    }

    protected IEnumerator Deactivated_Co()
    {
        _activated = false;
        yield return new WaitForSeconds(_timeDeactivated);
        _myFire.SetActive(true);
        StartCoroutine(Activated_Co());
    }

    public void ToInterract(bool state)
    {
        _canContinue = state;

        if(_activated)
            StartCoroutine(Deactivated_Co());
        else
            StartCoroutine(Activated_Co());
    }
}
