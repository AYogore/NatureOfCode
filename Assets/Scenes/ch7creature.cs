using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ch7creature : MonoBehaviour
{
    public GameObject creatureModel;
    // A list to store ruleset arrays
    public List<int[]> rulesetList = new List<int[]>();

    // Custom Rulesets
    public int[] ruleSet0 = { 0, 1, 0, 1, 1, 0, 1, 0 };
    public int[] ruleSet1 = { 1, 0, 1, 0, 1, 0, 1, 0 };
    public int[] ruleSet2 = { 0, 1, 0, 1, 1, 0, 1, 0 };
    public int[] ruleSet3 = { 1, 1, 0, 0, 1, 0, 1, 1 };
    public int[] ruleSet4 = { 0, 0, 0, 1, 1, 0, 1, 0 };
    public int[] ruleSet5 = { 1, 0, 0, 1, 1, 0, 1, 0 };
    public int[] ruleSet6 = { 0, 1, 1, 0, 1, 0, 1, 1 };

    private int rulesChosen;

    // An object to describe a Wolfram elementary Cellular Automata
    ch7creatureCA ca;
    moverBody mover;
    // How long after the CA has been drawn before reloading the scene, choosing new rule
    private int delay = 0;

    // Start is called before the first frame update
    void Start()
    {
        addRuleSetsToList();
        mover = new moverBody(creatureModel);
        // Choosing a random rule set using Random.Range
        rulesChosen = Random.Range(0, rulesetList.Count);
        int[] ruleset = rulesetList[rulesChosen];
        ca = new ch7creatureCA(ruleset, mover); // Initialize CA


        StartCoroutine(TimeManagement());


       
    }

    private void Update()
    {
        //ca.Display();
        //ca.Generate();

    }

    IEnumerator TimeManagement()
    {
        yield return new WaitForSeconds(2.0f);
        StartCoroutine(MovementManagement());
        Debug.Log("in time");
    }

    IEnumerator MovementManagement()
    {
        yield return new WaitForSeconds(2.0f);
        ca.Randomize();
        ca.Restart();
        ca.Generate();
        ca.Display();
        StartCoroutine(TimeManagement());
        Debug.Log("in move");

    }

    private void addRuleSetsToList()
    {
        rulesetList.Add(ruleSet0);
        rulesetList.Add(ruleSet1);
        rulesetList.Add(ruleSet2);
        rulesetList.Add(ruleSet3);
        rulesetList.Add(ruleSet4);
        rulesetList.Add(ruleSet5);
        rulesetList.Add(ruleSet6);
    }

}

public class ch7creatureCA
{
    private int[] cells; // An array of 0s and 1s
    private int generation; // How many generations?
    private int[] ruleset; // An array to store the ruleset, for example {0,1,1,0,1,1,0,1}
    private int rowWidth; // How wide to make the array
    private int cellCapacity; // We limit how many cells we instantiate
    private int numberOfCells; // Which needs us to keep count

    moverBody mb;
    public ch7creatureCA(int[] ruleSetToUse, moverBody g)
    {
        rowWidth = 17;
        cellCapacity = 650;
        mb = g;

        // How big our screen is in World Units
        numberOfCells = 0;
        ruleset = ruleSetToUse;
        cells = new int[cellCapacity / rowWidth];
        Restart();
    }

    public void Randomize() // If we wanted to make a random Ruleset
    {
        for (int i = 0; i < 8; i++)
        {
            ruleset[i] = Random.Range(0, 2);
        }
    }

    public void Restart()
    {
        for (int i = 0; i < cells.Length; i++)
        {
            cells[i] = 0;
        }
        cells[cells.Length / 2] = 1; // We arbitrarily start with just the middle cell having a state of "1"
        generation = 0;
    }

    // The process of creating the new generation
    public void Generate()
    {
        // First we create an empty array for the new values
        int[] nextGen = new int[cells.Length];

        // For every spot, determine new state by examing current state, and neighbor states
        // Ignore edges that only have one neighor
        for (int i = 1; i < cells.Length - 1; i++)
        {
            int left = cells[i - 1]; // Left neighbor state
            int me = cells[i]; // Current state
            int right = cells[i + 1]; // Right neighbor state
            nextGen[i] = rules(left, me, right); // Compute next generation state based on ruleset
        }

        // The current generation is the new generation
        cells = nextGen;
        generation++;
    }

    public void Display() // Drawing the cells. Cells with a state of 1 are black, cells with a state of 0 are white
    {
        mb.CheckEdges();
        
        if (numberOfCells <= cellCapacity)
        {
            for (int i = 0; i < cells.Length; i++)
            {
                
                numberOfCells++;
                if (cells[i] == 1)
                {
                    mb.Movement1();
                    //moverBody.transform;
                }
                else
                {
                    mb.Movement2();
                }


            }
        }
    }

    private int rules(int a, int b, int c) // Implementing the Wolfram rules
    {
        if (a == 1 && b == 1 && c == 1) return ruleset[0];
        if (a == 1 && b == 1 && c == 0) return ruleset[1];
        if (a == 1 && b == 0 && c == 1) return ruleset[2];
        if (a == 1 && b == 0 && c == 0) return ruleset[3];
        if (a == 0 && b == 1 && c == 1) return ruleset[4];
        if (a == 0 && b == 1 && c == 0) return ruleset[5];
        if (a == 0 && b == 0 && c == 1) return ruleset[6];
        if (a == 0 && b == 0 && c == 0) return ruleset[7];
        return 0;
    }


  
    
}

public class moverBody
{
    Vector3 location;
    Vector3 acceleration;
    Vector3 velocity;
    float topSpeed;

    private float minX, maxX, minY, maxY, minZ, maxZ;


    GameObject creatureBody;
    public moverBody(GameObject g)
    {
        creatureBody = GameObject.Instantiate(g);
        velocity = Vector3.zero;
        //acceleration = new Vector3(-1,0,1);
        //velocity = new Vector3(1f, 0f, 0f);
        topSpeed = 10F;
        minX = 5f;
        maxX = 50f;

        minZ = 5f;
        maxZ = 50f;

        minY = 0f;
        maxY = 20f;

    }

    public void Movement1()
    {
        location = creatureBody.transform.position;
        velocity = new Vector3(1f, 0f, -1f);

        velocity += acceleration * Time.deltaTime;
        // Limit Velocity to the top speed
        velocity = Vector3.ClampMagnitude(velocity, topSpeed);

        // Moves the mover
        location += velocity * Time.deltaTime;

        creatureBody.transform.position = location;
        Debug.Log("move1");
    }

    public void Movement2()
    {
        location = creatureBody.transform.position;

        velocity = new Vector3(-1f, 0f, 1f);

        velocity += acceleration * Time.deltaTime;
        // Limit Velocity to the top speed
        velocity = Vector3.ClampMagnitude(velocity, topSpeed);

        // Moves the mover
        location += velocity * Time.deltaTime;

        creatureBody.transform.position = location;
        Debug.Log("move2");

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



