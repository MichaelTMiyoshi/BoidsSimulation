using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlockingToggleControl : MonoBehaviour
{
    private bool flocking;
    Toggle FlockingToggle;
    Toggle alignmentToggle;
    Toggle cohesionToggle;
    Slider influenceSlider;

    void Start()
    {
        flocking = GameManager.instance.flocking;
        FlockingToggle = GetComponent<Toggle>();    // listener
        FlockingToggle.onValueChanged.AddListener(delegate { ToggleValueChanged(FlockingToggle); });
        alignmentToggle = this.gameObject.transform.parent.gameObject.transform.GetChild(3).GetComponent<Toggle>();
        cohesionToggle = this.gameObject.transform.parent.gameObject.transform.GetChild(4).GetComponent<Toggle>();
        influenceSlider = this.gameObject.transform.parent.gameObject.transform.GetChild(5).GetComponent<Slider>();
    }

    // Update is called once per frame
    void ToggleValueChanged(Toggle change)
    {
        flocking = !flocking;
        GameManager.instance.flocking = flocking;
        if (!flocking)
        {
            alignmentToggle.interactable = false;
            cohesionToggle.interactable = false;
            influenceSlider.interactable = false;
        }
        else
        {
            alignmentToggle.interactable = true;
            cohesionToggle.interactable = true;
            influenceSlider.interactable = true;
        }
    }
}
