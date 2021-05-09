using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chapterOneCreature : MonoBehaviour
{

    Vector3 location;
    Vector3 rotation;
    Vector3 acceleration;
    Vector3 velocity;

    enum State
    {
        Idle,
        Hunting,
        Return,
    };

    private State state;

    public float Radius;
    public float Speed;
    float t;

    float topSpeed;

    public float r;
    public float maxforce;
    public float maxspeed;
    public float mass;

    public Rigidbody rb;

    public GameObject foodObj;


    private float minX, maxX, minY, maxY, minZ, maxZ;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent <Rigidbody>();
        location = this.gameObject.transform.position; // Vector2.zero is a (0, 0) vector
        velocity = new Vector3(1f, 0f, 0f);
        acceleration = new Vector3(-0.1F, 0f, -1F);
        topSpeed = 10F;

        minX = 5f;
        maxX = 50f;

        minZ = 5f;
        maxZ = 50f;

        minY = 0f;
        maxY = 20f;

        state = State.Idle;

    }

    // Update is called once per frame
    void Update()
    {

        switch (state)
        {
            case State.Idle:
                //t += Time.deltaTime;
                //this.Rotation(t, Radius, Speed);
                velocity = new Vector3(1f, 0f, -1f);

                velocity += acceleration * Time.deltaTime; 
                // Limit Velocity to the top speed
                velocity = Vector3.ClampMagnitude(velocity, topSpeed);

                // Moves the mover
                location += velocity * Time.deltaTime;

                CheckEdges();

                // Updates the GameObject of this movement
                this.transform.position = new Vector3(location.x, location.y, location.z);
                break;
            case State.Hunting:
                seek(foodObj.transform.position);
                break;

            case State.Return:

                break;
        }
        

    }

    public void ApplyForce(Vector2 force)
    {
        rb.AddForce(force);
    }

    public void seek(Vector3 target)
    {
        Vector3 desired = target - this.transform.position;
        desired.Normalize();
        desired *= maxspeed;
        Vector3 steer = desired - this.velocity;
        steer.x = Mathf.Clamp(steer.x, -maxforce, maxforce);
        steer.y = Mathf.Clamp(steer.y, -maxforce, maxforce);
        steer.z = Mathf.Clamp(steer.z, -maxforce, maxforce);
        ApplyForce(steer);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "food")
        {
            state = State.Hunting;
            foodObj = collision.gameObject;
        }
    }

    /*public void Rotation(float t, float radius, float speed)
    {

        Radius = radius;
        Speed = speed;
        rotation = this.transform.position;
        float radiusY = (float)(Radius *
                      (0.5f + 0.5f * (
                           Mathf.Sin(t * 0.3f) +
                           0.3 * Mathf.Sin(2 * t + 0.8f) +
                           0.26 * Mathf.Sin(3 * t + 0.8f))));


        rotation = new Vector3(Radius * Mathf.Cos(t * Speed), 20f, radiusY * Mathf.Sin(t * Speed));



        this.transform.position = rotation;
    }*/

    public void CheckEdges()
    {

        if (location.x > maxX)
        {
            location.x -= maxX - minX;
        }
        else if (location.x < minX)
        {
            location.x += maxX - minX;
        }
        if (location.y > maxY)
        {
            location.y -= maxY - minY;
        }
        else if (location.y < minY)
        {
            location.y += maxY - minY;
        }

        if (location.z > maxZ)
        {
            location.z -= maxZ - minZ;
        }
        else if (location.z < minZ)
        {
            location.z += maxZ - minZ;
        }
    }
}
