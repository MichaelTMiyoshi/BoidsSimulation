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
 *      0/08/2020
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
    public int instances = 1;   // if initialized in Awake, cannot change in Unity interface
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
    public int bypassFrameCount;
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
        bypassFrameCount = 30;  // at 30 fps, 30 frames is only one second (started much too low when testing)
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
