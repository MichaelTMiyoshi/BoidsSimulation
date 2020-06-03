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
    public int instances = 1;
    Vector2 screenBounds;

    private void Awake()
    {
        if(instance == null) { instance = this; }   // This should be only GameManager
        else if(instance != this) { Destroy(gameObject); }  // only have one GameManager
    }
    // Start is called before the first frame update
    void Start()
    {
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        float xMax = screenBounds.x;
        float yMax = screenBounds.y;
        float xPos;
        float yPos;
        
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
