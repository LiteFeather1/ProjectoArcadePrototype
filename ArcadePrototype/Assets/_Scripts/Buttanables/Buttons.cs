using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buttons : Activators
{
    [SerializeField] protected bool _onceButton;
    protected bool _canMove = true;
    private bool _cooldown;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" && collision.transform.position.y > transform.position.y + 0.75f)
        {
            if (!_cooldown)
            {
                if (_canMove)
                {
                    _canMove = false;
                    transform.position = new Vector3(transform.position.x, transform.position.y - .35f);
                    Interact();
                }
                else
                {
                    if (!_onceButton)
                    {
                        _canMove = true;
                        transform.position = new Vector3(transform.position.x, transform.position.y + .35f);
                        Interact();
                    }
                }
                StartCoroutine(Cooldown());
            }
        }
    }

    IEnumerator Cooldown()
    {
        _cooldown = true;
        yield return new WaitForSeconds(.25f);
        _cooldown = false;
    }
}
