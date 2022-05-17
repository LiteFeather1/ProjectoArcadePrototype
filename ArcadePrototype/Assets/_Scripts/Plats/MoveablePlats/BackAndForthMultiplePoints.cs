using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackAndForthMultiplePoints : MoveablePlats
{
    [SerializeField] private Transform[] _whereTos;

    private void OnEnable()
    {
        StartCoroutine(Movement());
    }
    protected override void Awake()
    {

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, _whereTos[0].position);
        for (int i = 1; i <= _whereTos.Length - 1; i++)
        {
            Gizmos.DrawLine(_whereTos[i - 1].position, _whereTos[i].position);
        }
    }

    IEnumerator Movement()
    {
        foreach (var item in _whereTos)
        {
            while (transform.position != item.position)
            {
                transform.position = Vector2.MoveTowards(transform.position, item.position, _speed * Time.deltaTime);
                yield return null;
            }
        }
        StartCoroutine(Movement());
    }
}
