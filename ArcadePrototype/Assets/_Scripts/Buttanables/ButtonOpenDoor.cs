using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonOpenDoor : MonoBehaviour
{
    [SerializeField] OpenDoorCoin _myDoorToOpen;
    private bool _canMove;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (_canMove)
            {
                _canMove = false;
                transform.position = new Vector3(transform.position.x, transform.position.y - .99f);
                _myDoorToOpen.ToCollect();
            }
        }
    }
}