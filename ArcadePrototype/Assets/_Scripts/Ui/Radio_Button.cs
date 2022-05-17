using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Radio_Button : MonoBehaviour
{
    [SerializeField] private Button _myself;
    [SerializeField] private Button[] _buttons;
    [SerializeField] private Sprite _pressedSprite, _notPressedSprite; 

    private bool _selected;

    public void OnDown()
    {
        foreach (var item in _buttons)
        {
            item.image.sprite = _notPressedSprite;
        }

        _myself.image.sprite = _pressedSprite;

        //if(_selected)
        //{ 
        //    _myself.image.sprite = _pressedSprite;
        //}
        //else
        //{
        //    _myself.image.sprite = _notPressedSprite;
        //}
    }
}
