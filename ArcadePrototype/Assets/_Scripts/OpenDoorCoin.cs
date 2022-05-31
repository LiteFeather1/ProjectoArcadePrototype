using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoorCoin : MonoBehaviour, ICollectable
{
    [SerializeField] private DoorOpenWithCoin _doorToOpen;
    [SerializeField] private Sprite _collectedSprite;

    private SpriteRenderer _sr;
    private ParticleSystem _collectedParticles;

    private void Awake()
    {
        _sr = GetComponent<SpriteRenderer>();
        _collectedParticles = GetComponent<ParticleSystem>();
    }

    public void ToCollect()
    {
        GetComponent<Collider2D>().enabled = false;
        _doorToOpen.CoinCollected();
        _sr.sprite = _collectedSprite;
        _collectedParticles.Play();
    }
}
