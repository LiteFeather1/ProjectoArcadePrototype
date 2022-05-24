using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OnMouseHover : MonoBehaviour
{
    [SerializeField] private Image _mySelf;
    [SerializeField] private Sprite _hoverSprite, _defaultSprite;

    public void OnHover()
    {
        _mySelf.sprite = _hoverSprite;
    }

    public void OnExit()
    {
        _mySelf.sprite = _defaultSprite;
    }
}
