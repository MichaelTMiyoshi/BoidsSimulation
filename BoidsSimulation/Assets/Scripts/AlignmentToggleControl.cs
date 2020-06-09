using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AlignmentToggleControl : MonoBehaviour
{
    private bool alignment;
    public Toggle alignmentToggle;

    void Start()
    {
        alignment = GameManager.instance.alignment;
        alignmentToggle = GetComponent<Toggle>();    // listener
        alignmentToggle.onValueChanged.AddListener(delegate { ToggleValueChanged(alignmentToggle); });

    }

    void ToggleValueChanged(Toggle change)
    {
        alignment = !alignment;
        GameManager.instance.alignment = alignment;
    }
}
