/****************************************************************************
 * Boids:
 *      https://en.wikipedia.org/wiki/Boids
 *      
 *      Boids is an artificial life program, developed by Craig Reynolds 
 *      in 1986, which simulates the flocking behaviour of birds. His paper 
 *      on this topic was published in 1987 in the proceedings of the ACM 
 *      SIGGRAPH conference. [1] The name "boid" corresponds to a shortened 
 *      version of "bird-oid object", which refers to a bird-like object.[2] 
 *      Incidentally, "boid" is also a New York Metropolitan dialect 
 *      pronunciation for "bird".
 *      
 *      Rules applied in simple Boids
 *          Separation
 *          Alignment
 *          Cohesion
 *      
 *      As with most artificial life simulations, Boids is an example of 
 *      emergent behavior; that is, the complexity of Boids arises from the i
 *      nteraction of individual agents (the boids, in this case) adhering to 
 *      a set of simple rules. The rules applied in the simplest Boids world 
 *      are as follows:
 *      
 *          separation: steer to avoid crowding local flockmates
 *          alignment: steer towards the average heading of local flockmates
 *          cohesion: steer to move towards the average position 
 *              (center of mass) of local flockmates
 *          
 *      More complex rules can be added, such as obstacle avoidance and 
 *      goal seeking.
 *      
 **************************************************************************** 
 *
 *      To do:
 *          Add Area of influence.  This will be a collision circle outside the
 *          boid where any other boid inside this box will be used to calculate
 *          influence, separation, and cohesion.  Might need separate colliders
 *          for each of them or might just need one.
 *          
 *          For separation:
 *              Determine a minimum distance a boid needs to be from its
 *              neighbor and adjust speed accordingly.  (Speed up or slow down
 *              or change direction to be average distance between others in
 *              the cirlce of influence.
 *          
 *          For Alignment:
 *              Determine the average direction of the boids in the area of
 *              influence.
 *          
 *          For Cohesion:
 *              Determine the average position of boids in the area of 
 *              influence.
 *
 *          In the end, each of the three will produce some change to the 
 *          direction and speed of the boid.
 */

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

    List<GameObject> influencers;

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
        influencers = new List<GameObject>();
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null) { influencers.Add(collision.gameObject); }
        if (influencers.Count != 0) { Debug.Log("influencer size (added): " + influencers.Count); }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision != null) { influencers.Remove(collision.gameObject); }
        if (influencers.Count != 0) { Debug.Log("influencer size (remove): " + influencers.Count); }
    }
}
