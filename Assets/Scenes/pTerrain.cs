using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pTerrain : MonoBehaviour
{

    List<Vector3> terrainArray = new List<Vector3>();
    public GameObject terrainCube;

    //Rows and Columns to set limits for procedural cube
    public int columns, rows;
    public float thetaInput, rotationThetaInput;

    //Color Pallete
    public Color color1, color2, color3, color4, color5, color6;

    // Start is called before the first frame update
    void Start()
    {
        GameObject terrain = new GameObject();
        terrain.name = "terrain";


        float xOffset = 0;
        for(int i = 0; i < columns; i++)
        {
            float yOffset = 0;
            for (int j = 0; j < rows; j++)
            {
                float theta = ExtensionMethods.map(Mathf.PerlinNoise(xOffset, yOffset), 0f, 1f, 0f, thetaInput);
                //perlin noise ( x, y, set to 0, to 1, multiply from 0, to 10)


                float rotationTheta = ExtensionMethods.map(Mathf.PerlinNoise(xOffset, yOffset), 0f, 1f, 0f, rotationThetaInput);
                
                
                //adds rotation to not make grid
                Quaternion perlinRotation = new Quaternion();
                Vector3 perlinRotationVector3 = new Vector3(Mathf.Cos(rotationTheta), Mathf.Sin(rotationTheta), 0f);
                perlinRotation.eulerAngles = perlinRotationVector3 * 100;


                terrainCube = Instantiate(terrainCube, new Vector3(i, theta, j), perlinRotation);
                //creates cube at set row and column (x,z) but random height (y)
                terrainCube.transform.SetParent(terrain.transform);

                Renderer terrainRenderer = terrainCube.GetComponent<Renderer>();
                terrainRenderer.material.SetColor("_Color", colorTerrain(terrainCube.transform.position));

                yOffset += .06f;
            }
            
            xOffset += .06f;
        }
    }

    public Color colorTerrain (Vector3 terrainCubePosition)
    {
        Color terrainColor = new Vector4(1f, 1f, 1f);

        if (terrainCubePosition.y >= 0f && terrainCubePosition.y <= 3.5f)
        {
            terrainColor = color1;
        }
        else if (terrainCubePosition.y >= 3.5 && terrainCubePosition.y <= 4.5f)
        {
            terrainColor = color2;
        }
        else if (terrainCubePosition.y >= 4.5 && terrainCubePosition.y <= 5.5f)
        {
            terrainColor = color3;
        }
        else if (terrainCubePosition.y >= 5.5 && terrainCubePosition.y <= 6.5f)
        {
            terrainColor = color4;
        }
        else if (terrainCubePosition.y >= 6.5 && terrainCubePosition.y <= 7.5f)
        {
            terrainColor = color5;
        }
        else if (terrainCubePosition.y >= 7.5)
        {
            terrainColor = color6;
        }
        
        return terrainColor;
    }

}
    