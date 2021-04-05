using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bee : MonoBehaviour
{
    BeeObj bee; // A Mover and an Attractor
    public FlowerObj flower;
    public GameObject flowerObjInScene;

    private float t;
    public float Radius;
    public float Speed;
    // Start is called before the first frame update
    void Start()
    {
        bee = new BeeObj();
        flower = new FlowerObj(flowerObjInScene);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 force = flower.Attract(bee.body); // Apply the attraction from the Attractor on the Mover
        bee.ApplyForce(force);
        bee.Update();
        flower.Rotation(t, Radius, Speed);
        t += Time.deltaTime;

    }
}



public class FlowerObj
{
    public float mass;
    private Vector3 location;
    private float G;
    public Rigidbody body;

    private GameObject flower;

    public float Radius;
    public float Speed;
    private Vector3 rotation;


    public FlowerObj(GameObject f)
    {
        flower = f;
        flower.GetComponent<SphereCollider>().enabled = false;

        flower.AddComponent<Rigidbody>();
        body = flower.GetComponent<Rigidbody>();
        body.useGravity = false;
        Renderer renderer = flower.GetComponent<Renderer>();
        renderer.material = new Material(Shader.Find("Diffuse"));
        renderer.material.color = Color.red;



        body.mass = 20f;
        G = 9.8f;
    }

    public Vector3 Attract(Rigidbody m)
    {
        Vector3 force = body.position - m.position;
        float distance = force.magnitude;

        // Remember we need to constrain the distance so that our circle doesn't spin out of control
        distance = Mathf.Clamp(distance, 5f, 25f);

        force.Normalize();
        float strength = G * (body.mass * m.mass) / (distance * distance);
        force *= strength;
        return force;
    }

    public void Rotation(float t, float radius, float speed)
    {

        Radius = radius;
        Speed = speed;
        rotation = flower.transform.position;
        float radiusY = (float)(Radius *
                      (0.5f + 0.5f * (
                           Mathf.Sin(t * 0.3f) +
                           0.3 * Mathf.Sin(2 * t + 0.8f) +
                           0.26 * Mathf.Sin(3 * t + 0.8f))));


        rotation = new Vector2(Radius * Mathf.Cos(t * Speed), radiusY * Mathf.Sin(t * Speed));



        flower.transform.position = rotation;
    }

    
}



public class BeeObj
{
    // The basic properties of a mover class
    public Transform transform;
    public Rigidbody body;

    private Vector2 minimumPos, maximumPos;
    
    private GameObject bee;

    public BeeObj()
    {
        bee = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        transform = bee.transform;
        bee.AddComponent<Rigidbody>();
        body = bee.GetComponent<Rigidbody>();
        body.useGravity = false;
        Renderer renderer = bee.GetComponent<Renderer>();
        renderer.material = new Material(Shader.Find("Diffuse"));
        renderer.material.color = Color.white;

        body.mass = 10;
        transform.position = new Vector2(5, 0); // Default location
        findWindowLimits();

    }

    public void ApplyForce(Vector2 force)
    {
        body.AddForce(force, ForceMode.Force);
    }

    public void Update()
    {
        CheckEdges();
    }

    public void CheckEdges()
    {
        Vector2 velocity = body.velocity;
        if (transform.position.x > maximumPos.x || transform.position.x < minimumPos.x)
        {
            velocity.x *= -1;
        }
        if (transform.position.y > maximumPos.y || transform.position.y < minimumPos.y)
        {
            velocity.y *= -1;
        }
        body.velocity = velocity;
    }
    

    private void findWindowLimits()
    {
        Camera.main.orthographic = true;
        Camera.main.orthographicSize = 4;
        minimumPos = Camera.main.ScreenToWorldPoint(Vector2.zero);
        maximumPos = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
    }
}
