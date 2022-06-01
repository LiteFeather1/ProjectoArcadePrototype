using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buttons : Activators
{
    [SerializeField] protected bool _onceButton;
    protected bool _canMove = true;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (_canMove)
            {
                _canMove = false;
                transform.position = new Vector3(transform.position.x, transform.position.y - .99f);
                Interact();
            }
            else
            {
                if (!_onceButton)
                {
                    _canMove = true;
                    transform.position = new Vector3(transform.position.x, transform.position.y + .99f);
                    Interact();
                }
            }
        }
    }
}
