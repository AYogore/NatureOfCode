using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tentacles : MonoBehaviour
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

    List<oscillator> oscilattors = new List<oscillator>();



    private float minX, maxX, minY, maxY, minZ, maxZ;

    // Start is called before the first frame update
    void Start()
    {

        while (oscilattors.Count < 10)
        {
            oscilattors.Add(new oscillator());
        }

        rb = GetComponent<Rigidbody>();
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
                break;

            case State.Return:

                break;
        }


    }

    void FixedUpdate()
    {
        foreach (oscillator o in oscilattors)
        {
            //Each oscillator object oscillating on the x-axis
            float x = Mathf.Sin(o.angle.x) * o.amplitude.x;
            //Each oscillator object oscillating on the y-axis
            float y = Mathf.Sin(o.angle.y) * o.amplitude.y;
            //Add the oscillator's velocity to its angle
            o.angle += o.velocity;
            // Draw the line for each oscillator
            o.lineRender.SetPosition(1, o.oGameObject.transform.position);
            //Move the oscillator
            o.oGameObject.transform.transform.Translate(new Vector2(x, y) * Time.deltaTime);
        }
    }


    public class oscillator
    {

        // The basic properties of an oscillator class
        public Vector3 velocity, angle, amplitude;

        // The window limits
        private Vector3 maximumPos;

        // Gives the class a GameObject to draw on the screen
        public GameObject oGameObject = GameObject.CreatePrimitive(PrimitiveType.Sphere);

        //Create variables for rendering the line between two vectors
        public LineRenderer lineRender;

        public oscillator()
        {
            angle = Vector3.zero;
            velocity = new Vector3(Random.Range(-.05f, .05f), Random.Range(-0.05f, 0.05f));
            amplitude = new Vector3(Random.Range(-maximumPos.x / 2, maximumPos.x / 2), Random.Range(-maximumPos.y / 2, maximumPos.y / 2));

            //We need to create a new material for WebGL
            Renderer r = oGameObject.GetComponent<Renderer>();
            r.material = new Material(Shader.Find("Diffuse"));

            // Create a GameObject that will be the line
            GameObject lineDrawing = new GameObject();
            //Add the Unity Component "LineRenderer" to the GameObject lineDrawing.
            lineRender = lineDrawing.AddComponent<LineRenderer>();
            lineRender.material = new Material(Shader.Find("Diffuse"));
            //Begin rendering the line between the two objects. Set the first point (0) at the centerSphere Position
            //Make sure the end of the line (1) appears at the new Vector3
            Vector3 center = new Vector3(0f, 0f);
            lineRender.SetPosition(0, center);
        }
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