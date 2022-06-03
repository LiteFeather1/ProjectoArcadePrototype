using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonOpenDoor : MonoBehaviour
{
    [SerializeField] DoorOpenWithCoin _myDoorToOpen;
    private bool _canMove = true;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" && collision.transform.position.y > transform.position.y + 0.75f)
        {
            if (_canMove)
            {
                _canMove = false;
                transform.position = new Vector3(transform.position.x, transform.position.y - .35f);
                _myDoorToOpen.CoinCollected();
            }
        }
    }
}