using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SliderValueToText : MonoBehaviour
{
    [SerializeField] private TMP_Text _valueText;
    [SerializeField] private TMP_Text _shadowText;
    [SerializeField] private Slider _slider;

    private void Start()
    {
        ConvertSliderValueToText();
    }

    public void ConvertSliderValueToText()
    {
        _valueText.text = Mathf.RoundToInt(_slider.value * 100f) + "%";
        _shadowText.text = _valueText.text;
    }
}
