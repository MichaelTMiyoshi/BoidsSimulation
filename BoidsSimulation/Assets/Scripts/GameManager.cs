/*
 *      Created Singleton.
 *      
 *      one reference on Singletons (where I got the original code in Awake): 
 *      https://www.studica.com/blog/how-to-create-a-singleton-in-unity-3d
 *      
 *      The site also tells these two steps:
 *      1. Create empty game object in the scene
 *      2. Create a script (this one).  I call it GameMangager.
 *      
 *      This singleton contains all the global variablees necessary for the
 *      game.
 * 
 *      I use the singleton to instantiate my boids.  My boids are prefabs
 *      that go where I put them.  An interesting thing is that when you 
 *      instantiate an object, it goes where the Instantiate call tells it
 *      not where the default value in the class tells it.  Which is why I
 *      get the screen bounds here and instantiate the boids with random
 *      locations on the screen.
 *      
 *      Just a note on GitHub (branches):
 *      
 *      https://www.datree.io/resources/git-create-branch
 *      
 *      06/04/2020
 *          Made quite a few global variables.  These help to fine tune
 *          the game.  Or at least they are supposed to.  Discovered that 
 *          in order to make the globals do anything, the local variables
 *          that get them must be in the Update() method.  Awake() and Start()
 *          only happen once each.  (Awake() can happen more, but not Start().)
 *          
 *      06/08/2020
 *          See notes in Boid.cs.
 *          
 *          One interesting thing that I learned today was about scriptable
 *          objects.  A philosophy of coding Unity projects with these new
 *          (to me) items is seen in:
 *          
 *          https://unity.com/how-to/architect-game-code-scriptable-objects
 *          
 *          I think they would be very useful, but I am not going to use them
 *          in this project.  Probably.
 *          
 *          I am thinking that I will make the project public on GitHub.  
 *          Probably on Friday though.  I want to make a little more progress
 *          before doing so.  But I am about done.  It is a fun project and I
 *          have learned a lot about Unity, but I think it is about time to do
 *          something new.  I just need to make it "complete."  Or done.
 *          Which is a minomer.  I am not sure code is ever done.
 *          
 *      06/09/2020
 *          Made a mistake yesterday that cost a couple hours or so of
 *          debugging.  Forgot to attach the toggle scripts to their
 *          respective toggle switches.  Just needed to drag the scripts to 
 *          the assets in the canvas prefab.  Simple.  Ah well.  Learning
 *          experience.  I tried to figure out the difference in wrapping
 *          (which worked) and the other two toggles (which did not work).  The
 *          simple answer was that they were not the same.  The code was not
 *          included in the toggles.  Ah well.  Quick fix today.
 *          
 *          Got the toggles and slider to work.  Mostly.  I could not get the
 *          slider to not be interactable when the flocking was on, but the 
 *          influence was off.  When flocking is off, the slider is not
 *          interactable, but when flocking is on and influence is off, the
 *          slider is still interactable.  I probably need to look at the 
 *          truth table better, but I am not going to do that.
 *          
 *          I never did fix it so that I could make a different amount of
 *          boids for the simulation.  You can obviously do that with the
 *          Unity project, but not with the full build.  I am going to try to 
 *          do a release on GitHub.  I looked up how to do it.  I am just not
 *          quite sure how it will turn out.
 *          
 *          Overall, I am pleased with the project.  It is something I did 
 *          with a little success when I was trying to demonstrate Windows
 *          programming to my students many years ago.  This is a much better
 *          demonstration even though it is not quite what I thought it would
 *          be.  The boids do not flock quite the way that I was hoping.
 *          
 *          If I was going to continue the project, I would add a way to make
 *          obstacles.  I would also see if I could get the boids to flock 
 *          better.
 *          
 *          The last thing I want to point out is the strange edge case.  The
 *          boundary offers a strange gathering of boids.  I am not sure what
 *          it is.  I do not think they exert influence across the wrap 
 *          boundary.  Still, they congregate at the edges and corners when 
 *          allowed to run for a while.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Singleton GameManager
    //      There will only be one instance of GameManager
    //      There can be only one!
    public static GameManager instance = null;
    // Put other public variables and constants here.
    public GameObject BoidPrefab;
    public GameObject guiControls;
    public int instances = 50;   // if initialized in Awake, cannot change in Unity interface
    Vector2 screenBounds;

    // screen wrap (show up on the other side) or bounce
    public bool wrap;

    public bool flocking;
    // subset of flocking
    public bool separation;
    public bool alignment;
    public bool cohesion;
    public float influenceRadius;
    public float separationFactor;
    public float alignmentFactor;
    public float cohesionFactor;
    public float boundaryFudgeFactor;
    //public int bypassFrameCount;
    //    public float collisionDiameter;
    public bool boidCollisions;

    // Called before start
    private void Awake()
    {
        if(instance == null) { instance = this; }   // This should be only GameManager
        else if(instance != this) { Destroy(gameObject); }  // only have one GameManager

        wrap = true;
        flocking = true;
        separation = true;
        alignment = true;
        cohesion = true;
        influenceRadius = 0.5f;
        separationFactor = 1.0f;
        alignmentFactor = 1.0f;
        cohesionFactor = 0.5f;
        boundaryFudgeFactor = 0.5f; //2.0f;
        //bypassFrameCount = 30;  // at 30 fps, 30 frames is only one second (started much too low when testing)
                                //        collisionDiameter = 0.8f;
        boidCollisions = true;
    }
    // Start is called before the first frame update
    void Start()
    {
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        float xMax = screenBounds.x;
        float yMax = screenBounds.y;
        float xPos;
        float yPos;

        //Instantiate(guiControls, new Vector3(-Screen.width, -Screen.height, 0), Quaternion.identity);
        //Instantiate(guiControls, new Vector3(0, 0, 0), Quaternion.identity);

        for (int i = 0; i < instances; i++)
        {
            //xPos = Random.Range(-8.0f, 8.0f);
            //yPos = Random.Range(-4.0f, 4.0f);
            xPos = Random.Range(-xMax, xMax);
            yPos = Random.Range(-yMax, yMax);
            Instantiate(BoidPrefab, new Vector3(xPos, yPos, 0), Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
