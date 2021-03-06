using UnityEngine;

// In this example, we will be scaling a vector to the unit length of 1.
// The following example draws a vector from a central location towards
// the mouse that is the length of 1 meter(standard unit in Unity).

public class Chapter1Fig6 : MonoBehaviour
{
    // These objects are brought in from the unity scene
    public Camera camera;
    public GameObject centerSphere;
    public GameObject cursorSphere;

    // Create variables for rendering the line between two vectors
    private LineRenderer lineRender;

    // Start is called before the first frame update
    void Start()
    {
        // Create the line renderer component on this script's GameObject
        lineRender = gameObject.AddComponent<LineRenderer>();
        //We need to create a new material for WebGL
        lineRender.material = new Material(Shader.Find("Diffuse"));
    }

    // Update is called once per frame
    void Update()
    {
        // Track the Vector2 of the mouse's position and the center sphere's position
        Vector2 mousePos = camera.ScreenToWorldPoint(Input.mousePosition);
        Vector2 centerPos = centerSphere.transform.position;

        // Calculate the magnitude(the distance between the two spheres)
        Vector2 differenceVector = subtractVectors(mousePos, centerPos);
        // Resize the vector to be the length one meter
        Vector2 unitVector = normalizedOf(differenceVector);

        cursorSphere.transform.position = unitVector;
        // Render the line between the spheres directly 
        lineRender.SetPosition(0, centerPos);
        lineRender.SetPosition(1, unitVector);
    }

    // This method scales the length(magnitude) of a vector to be 1
    // normalizedOf(vec) will yield the same output as Unity's built in vec.normalized
    Vector2 normalizedOf(Vector2 vector)
    {
        // To get the length of the vector to one we have multiply by the reciprocal:
        //               1
        //   length * -------- = 1
        //             length
        float length = magnitudeOf(vector);
        Vector2 lengthOneVector = multiplyVector(vector, 1 / length);
        return lengthOneVector;
    }

    // This method finds the length of a vector using pythagoras theorem
    // magnitudeOf(vec) will yield the same output as Unity's built in property vect.magnitude
    float magnitudeOf(Vector2 vector)
    {
        return Mathf.Sqrt(vector.x * vector.x + vector.y * vector.y);
    }

    // This method calculates A - B component wise
    // subtractVectors(vecA, vecB) will yield the same output as Unity's built in operator: vecA - vecB
    Vector2 subtractVectors(Vector2 vectorA, Vector2 vectorB)
    {
        float newX = vectorA.x - vectorB.x;
        float newY = vectorA.y - vectorB.y;
        return new Vector2(newX, newY);
    }

    // This method calculates A * b component wise
    // multiplyVector(vector, factor) will yield the same output as Unity's built in operator: vector * factor
    Vector2 multiplyVector(Vector2 toMultiply, float scaleFactor)
    {
        float x = toMultiply.x * scaleFactor;
        float y = toMultiply.y * scaleFactor;
        return new Vector2(x, y);
    }
}
