using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WrapToggleControl : MonoBehaviour
{
    private bool wrap;
    Toggle WrapToggle;
    // Start is called before the first frame update
    //void OnGUI()
    //{
        //wrap = GUI.Toggle(new Rect(25, 25, 100, 30), wrap, "WrapToggle");   // 160 x 20; -370, 220
    //}

    void Start()
    {
        wrap = GameManager.instance.wrap;
        WrapToggle = GetComponent<Toggle>();    // listener
        WrapToggle.onValueChanged.AddListener(delegate { ToggleValueChanged(WrapToggle); });
    }

    // Update is called once per frame
    void ToggleValueChanged(Toggle change)
    {
        wrap = !wrap;
        GameManager.instance.wrap = wrap;
    }
}
