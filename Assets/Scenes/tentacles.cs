using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tentacles : MonoBehaviour
{
    public float amplitude = 1000f;
    
    
    public Vector3 center, location, velocity, angle, tempVelocity;

    //Create variables for rendering the line between two vectors
    GameObject lineDrawing;
    LineRenderer lineRender;
    GameObject sphere;

    // Start is called before the first frame update
    void Start()
    {


        StartCoroutine(VelocityRandomizer(3.0f));
        angle = Vector3.zero;
        // Create a GameObject that will be the line
        lineDrawing = new GameObject();
        //Add the Unity Component "LineRenderer" to the GameObject lineDrawing.
        lineRender = lineDrawing.AddComponent<LineRenderer>();
        lineRender.material = new Material(Shader.Find("Diffuse"));
        //Begin rendering the line between the two objects. Set the first point (0) at the centerSphere Position
        //Make sure the end of the line (1) appears at the new Vector3
        location = this.gameObject.transform.position;
        center = location;
        lineRender.SetPosition(0, center);

        //Create the sphere at the end of the line
        sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        //We need to create a new material for WebGL
        Renderer r = sphere.GetComponent<Renderer>();
        r.material = new Material(Shader.Find("Diffuse"));

        velocity = new Vector2(Random.Range(-.05f, .05f), Random.Range(-0.05f, 0.05f));
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        center = this.gameObject.transform.position;
        lineRender.SetPosition(0, center);
        float x = amplitude * Mathf.Sin(angle.x);
        float y = amplitude * Mathf.Cos(angle.y);
        float z = amplitude * Mathf.Cos(angle.z);
        //Using the concept of angular velocity to increment an angle variable
        //Admittedly, in this example we are not really using this variable as an angle, but we will next
        angle += velocity;

        //Place the sphere and the line at the position
        sphere.transform.position = new Vector3(center.x + x, center.y + y, center.z + z);
        lineRender.SetPosition(1, sphere.transform.position);
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

    public void DestroyTentacle()
    {
        Destroy(sphere);
        Destroy(lineDrawing);
    }
}