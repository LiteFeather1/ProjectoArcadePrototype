using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlickBoxCollider : MonoBehaviour
{
    [SerializeField] private BoxCollider2D _component;
    [SerializeField] private float _timeBetween;

    void Start()
    {
        StartCoroutine(Co_Flicker());
    }

    IEnumerator Co_Flicker()
    {
        _component.enabled = false;
        yield return new WaitForSeconds(.1f);
        _component.enabled = true;
        yield return new WaitForSeconds(_timeBetween);
        StartCoroutine(Co_Flicker());
    }
}
