using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ch6creature : MonoBehaviour
{
    public float r;
    public float maxforce;
    public float maxspeed;
    public float mass;
    public float gravity = 657f;

    private GameObject vehicle;
    public GameObject food;
    private Rigidbody body;

    private float minX, maxX, minY, maxY, minZ, maxZ;
    private Vector3 location, velocity, acceleration, tempVelocity;

    public Vector3 futureLocation;
    private float topSpeed;

    private float hunger;

    enum State
    {
        Idle,
        Hunting,
        Return,
    };

    private State state;


    // Start is called before the first frame update
    void Start()
    {
        vehicle = this.gameObject;
        location = this.gameObject.transform.position;
        velocity = new Vector3(1f, 0f, -1f);
        acceleration = new Vector3(-0.1F, 0f, -1F);
        topSpeed = 10f;

        minX = 5f;
        maxX = 50f;

        minZ = 5f;
        maxZ = 50f;

        minY = 5f;
        maxY = 30f;


        state = State.Idle;
        //see food start
        body = vehicle.GetComponent<Rigidbody>();
        //assign the mover's GameObject to the varaible

        r = 3.0f;
        maxspeed = 1.0f;
        maxforce = 1f;

        body.drag = 0;
        body.useGravity = false;

        food = GameObject.FindGameObjectWithTag("food");

        state = State.Idle;
        StartCoroutine(BehaviorSwitch(3f));
        StartCoroutine(VelocityRandomizer(3.0f));


    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //idle state
        //add random move behavior
        /*
        velocity = new Vector3(1f, 0f, -1f);

        velocity += acceleration * Time.deltaTime;
        // Limit Velocity to the top speed
        velocity = Vector3.ClampMagnitude(velocity, topSpeed);
        location += velocity * Time.deltaTime;

        this.transform.position = new Vector3(location.x, location.y, location.z);
        
        /*
        //chapter 6 seek movement 
        body.velocity = new Vector3(
            Mathf.Clamp(body.velocity.x, -maxspeed, maxspeed),
            Mathf.Clamp(body.velocity.y, -maxspeed, maxspeed),
            Mathf.Clamp(body.velocity.z, -maxspeed, maxspeed));

        //arrive(target.transform.position);
        vehicle.transform.rotation = Quaternion.LookRotation(body.angularVelocity);
       
        food = GameObject.FindGameObjectWithTag("Player");

        Vector3 foodLocation = food.transform.position;
        seek(food.transform.position);
        */

    }

    void Update()
    {

        

        switch (state)
        {
            case State.Idle:
                this.gameObject.tag = "predatorIdle";
                //t += Time.deltaTime;
                //this.Rotation(t, Radius, Speed);
                velocity = tempVelocity;

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
                this.transform.position = new Vector3(this.transform.position.x, 10f, this.transform.position.z);
                break;

            case State.Return:

                break;
        }
        lookForward();
        CheckEdges();


    }

    IEnumerator BehaviorSwitch(float timer)
    {
        float rand = Random.Range(0f, 4f);
        if(rand >= 2)
        {
            state = State.Hunting;
            this.gameObject.tag = "c1predetor";
            Debug.Log("hunting");

        }
        else
        {
            location = this.gameObject.transform.position;

            state = State.Idle;
            Debug.Log("running");
        }

        yield return new WaitForSeconds(timer);
        StartCoroutine(BehaviorSwitch(timer));
    }

    IEnumerator VelocityRandomizer(float timer)
    {
        float rand = Random.Range(-10f, 10f);
        float randY = Random.Range(-5f, 5f);
        tempVelocity = new Vector3(rand, randY, rand);

        float randTimer = Random.Range(1f, 5f);

        yield return new WaitForSeconds(randTimer);
        StartCoroutine(VelocityRandomizer(randTimer));
    }

    private void lookForward()
    {
       
        futureLocation = location + velocity;
        this.gameObject.transform.LookAt(futureLocation); // We can use the built in 'LookAt' function to automatically face us the right direction

        
    }

    public Vector3 attract(GameObject predator)
    {
        Vector3 difference = location - predator.transform.position;
        float dist = difference.magnitude;
        Vector3 gravityDirection = difference.normalized;
        float g = gravity * (mass * predator.GetComponent<Rigidbody>().mass) / (dist * dist);
        Vector3 gravityVector = gravityDirection * g;

        return gravityVector;
    }

    public void seek(Vector3 target) //turning movement
    {
        Vector3 desired = target - body.transform.position;
        desired.Normalize();
        desired *= maxspeed;
        Vector3 steer = desired - body.velocity;
        Debug.Log(desired);
        steer.x = Mathf.Clamp(steer.x, -maxforce, maxforce);
        steer.y = Mathf.Clamp(steer.y, -maxforce, maxforce);
        steer.z = Mathf.Clamp(steer.z, -maxforce, maxforce);
        applyForce(steer);
    }

    public void arrive(Vector3 target)
    {
        Vector3 desired = target - body.transform.position;
        float d = desired.magnitude;
        desired = desired.normalized;
        Debug.Log(d);
        if (d < 3)
        {
            float m = ExtensionMethods.map(d, 0f, 3f, 0, maxspeed);
            desired *= m;
            Debug.Log("near" + desired);

        }
        else
        {
            desired *= maxspeed;
            Debug.Log("far" + desired);
        }

        Vector3 steer = desired - body.velocity;
        applyForce(steer);
        Debug.DrawLine(body.transform.position, steer + body.transform.position);
    }

    //Newton's second law
    //Receive a force, divide by mass, and add to acceleration
    public void applyForce(Vector3 force)
    {
        body.AddForce(force * Time.fixedDeltaTime, ForceMode.VelocityChange);
    }

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
