using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CohesionToggleControl : MonoBehaviour
{
    private bool cohesion;
    public Toggle cohesionToggle;

    void Start()
    {
        cohesion = GameManager.instance.cohesion;
        cohesionToggle = GetComponent<Toggle>();    // listener
        cohesionToggle.onValueChanged.AddListener(delegate { ToggleValueChanged(cohesionToggle); });
    }

    // Update is called once per frame
    void ToggleValueChanged(Toggle change)
    {
        cohesion = !cohesion;
        GameManager.instance.cohesion = cohesion;
    }
}
