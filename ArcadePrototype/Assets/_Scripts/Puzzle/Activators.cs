using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Activators : MonoBehaviour
{
    [SerializeField] protected GameObject[] _whatToInteract;
    private bool _state = true;
    
    protected virtual void Interact()
    {
        _state = !_state;
        foreach (var interactable in _whatToInteract)
        {
            interactable.GetComponent<IButtonable>().ToInterract(_state);
        }
    }

}
