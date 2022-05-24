using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OnMouseOverAnimation : MonoBehaviour
{
    [SerializeField] RectTransform _rt;
    [SerializeField] private int _amountToMove;
    [SerializeField] private float _timeBetween;
    [Range(-1, 1)] [SerializeField] private int _leftOrRight;
    private bool _stayed;

    public void OnEnter()
    {
        _stayed = true;
        StartCoroutine(MyAnimation());
    }

    public void OnExit()
    {
        _stayed = false;
    }

    IEnumerator MyAnimation()
    {
        if (_stayed)
        {
            for (int i = 0; i < _amountToMove; i++)
            {
                _rt.anchoredPosition = new Vector2(_rt.anchoredPosition.x + i * _leftOrRight, _rt.anchoredPosition.y);
                yield return new WaitForSeconds(_timeBetween);
            }
            for (int i = 0; i < _amountToMove; i++)
            {
                _rt.anchoredPosition = new Vector2(_rt.anchoredPosition.x - i * _leftOrRight, _rt.anchoredPosition.y);
                yield return new WaitForSeconds(_timeBetween);
            }
            StartCoroutine(MyAnimation());
        }     
    }
}
