using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeaFireFly : MonoBehaviour
{
    enum State
    {
        Idle,
        Hunting,
        Return,
    };

    private State state;

    private float minX, maxX, minY, maxY, minZ, maxZ;

    Vector3 location;
    Vector3 acceleration;
    Vector3 velocity;

    Rigidbody rb;
    float topSpeed;

    void Start()
    {
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
        
    }


}
