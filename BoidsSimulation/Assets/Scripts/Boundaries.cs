using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 *      from: https://www.youtube.com/watch?v=ailbszpt_AI
 *      
 *      06/03/2020
 *          Had a problem inmplementing the boundary yesterday.  The problem
 *          was with the Clamp method.  It works like it says, but the tutorial
 *          either has a probem or something changed in Unity or I messed up.
 *          I had the min and max swapped.  I did hear in the video that the 
 *          screenBounds are opposite of something so I did not see anything
 *          wrong with the -1 multiplier when it was on the max.  But the 
 *          Boid kept disappearing right at the start.  So I commented out 
 *          everything and added stuff back in.  I output to the console and
 *          did all the stuff that I normally do.  Turned out to be Clamp.  The
 *          min and max values were in opposite places.  So I swapped them and
 *          things are hunky dory.
 *          
 *      06/04/2020
 *          On to screen wrapping.
 *          Plan:  
 *              public bool variable that says whether wrapping is on or not.
 *              When the bool is true, the boid will go to the other side of
 *              the screen.  When the bool is false, the boid will just sit
 *              on the edge.  Or it will bounce.  Leaning toward bouncing.
 *                 
 *          Results:
 *              Got both wrap and !wrap (bounce) working.  Mostly.
 *              The biggest headaches came with clamping.  I think that the 
 *              bouncing and wrapping were both working, but that when doing
 *              the comparisons to the edge, moving to the other edge or
 *              changing the velocity did not quite do the trick.  In wrapping,
 *              I needed to change the equality comparison so that the boid
 *              did not just wrap from one side to the other right away.  I 
 *              could not see this happening, but I am pretty sure that was it.
 *              In bouncing, I needed to access the velocity so I made a setter
 *              and getter in the boid code.  This almost worked.  I also 
 *              needed to make the boid move with the new velocity.  So I added
 *              that into the boundary code as well.
 *              
 *          Still to do:
 *              I still need to put in some fudge factor because there are a
 *              couple cases where the boid gets stuck on the edge in wrapping.
 *              Maybe even just a plus one somewhere.
 *          
 *          Done:
 *              Added fudgeFactor.  Used it to multiply the speed when updating
 *              the position.  Warp speed at the boundary.  Looks fine for 
 *              wrapping.  Made it public so I can test it.  Not noticable for
 *              bouncing or wrapping.  Still might need to up it for wrap.
 *              Gets caught on the edge every once in a while.  (Top is most
 *              noticable.)
 */
public class Boundaries : MonoBehaviour
{
    private Vector2 screenBounds;
    private float objectWidth;
    private float objectHeight;
    public bool wrap;
    public float fudgeFactor = 2.0f;

    // Start is called before the first frame update
    void Start()
    {
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        //screenBounds = Camera.main.WorldToScreenPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        objectWidth = transform.GetComponent<SpriteRenderer>().bounds.size.x / 2;
        objectHeight = transform.GetComponent<SpriteRenderer>().bounds.size.y / 2;
        //Debug.Log("ScreenBounds: (" + Screen.width + ", " + Screen.height + ")");
        //Debug.Log("Width, Height: (" + objectWidth + ", " + objectHeight + ")");
        wrap = true;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 viewPos = transform.position;
        //Vector3 viewPos = GetComponent<Rigidbody2D>().position;
        //viewPos.x = Mathf.Clamp(viewPos.x, screenBounds.x * (-1) + objectWidth, screenBounds.x - objectWidth);
        //viewPos.y = Mathf.Clamp(viewPos.y, screenBounds.y * (-1) + objectHeight, screenBounds.y - objectHeight);
        float xMax = screenBounds.x;
        float yMax = screenBounds.y;
        //viewPos.x = Mathf.Clamp(viewPos.x, xMax * (-1), xMax);
        //viewPos.y = Mathf.Clamp(viewPos.y, yMax * (-1), yMax);
        Boid boid = GetComponent<Boid>();
        Vector2 vel = boid.vel;
        if (viewPos.x <= -xMax || xMax <= viewPos.x)
        {
            if(wrap) { viewPos.x = -1 * viewPos.x; }
            else
            {
                //boid.velocity.x = -1 * boid.velocity.x;
                vel.x = -1 * vel.x;
                boid.vel = vel;
            }
        }
        if (viewPos.y <= -yMax || yMax <= viewPos.y)
        {
            if (wrap) { viewPos.y = -1 * viewPos.y; }
            else
            {
                //boid.velocity.y = -1 * boid.velocity.y;
                vel.y = -1 * vel.y;
                boid.vel = vel;
            }
        }
        viewPos.x += fudgeFactor * vel.x * Time.deltaTime;
        viewPos.y += fudgeFactor * vel.y * Time.deltaTime;
        transform.position = viewPos;
        //Debug.Log("viewPos.x, y : (" + viewPos.x + ", " + viewPos.y + ")");
        //Debug.Log("Bounds: " + screenBounds.x + ", " + screenBounds.y + ")");
        //Debug.Log("BoidVel: " + boid.velocity.x + ", " + boid.velocity.y + ")");
    }
}