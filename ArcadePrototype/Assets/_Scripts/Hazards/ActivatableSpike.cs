using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivatableSpike : MonoBehaviour
{
    [SerializeField] private float _delayToActivate;

    private bool _activated;

    private Animator _ac;
    [SerializeField] private Collider2D _collider;

    private void Awake()
    {
        _ac = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            StartCoroutine(MoveSpikes_Co());
        }
    }

    IEnumerator MoveSpikes_Co()
    {
        yield return new WaitForSeconds(_delayToActivate);
        _ac.SetBool("Activated", true);
        _collider.enabled = true;
        yield return new WaitForSeconds(_delayToActivate * 2);
        _ac.SetBool("Activated", false);
        _collider.enabled = false;
    }
}
