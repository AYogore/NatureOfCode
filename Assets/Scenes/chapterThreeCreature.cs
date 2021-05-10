using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chapterThreeCreature : MonoBehaviour
{

    Vector3 location;
    Vector3 rotation;
    Vector3 acceleration;
    Vector3 velocity;
    Vector3 tempVelocity;

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

    public GameObject thisGameObj;
    public GameObject nestGO;

    private float birthCount;
    private float deathCount;

    public int maxOffspring;
    public int currentOffspring;

    private float minX, maxX, minY, maxY, minZ, maxZ;

    public ecosystem eco;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        location = this.gameObject.transform.position; // Vector2.zero is a (0, 0) vector
        velocity = new Vector3(1f, 0f, 0f);
        tempVelocity = new Vector3(0f, 0f, 0f);
        acceleration = new Vector3(-0.1F, 0f, -1F);
        topSpeed = 10F;

        minX = 0f;
        maxX = 50f;

        minZ = 0f;
        maxZ = 50f;

        minY = 5f;
        maxY = 30f;

        state = State.Idle;

        StartCoroutine(VelocityRandomizer(1.0f));

        float randBC = Random.Range(-5f, 1f);
        birthCount = 0 + randBC;
        deathCount = 20 + randBC;
        nestGO = GameObject.FindGameObjectWithTag("Nest");
        maxOffspring = 1;

        eco = GameObject.Find("EcosystemController").GetComponent<ecosystem>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Debug.Log("birth count = " + birthCount);
        deathCount -= Time.deltaTime;
        if (deathCount <= 0)
        {
            eco.chapterThreeCreatures.Remove(this.gameObject);
            tentacles t = this.gameObject.GetComponent<tentacles>();
            t.DestroyTentacle();
            Destroy(this.gameObject);

        }
        switch (state)
        {
            case State.Idle:

                velocity = tempVelocity;
                velocity.y = Random.Range(-5f, 5f) * Time.deltaTime;
                velocity += acceleration * Time.deltaTime;
                // Limit Velocity to the top speed
                velocity = Vector3.ClampMagnitude(velocity, topSpeed);

                // Moves the mover
                location += velocity * Time.deltaTime;

                CheckEdges();

                // Updates the GameObject of this movement
                this.transform.position = new Vector3(location.x, location.y, location.z);
                birthCount += Time.deltaTime;
                if (birthCount >= 10)
                {
                    velocity = nestGO.transform.position - this.gameObject.transform.position;

                    state = State.Return;
                    birthCount = 0;

                }
                break;
            case State.Hunting:
                break;

            case State.Return:

                if (currentOffspring <= maxOffspring)
                {
                    birthCount = 0;
                    StartCoroutine(BirthCount(10f));

                    state = State.Idle;
                }

                else
                {
                    birthCount = 0;
                    state = State.Idle;
                }

                break;
        }


    }

    IEnumerator VelocityRandomizer(float timer)
    {
        float randX = Random.Range(-10f, 10f);
        float randY = Random.Range(-5f, 5f);
        float randZ = Random.Range(-10f, 10f);

        tempVelocity = new Vector3(randX, randY, randZ);

        float randTimer = Random.Range(1f, 5f);

        yield return new WaitForSeconds(randTimer);
        StartCoroutine(VelocityRandomizer(randTimer));
    }

    IEnumerator BirthCount(float timer)
    {

        float bCount = birthCount;
        if (bCount >= 0)
        {
            birthCount = 0;
            GiveBirth();
        }

        float rand = Random.Range(-2f, 2f);

        yield return new WaitForSeconds(rand + timer);
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
        if (collision.gameObject.tag == "fish")
        {
            //birthCount += 1;
        }
    }

    public void GiveBirth()
    {
        birthCount = 0;
        GameObject child = thisGameObj;

        eco.chapterThreeCreatures.Add(child);
        Instantiate(child, nestGO.transform.position, Quaternion.identity);

        currentOffspring += 1;

        state = State.Idle;
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
