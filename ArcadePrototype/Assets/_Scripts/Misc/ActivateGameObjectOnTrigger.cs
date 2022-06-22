using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateGameObjectOnTrigger : MonoBehaviour
{
    [SerializeField] private GameObject[] _whatToActivate = new GameObject[1];
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            foreach (var item in _whatToActivate)
            {
                item.SetActive(true);
            }
        }
    }
}
