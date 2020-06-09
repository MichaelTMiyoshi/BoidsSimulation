using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AlignmentToggleControl : MonoBehaviour
{
    private bool alignment;
    public Toggle alignmentToggle;
//    Slider influenceSlider;

    void Start()
    {
        alignment = GameManager.instance.alignment;
        alignmentToggle = GetComponent<Toggle>();    // listener
        alignmentToggle.onValueChanged.AddListener(delegate { ToggleValueChanged(alignmentToggle); });
//        influenceSlider = this.gameObject.transform.parent.gameObject.transform.GetChild(5).GetComponent<Slider>();

    }

    // Update is called once per frame
    void ToggleValueChanged(Toggle change)
    {
        alignment = !alignment;
        GameManager.instance.alignment = alignment;
/*        if (alignmentToggle.interactable && GameManager.instance.flocking)
        {
            influenceSlider.interactable = true;
        }
        else
        {
            influenceSlider.interactable = false;
        }
*/
    }
}
