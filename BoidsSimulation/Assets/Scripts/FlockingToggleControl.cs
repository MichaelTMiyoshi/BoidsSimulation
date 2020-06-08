using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlockingToggleControl : MonoBehaviour
{
    private bool flocking;
    Toggle FlockingToggle;
    // Start is called before the first frame update
    //void OnGUI()
    //{
    //wrap = GUI.Toggle(new Rect(25, 25, 100, 30), wrap, "WrapToggle");   // 160 x 20; -370, 220
    //}

    void Start()
    {
        flocking = GameManager.instance.flocking;
        FlockingToggle = GetComponent<Toggle>();    // listener
        FlockingToggle.onValueChanged.AddListener(delegate { ToggleValueChanged(FlockingToggle); });
    }

    // Update is called once per frame
    void ToggleValueChanged(Toggle change)
    {
        flocking = !flocking;
        GameManager.instance.flocking = flocking;
    }
}
