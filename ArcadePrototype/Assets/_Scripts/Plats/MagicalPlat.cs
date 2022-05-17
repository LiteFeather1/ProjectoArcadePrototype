using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicalPlat : MonoBehaviour
{
    [SerializeField] private GameObject _myStand;
    private Collider2D _myCollider;

    private void Awake()
    {
        _myCollider = GetComponent<Collider2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            float posX = collision.contacts[0].point.x;
            float posY = gameObject.transform.position.y;
            _myStand.SetActive(true);
            _myStand.transform.position = new Vector2(posX, posY);
            _myCollider.isTrigger = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (collision.gameObject.GetComponent<Rigidbody2D>().velocity.y < 0)
            {
                _myCollider.isTrigger = true;
            }
            else
            {
                _myStand.SetActive(false);
                _myCollider.isTrigger = false;
            }
        }
    }
}
