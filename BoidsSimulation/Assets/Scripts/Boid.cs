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
 *          
 *      06/08/2020
 *          Added collisions on Friday.  It is a capsulecollider2d so that it
 *          is distinguished from the circle collider.  It seems to be working.
 *          
 *          Added layers so that I could make the boids not collide with 
 *          each other or collide with each other.  Does not seem to quite
 *          work.  It seems that if you turn off the collider the trigger also
 *          turns off.  Searching out the answer for that.
 *          
 *          One interesting thing is that collisions now seem to be intricately
 *          woven in with flocking.  Turn off the collisions and flocking is
 *          essentially off.  Odd.
 *          
 *          Was going to finish off the last thing which is just a GUI.  The
 *          GUI was just going to have a few toggle boxes and maybe some
 *          sliders for adjusting the flocking parameters.  Did not get to the
 *          flocking parameters.  And the only toggle that seems to work is
 *          the wrap.  The boid collisions and flocking both toggle, but do not 
 *          quite work like their Unity counterparts in the properties of 
 *          _GameManager.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boid : MonoBehaviour
{
    public Vector2 position;
    public Vector2 velocity;
    public float speed { get { return velocity.magnitude; } }
    // setter of vel not private because it is used by Boundaries.cs
    public Vector2 vel {  get { return velocity; } set { velocity = value; } }

    Rigidbody2D rigidbody2d;
    CircleCollider2D circlecollider2d;
    // Does instantiating it here really make a difference (not in Start())
    List<GameObject> influencers = new List<GameObject>();
    bool boidCollisions;
    bool flocking;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        position = new Vector2(3.0f, 3.0f);
        float x= 0.0f, y = 0.0f;
        while (x == 0.0f && y == 0.0f)
        {
            x = Random.Range(-3.0f, 3.0f);  // thought to randomize speed
            y = Random.Range(-3.0f, 3.0f);  // normalizing (below) just randomizes direction
        }
        velocity = new Vector2(x, y);
        velocity.Normalize();
        velocity *= Random.Range(0.0f, 5.0f);   // give the speed a randomness too
        circlecollider2d = GetComponent<CircleCollider2D>();
        circlecollider2d.radius = GameManager.instance.influenceRadius;
        boidCollisions = GameManager.instance.boidCollisions;
        flocking = GameManager.instance.flocking;
        this.gameObject.layer = LayerMask.NameToLayer("BoidCollisions");
        
    }

    // Update is called once per frame
    void Update()
    {
        position = rigidbody2d.position;
        circlecollider2d.radius = GameManager.instance.influenceRadius;
        boidCollisions = GameManager.instance.boidCollisions;
        flocking = GameManager.instance.flocking;
        // Calculate position and velocity first without any influence of the
        // flock.  Then, modify the velocity to take into account the flock
        // influences.  Check to see that flocking is enabled.
        Vector2 positionDifference =  new Vector2(0.0f, 0.0f);     // cohesion
        float separationFactor = GameManager.instance.separationFactor;
        float alignmentFactor = GameManager.instance.alignmentFactor;
        float cohesionFactor = GameManager.instance.cohesionFactor;
        Vector2 velocityChange = new Vector2(0.0f, 0.0f);
        float currentSpeed = speed;
        Vector2 direction = GetDirection();
        Vector2 directionWeighted = vel;
        if (boidCollisions)
        {
            this.gameObject.layer = LayerMask.NameToLayer("BoidCollisions");
        }
        else
        {
            this.gameObject.layer = LayerMask.NameToLayer("NoBoidCollisions");
        }
        if (flocking)
        {
            Vector2 averagePosition = new Vector2(0.0f, 0.0f);
            if (influencers.Count != 0)
            {
                for (int i = 0; i < influencers.Count; i++)
                {
                    if (GameManager.instance.cohesion)
                    {
                        Rigidbody2D other = influencers[i].GetComponent<Rigidbody2D>();
                        averagePosition.x += other.transform.position.x;
                        averagePosition.y += other.transform.position.y;
                    }
                    if (GameManager.instance.alignment)
                    {
                        directionWeighted += influencers[i].GetComponent<Boid>().vel;   // weights speed
                    }

                }
                if (GameManager.instance.alignment)
                {
                    directionWeighted /= (influencers.Count + 1);
                    directionWeighted.Normalize();
                    velocity = directionWeighted * currentSpeed;
                }
            }
            if (averagePosition != new Vector2(0.0f, 0.0f)  && GameManager.instance.cohesion)
            {
                positionDifference =  averagePosition- position;    // direction of speed change to get cohesion

                positionDifference.Normalize();
                Vector2 positionDifferenceInfluence = positionDifference * cohesionFactor;
                velocityChange = velocity + positionDifferenceInfluence;
            }
            velocity += velocityChange;
            // for debugging to see why the velocity changes so greatly
            velocity.Normalize();
            velocity *= currentSpeed;   // just to see if the velocity is changing direction
        }

        // Angle boid pointing and location of boid need to be calculated after
        // new velocity is calculated, whether flocking or not.

        float degrees;
        // tan of 90 and 270 degrees not defined; avoid div by 0
        if (velocity.x != 0)
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
        rigidbody2d.MovePosition(position);
        rigidbody2d.MoveRotation(degrees);
    }

    public Vector2 GetDirection()
    {
        Vector2 direction = velocity;
        direction.Normalize();
        return direction;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null && !influencers.Contains(collision.gameObject)) { influencers.Add(collision.gameObject); }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision != null && influencers.Contains(collision.gameObject)) { influencers.Remove(collision.gameObject); }
    }
}
