using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoverFollowingFood : MonoBehaviour
{
    //We need to create a walker
    introFollower walker;

    //create food
    introFollower food;

    // Start is called before the first frame update
    void Start()
    {
        //initiate food as intromover, and mover as follower
        walker = new introFollower();
        food = new introFollower();
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {        
        //walker.step();
        food.step();
        food.CheckEdges();
        //new method to follow food
        walker.follow(food);
        walker.CheckEdges();
    }

    
    public class introFollower
    {
        // The basic properties of a mover class
        private Vector3 location;

        // The window limits
        private Vector2 minimumPos, maximumPos;

        // Gives the class a GameObject to draw on the screen
        public GameObject mover = GameObject.CreatePrimitive(PrimitiveType.Sphere);

        public introFollower()
        {
            findWindowLimits();
            location = Vector2.zero;
            //We need to create a new material for WebGL
            Renderer r = mover.GetComponent<Renderer>();
            r.material = new Material(Shader.Find("Diffuse"));
        }

        public void follow(introFollower foodObj)
        {
            float num = Random.Range(0f, 1f);
            location = mover.transform.position;
            Vector3 foodLocation = Vector2.zero;
            foodLocation = foodObj.mover.transform.position;

            //Each frame choose a new Random number 0-1;
            //If the number is less than the the float take a step
            if (num < 0.2F)
            {
                location.y++;
            }
            else if (num > 0.2F && num < 0.4F)
            {
                location.y--;
            }
            else if (num > 0.4F && num < .6F)
            {
                location.x--;
            }
            else if (num > .6f)
            {
                location = Vector3.MoveTowards(location, foodLocation, Time.deltaTime);
            }
            mover.transform.position += location * Time.deltaTime;


        }

        /*public void follow(introFollower foodObj)
        {
            /*
            //set positions of food and mover
            location = mover.transform.position;
            Vector3 foodLocation = Vector2.zero;
            foodLocation = foodObj.mover.transform.position;

            //sets a follow vector for mover to move towards food
            Vector2 followVector = new Vector2(foodLocation.x - location.x, foodLocation.y - location.y);



            //set to percentage 50% at int 5
            //if(Random.Range(0, 10) <= 5)
            //{
                //move to food with vector by shrinking vector
                if (foodLocation.x > location.x)
                    location.x++;
                else if (foodLocation.x < location.x)
                    location.x--;

                if (foodLocation.y > location.y)
                    location.y++;
                else if (foodLocation.y < location.y)
                    location.y--;
            //}

            //eat object if in same position
            if(foodLocation.x == location.x && foodLocation.y == location.y)
            {
                Destroy(foodObj.mover);
            }
            

            float num = Random.Range(0f, 1f);
            location = mover.transform.position;
            //Each frame choose a new Random number 0,1,2,3, 
            //If the number is equal to one of those values, take a step
            

            mover.transform.position += location * Time.deltaTime;



        }
    */
        public void step()
        {
            location = mover.transform.position;
            //Each frame choose a new Random number 0,1,2,3, 
            //If the number is equal to one of those values, take a step
            int choice = Random.Range(0, 4);
            if (choice == 0)
            {
                location.x++;

            }
            else if (choice == 1)
            {
                location.x--;
            }
            else if (choice == 3)
            {
                location.y++;
            }
            else
            {
                location.y--;
            }

            mover.transform.position += location * Time.deltaTime;
        }

        public void CheckEdges()
        {
            location = mover.transform.position;

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
            mover.transform.position = location;
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
