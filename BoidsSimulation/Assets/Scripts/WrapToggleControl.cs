using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WrapToggleControl : MonoBehaviour
{
    private bool wrap;
    Toggle WrapToggle;

    void Start()
    {
        wrap = GameManager.instance.wrap;
        WrapToggle = GetComponent<Toggle>();    // listener
        WrapToggle.onValueChanged.AddListener(delegate { ToggleValueChanged(WrapToggle); });
    }

    void ToggleValueChanged(Toggle change)
    {
        wrap = !wrap;
        GameManager.instance.wrap = wrap;
    }
}
