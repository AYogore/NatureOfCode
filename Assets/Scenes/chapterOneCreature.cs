﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chapterOneCreature : MonoBehaviour
{

    Vector3 location;
    Vector3 rotation;
    Vector3 acceleration;
    Vector3 velocity;
    public float Radius;
    public float Speed;
    float t;

    float topSpeed;


    private float minX, maxX, minY, maxY, minZ, maxZ;

    // Start is called before the first frame update
    void Start()
    {
        location = this.gameObject.transform.position; // Vector2.zero is a (0, 0) vector
        velocity = new Vector3(1f, 0f, -1f);
        acceleration = new Vector3(-0.1F, 0f, -1F);
        topSpeed = 10F;

        minX = 5f;
        maxX = 115f;

        minZ = 5f;
        maxZ = 115f;

        minY = 0f;
        maxY = 20f;
    }

    // Update is called once per frame
    void Update()
    {
        //t += Time.deltaTime;
        //this.Rotation(t, Radius, Speed);
        velocity = new Vector3(1f, 0f, -1f);

        velocity += acceleration * Time.deltaTime; // Time.deltaTime is the time passed since the last frame.
        Debug.Log(acceleration);
        Debug.Log(velocity);
        // Limit Velocity to the top speed
        velocity = Vector3.ClampMagnitude(velocity, topSpeed);

        // Moves the mover
        location += velocity * Time.deltaTime;

        CheckEdges();

        // Updates the GameObject of this movement
        this.transform.position = new Vector3(location.x, location.y, location.z);

    }

    public void Rotation(float t, float radius, float speed)
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