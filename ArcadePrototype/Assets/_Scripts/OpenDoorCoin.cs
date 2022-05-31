using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoorCoin : MonoBehaviour, ICollectable
{
    [SerializeField] private DoorOpenWithCoin _doorToOpen;

    public void ToCollect()
    {
        GetComponent<Collider2D>().enabled = false;
        _doorToOpen.CoinCollected();
        GetComponent<SpriteRenderer>().color = new Color32(240, 121, 255, 255);
    }
}
