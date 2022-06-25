using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleAllCanvas : ComplexSingleton<ToggleAllCanvas>
{
    private bool _switch = true;
    // Update is called once per frame
    void Update()
    {
       if(Input.GetKeyDown(KeyCode.C))
        {
            _switch = !_switch;
            Canvas[] _canvas = FindObjectsOfType<Canvas>(true);
            foreach (var item in _canvas)
            {
                item.gameObject.SetActive(_switch);
            }
        }
    }
}
