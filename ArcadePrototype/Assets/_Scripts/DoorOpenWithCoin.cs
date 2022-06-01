using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpenWithCoin : MonoBehaviour
{
    [SerializeField] private int _coinsToCollect;
    [SerializeField] private float _movimentSpeed;
    [SerializeField] private Vector3 _whereToMove;
    private Vector3 _realWhereTo;

    [SerializeField] private SpriteRenderer _lockSprite;

    private void Start() => _realWhereTo = transform.position + _whereToMove;
    public void CoinCollected()
    {
        _coinsToCollect--;
        if (_coinsToCollect == 0) OpenDoor();
    }

    private void OpenDoor()
    {
        StartCoroutine(DoorMoviment());
        _lockSprite.enabled = false;
    }

    IEnumerator DoorMoviment()
    {
        while(transform.position != _realWhereTo)
        {
            transform.position = Vector2.MoveTowards(transform.position, _realWhereTo, _movimentSpeed * Time.deltaTime);
            yield return new WaitForFixedUpdate();
        }
    }
}
