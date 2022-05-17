using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SliderScript : MonoBehaviour
{
    [SerializeField] private Slider _slider ;
    [SerializeField] private TextMeshProUGUI _sliderNum;

    void Start()
    {
        _slider.onValueChanged.AddListener((v) => { _sliderNum.text = v.ToString("0"); });
    }
}
