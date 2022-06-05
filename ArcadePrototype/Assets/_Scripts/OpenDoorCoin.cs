using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoorCoin : MonoBehaviour, ICollectable
{
    [SerializeField] private DoorOpenWithCoin _doorToOpen;
    [SerializeField] private Sprite _default;
    [SerializeField] private Sprite _collectedSprite;

    private SpriteRenderer _sr;
    private Collider2D _collider;
    private ParticleSystem _collectedParticles;

    [SerializeField] private PlayerHitBox _player;

    private void OnEnable()
    {
        _player.Death.AddListener(Reset);
    }

    private void Awake()
    {
        _sr = GetComponent<SpriteRenderer>();
        _collectedParticles = GetComponent<ParticleSystem>();
        _collider = GetComponent<Collider2D>();
    }

    public void ToCollect()
    {
        _collider.enabled = false;
        _doorToOpen.CoinCollected();
        _sr.sprite = _collectedSprite;
        _collectedParticles.Play();
    }

    private void Reset()
    {
        _sr.sprite = _default;
        _collider.enabled = true;
    }
}
