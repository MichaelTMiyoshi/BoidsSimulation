using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 *      from: https://www.youtube.com/watch?v=ailbszpt_AI
 */
public class Boundaries : MonoBehaviour
{
    private Vector2 screenBounds;
    private float objectWidth;
    private float objectHeight;

    // Start is called before the first frame update
    void Start()
    {
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        //screenBounds = Camera.main.WorldToScreenPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        objectWidth = transform.GetComponent<SpriteRenderer>().bounds.size.x / 2;
        objectHeight = transform.GetComponent<SpriteRenderer>().bounds.size.y / 2;
        Debug.Log("ScreenBounds: (" + Screen.width + ", " + Screen.height + ")");
        Debug.Log("Width, Height: (" + objectWidth + ", " + objectHeight + ")");
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 viewPos = transform.position;
        //Vector3 viewPos = GetComponent<Rigidbody2D>().position;
        viewPos.x = Mathf.Clamp(viewPos.x, screenBounds.x * (-1) + objectWidth, screenBounds.x - objectWidth);
        viewPos.y = Mathf.Clamp(viewPos.y, screenBounds.y * (-1) + objectHeight, screenBounds.y - objectHeight);
        transform.position = viewPos;
        Debug.Log("viewPos.x, y : (" + viewPos.x + ", " + viewPos.y + ")");
        Debug.Log("Bounds: " + screenBounds.x + ", " + screenBounds.y + ")");
    }
}