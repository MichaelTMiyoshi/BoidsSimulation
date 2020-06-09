using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfluenceSliderControl : MonoBehaviour
{
    private float influenceRadius;
    public Slider influenceSlider;

    void Start()
    {
        influenceRadius = GameManager.instance.influenceRadius;
        influenceSlider.onValueChanged.AddListener(delegate { ValueChangedCheck(); });
        influenceSlider.minValue = 0.5f;
        influenceSlider.maxValue = 10.0f;
    }

    void ValueChangedCheck()
    {
        GameManager.instance.influenceRadius = influenceRadius;
    }
}
