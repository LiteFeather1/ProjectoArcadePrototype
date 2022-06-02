using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakablePlatTimed : MonoBehaviour
{
    [SerializeField] protected float _timeToGetDestroyed;
    [SerializeField] protected float _timeToGetBack;
    [Header("Sprites")]
    [SerializeField] private Sprite _defaultSprite;
    [SerializeField] private Sprite _nullSprite;
    [SerializeField] private CustomAnimator _customAnimator;

    private Collider2D _collider;
    private SpriteRenderer _sr;

    protected virtual void Awake()
    {
        _collider = GetComponent<Collider2D>();
        _sr = GetComponent<SpriteRenderer>();
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
        _customAnimator.PlayAnimation(transform);
        yield return new WaitForSeconds(_timeToGetDestroyed);
        _customAnimator.StopAnimation();
        _collider.enabled = false;
        _sr.sprite = _nullSprite;
        yield return new WaitForSeconds(_timeToGetBack);
        _collider.enabled = true;
        _sr.sprite = _defaultSprite;
    }
}
