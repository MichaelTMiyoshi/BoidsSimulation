using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoidCollisionsToggle : MonoBehaviour
{
    private bool boidCollisions;
    Toggle boidCollisionsToggle;
    // Start is called before the first frame update
    //void OnGUI()
    //{
    //wrap = GUI.Toggle(new Rect(25, 25, 100, 30), wrap, "WrapToggle");   // 160 x 20; -370, 220
    //}

    void Start()
    {
        boidCollisions = GameManager.instance.boidCollisions;
        boidCollisionsToggle = GetComponent<Toggle>();    // listener
        boidCollisionsToggle.onValueChanged.AddListener(delegate { ToggleValueChanged(boidCollisionsToggle); });
    }

    // Update is called once per frame
    void ToggleValueChanged(Toggle change)
    {
        boidCollisions = !boidCollisions;
        GameManager.instance.boidCollisions = boidCollisions;
    }
}
