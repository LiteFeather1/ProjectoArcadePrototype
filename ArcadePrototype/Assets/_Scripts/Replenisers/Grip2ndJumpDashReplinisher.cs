using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grip2ndJumpDashReplinisher : MonoBehaviour
{
    [SerializeField] private bool _restoreStamina = true;
    [SerializeField] private bool _restoreSecondJump = true;
    [SerializeField] private bool _restoreDash = true;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (_restoreStamina) collision.GetComponent<WallStamina>().ReplenishStamina();
            if (_restoreSecondJump) collision.GetComponent<Jump>().ReplenishSecondaryJump();
            if (_restoreDash) collision.GetComponent<Dash>().ReplenishDash();
            Destroy(gameObject);
        }
    }
}
