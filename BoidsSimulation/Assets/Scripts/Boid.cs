using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boid : MonoBehaviour
{
    public Vector2 position;
    public Vector2 velocity;
    public float speed { get { return velocity.magnitude; } }
    public Vector2 vel {  get { return velocity; } set { velocity = value; } }

    Rigidbody2D rigidbody2d;

    // for boundaries (could not make it work with two scripts
    //private Vector2 screenBounds;
    //private float objectWidth;
    //private float objectHeight;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        position = new Vector2(3.0f, 3.0f);
        //velocity = new Vector2(3.0f, 0.0f);
        //velocity = new Vector2(3.0f / Mathf.Sqrt(2.0f), -(3.0f / Mathf.Sqrt(2.0f)));
        float x= 0.0f, y = 0.0f;
        while (x == 0.0f && y == 0.0f)
        {
            x = Random.Range(-3.0f, 3.0f);  // thought to randomize speed
            y = Random.Range(-3.0f, 3.0f);  // normalizing (below) just randomizes direction
        }
        velocity = new Vector2(x, y);
        velocity.Normalize();
        //velocity *= 3.0f;
        velocity *= Random.Range(0.0f, 5.0f);   // give the speed a randomness too

        // for boundaries (could not make it work with two scripts
        //screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        //objectWidth = transform.GetComponent<SpriteRenderer>().bounds.size.x / 2;
        //objectHeight = transform.GetComponent<SpriteRenderer>().bounds.size.y / 2;
        //Debug.Log("Width, Height: (" + objectWidth + ", " + objectHeight + ")");
    }

    // Update is called once per frame
    void Update()
    {
        position = rigidbody2d.position;
        float degrees;
        if(velocity.x != 0)
        {
            float radians = Mathf.Atan2(velocity.y, velocity.x);
            degrees = Mathf.Rad2Deg * radians;
            // Absolute value needed because a negative position multiplied by
            // a negative Cos or Sin result will make the position off.
            // The rotation will be correct, but the movement will be wrong.
            // Could take the absolute value of either velocity or sin/cos.
            // But not both.
            position.x += velocity.x * Mathf.Abs(Mathf.Cos(radians)) * Time.deltaTime;
            position.y += velocity.y * Mathf.Abs(Mathf.Sin(radians)) * Time.deltaTime;
        }
        else
        {
            position.y += velocity.y * Time.deltaTime;
            if(velocity.y < 0)
            {
                degrees = 270.0f;
            }
            else
            {
                degrees = 90.0f;
            }
        }
        //Debug.Log("Position: (" + position.x + ", " + position.y + ")");  // forgot to add Rigidbody2D to PreFab
        rigidbody2d.MovePosition(position);
        rigidbody2d.MoveRotation(degrees);
    }

/*    public void ChangeVelocity(Vector2 vel)
    {
        velocity = vel;
    }*/
}
