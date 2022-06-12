using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardOfInformation : MonoBehaviour, IIteractable
{
    [SerializeField] private GameObject _myInfo;
    private bool _active;

    public void ToInteract()
    {
        _active = !_active;

        _myInfo.SetActive(_active);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            if (_active)
                ToInteract();
        }
    }
}
