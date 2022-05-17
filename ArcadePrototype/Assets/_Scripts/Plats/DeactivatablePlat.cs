using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeactivatablePlat : MonoBehaviour, IIteractable
{
    private Collider2D _collider;

    private void Start()
    {
        _collider = GetComponent<Collider2D>();
    }

    IEnumerator Co_DeactivatePlat()
    {
        _collider.enabled = false;
        yield return new WaitForSeconds(0.25f);
        _collider.enabled = true;
    }

    public void ToInteract()
    {
        StartCoroutine(Co_DeactivatePlat());
    }
}
