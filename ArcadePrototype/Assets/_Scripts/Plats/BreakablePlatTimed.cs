using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakablePlatTimed : MonoBehaviour
{
    [SerializeField] protected float _timeToGetDestroyed;
    [SerializeField] protected float _timeToGetBack;
    private Collider2D _collider;
    private SpriteRenderer _sr;
    [SerializeField] private Sprite _temp;

    protected virtual void Awake()
    {
        _collider = GetComponent<Collider2D>();
        _sr = GetComponent<SpriteRenderer>();
        _temp = _sr.sprite;
    }

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            StartCoroutine(Co_Logic());
        }
    }

    protected virtual IEnumerator Co_Logic()
    {
        yield return new WaitForSeconds(_timeToGetDestroyed);
        _collider.enabled = false;
        _sr.sprite = null;
        yield return new WaitForSeconds(_timeToGetBack);
        _collider.enabled = true;
        _sr.sprite = _temp;
    }
}
