using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoidCollisionsToggle : MonoBehaviour
{
    private bool boidCollisions;
    Toggle boidCollisionsToggle;

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
