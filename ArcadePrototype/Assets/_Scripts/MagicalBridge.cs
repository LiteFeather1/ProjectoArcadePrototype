using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicalBridge : MonoBehaviour
{

    [SerializeField] private GameObject _myStand;

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            float posX = collision.contacts[0].point.x;
            float posY = gameObject.transform.position.y;
            _myStand.SetActive(true);
            _myStand.transform.position = new Vector2(posX, posY);
        }
    }
}
