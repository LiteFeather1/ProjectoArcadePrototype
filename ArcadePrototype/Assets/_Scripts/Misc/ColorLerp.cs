using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorLerp : MonoBehaviour
{
    [SerializeField] private Color32 _color1;
    private Color32 _from;
    [SerializeField] private Color32 _color2;
    private Color32 _to;
    private Color32 _colorTolerp;

    private float _t;
    [SerializeField] private float _timeToPass;
    private float _timePassed;

    [SerializeField] private bool _looping = true;
    private bool _switch;

    private SpriteRenderer _sR;

    private void Start()
    {
        _sR = GetComponent<SpriteRenderer>();
        _colorTolerp = _color1;
        _from = _color1;
        _to = _color2;
    }

    void Update()
    {
        if (_timePassed < _timeToPass)
        {
            _timePassed += Time.deltaTime;
            _t = _timePassed / _timeToPass;
            _colorTolerp = Color32.Lerp(_from, _to, _t);
        }
        else if (_timePassed > _timeToPass)
        {
            _timePassed = _timeToPass;
            if(_looping)
            {
                _timePassed = 0;
                SwitchColor();
            }
        }

        _sR.color = _colorTolerp; 
    } 

    private void SwitchColor()
    {
        _switch = !_switch;

        if(_switch)
        {
            _from = _color2;
            _to = _color1;
        }
        else
        {
            _from = _color1;
            _to = _color2;
        }
    }
}
