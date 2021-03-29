using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Butterfly : MonoBehaviour
{
    // Variables for the location and speed of mover
    private Vector2 location = new Vector2(0F, 0F);
    private Vector2 velocity = new Vector2(0.01F, 0.01F);

    // Variables to limit the mover within the screen space
    private Vector2 minimumPos, maximumPos;

    // A Variable to represent our mover in the scene
    private GameObject mover;

    public ButterflyObj butterfly1;

    // Start is called before the first frame update
    void Start()
    {
        // We want to start by setting the camera's projection to Orthographic mode
        Camera.main.orthographic = true;

        // Next we grab the minimum and maximum position for the screen
        minimumPos = Camera.main.ScreenToWorldPoint(Vector2.zero);
        maximumPos = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));

        // We now can set the mover as a primitive sphere in unity
        mover = GameObject.CreatePrimitive(PrimitiveType.Sphere);

        butterfly1 = new ButterflyObj();
        //We need to create a new material for WebGL
        Renderer r = mover.GetComponent<Renderer>();
        r.material = new Material(Shader.Find("Diffuse"));
    }

    // Update is called once per frame forever and ever (until you quit).
    void Update()
    {
        // Each frame, we will check to see if the mover has touched a boarder
        // We check if the X/Y position is greater than the max position OR if it's less than the minimum position
        bool xHitBorder = location.x > maximumPos.x || location.x < minimumPos.x;
        bool yHitBorder = location.y > maximumPos.y || location.y < minimumPos.y;

        // If the mover has hit at all, we will mirror it's speed with the corrisponding boarder

        if (xHitBorder)
        {
            velocity.x = -velocity.x;
        }

        if (yHitBorder)
        {
            velocity.y = -velocity.y;
        }

        // Lets now update the location of the mover
        location += velocity;

        // Now we apply the positions to the mover to put it in it's place
        mover.transform.position = new Vector2(location.x, location.y);
    }

    private void FixedUpdate()
    {
        //walker.step();
        butterfly1.step();
        butterfly1.CheckEdges();
    }

    public class ButterflyObj
    {
        // The basic properties of a mover class
        private Vector3 location;

        // The window limits
        private Vector2 minimumPos, maximumPos;

        // Gives the class a GameObject to draw on the screen
        public GameObject butterfly1 = GameObject.CreatePrimitive(PrimitiveType.Sphere);

        public ButterflyObj()
        {
            findWindowLimits();
            location = Vector2.zero;
            //We need to create a new material for WebGL
            Renderer r = butterfly1.GetComponent<Renderer>();
            r.material = new Material(Shader.Find("Diffuse"));
        }

        public void step()
        {
            location = butterfly1.transform.position;
            //Each frame choose a new Random number 0,1,2,3, 
            //If the number is equal to one of those values, take a step
            int choice = Random.Range(0, 10);
            if (choice <= 3)
            {
                location.x++;

            }
            else if (choice == 4)
            {
                location.x--;
            }
            else if (choice == 5)
            {
                location.y++;
            }
            else
            {
                location.y--;
            }

            butterfly1.transform.position += location * Time.deltaTime;
        }

        public void CheckEdges()
        {
            location = butterfly1.transform.position;

            if (location.x > maximumPos.x)
            {
                location = Vector2.zero;
            }
            else if (location.x < minimumPos.x)
            {
                location = Vector2.zero;
            }
            if (location.y > maximumPos.y)
            {
                location = Vector2.zero;
            }
            else if (location.y < minimumPos.y)
            {
                location = Vector2.zero;
            }
            butterfly1.transform.position = location;
        }

        private void findWindowLimits()
        {
            // We want to start by setting the camera's projection to Orthographic mode
            Camera.main.orthographic = true;
            // Next we grab the minimum and maximum position for the screen
            minimumPos = Camera.main.ScreenToWorldPoint(Vector2.zero);
            maximumPos = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        }

    }
        

}
