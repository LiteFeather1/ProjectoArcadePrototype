using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSprite : MonoBehaviour
{
    [SerializeField] private Sprite[] _sprite;

    private void Start()
    {
        RandomizeSprite();
    }

    public void RandomizeSprite()
    {
        int randomSprte = Random.Range(0, _sprite.Length - 1);
        GetComponent<SpriteRenderer>().sprite = _sprite[randomSprte];
    }
}
