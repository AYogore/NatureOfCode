using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Firefly : MonoBehaviour
{//We need to create a walker
    FireflyObj firefly1;
    private float t;
    public float Radius;
    public float Speed;


    // Start is called before the first frame update
    void Start()
    {
        firefly1 = new FireflyObj();
    }

    // Update is called once per frame
    void FixedUpdate()
    {        //Have the walker choose a direction
        //firefly1.step();
        firefly1.CheckEdges();


        t += Time.deltaTime;
        firefly1.Rotation(t, Radius, Speed);
        firefly1.step();
        
    }

}


public class FireflyObj
{
    // The basic properties of a mover class
    private Vector3 location;
    private Vector3 rotation;

    //rotate parameters

    
    public float Radius;
    public float Speed;

    // The window limits
    private Vector2 minimumPos, maximumPos;

    // Gives the class a GameObject to draw on the screen
    public GameObject firefly1 = GameObject.CreatePrimitive(PrimitiveType.Sphere);

    public FireflyObj()
    {
        findWindowLimits();
        location = Vector2.zero;
        rotation = Vector2.zero;
        //We need to create a new material for WebGL
        Renderer r = firefly1.GetComponent<Renderer>();
        r.material = new Material(Shader.Find("Diffuse"));
    }

    public void step()
    {
        location = firefly1.transform.position;
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

        firefly1.transform.position += location * Time.deltaTime;
    }

    public void CheckEdges()
    {
        location = firefly1.transform.position;

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
        firefly1.transform.position = location;
    }

    public void Rotation(float t, float radius, float speed)
    {
        
        Radius = radius;
        Speed = speed;
        rotation = firefly1.transform.position;
        float radiusY = (float)(Radius *
                      (0.5f + 0.5f * (
                           Mathf.Sin(t * 0.3f) +
                           0.3 * Mathf.Sin(2 * t + 0.8f) +
                           0.26 * Mathf.Sin(3 * t + 0.8f))));


        rotation = new Vector2(Radius * Mathf.Cos(t * Speed), radiusY * Mathf.Sin(t * Speed));
       


        firefly1.transform.position = rotation;
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
