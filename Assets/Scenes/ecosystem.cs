using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ecosystem : MonoBehaviour
{
    //This is the full ecosystem controller

    //Chapter 1 creature moving left to right
    public List<GameObject> chapterOneCreatures = new List<GameObject>();
    public GameObject chapterOneCreature;
    public int chapterOneCreaturePopulation;
    //Chapter 2 creature
    public List<GameObject> chapterTwoCreatures = new List<GameObject>();
    public GameObject chapterTwoCreature;
    public int chapterTwoCreaturePopulation;

    //Chapter 3 creature
    public List<GameObject> chapterThreeCreatures = new List<GameObject>();
    public GameObject chapterThreeCreature;
    public int chapterThreeCreaturePopulation;

    //Chapter 6 creature predator
    public List<GameObject> chapterSixCreatures = new List<GameObject>();
    public GameObject chapterSixCreature;
    public int chapterSixCreaturePopulation;

    //Chapter 7 creature flock
    public List<GameObject> chapterSevenCreatures = new List<GameObject>();
    public GameObject chapterSevenCreature;
    public int chapterSevenCreaturePopulation;

    public flockBehavior flockBehaviorScript;

    //Chapter 8 creature
    public List<GameObject> chapterEightCreatures = new List<GameObject>();
    public GameObject chapterEightCreature;
    public int chapterEightCreaturePopulation;

    public float terrainMin;
    //Terrain
    public pTerrain terrain;

    void Start()
    {

        StartCoroutine(ShortenList());
        for(int i = 0; i < chapterOneCreaturePopulation; i++) //grey
        {
            chapterOneCreature = Instantiate(chapterOneCreature, new Vector3(Random.Range(terrainMin, terrain.columns), Random.Range(4f, 20f), Random.Range(terrainMin, terrain.rows)), Quaternion.identity);
            chapterOneCreatures.Add(chapterOneCreature);
        }

        for (int i = 0; i < chapterTwoCreaturePopulation; i++) //nest
        {
            chapterTwoCreature = Instantiate(chapterTwoCreature, new Vector3(Random.Range(terrainMin, terrain.columns), Random.Range(4f, 20f), Random.Range(terrainMin, terrain.rows)), Quaternion.identity);
            chapterTwoCreatures.Add(chapterTwoCreature);
        }

        for (int i = 0; i < chapterThreeCreaturePopulation; i++) //oscillation
        {
            chapterThreeCreature = Instantiate(chapterThreeCreature, new Vector3(Random.Range(terrainMin, terrain.columns), Random.Range(4f, 20f), Random.Range(terrainMin, terrain.rows)), Quaternion.identity);
            chapterThreeCreatures.Add(chapterThreeCreature);
        }

        for (int i = 0; i < chapterSixCreaturePopulation; i++) //predator
        {
            chapterSixCreature = Instantiate(chapterSixCreature, new Vector3(Random.Range(terrainMin, terrain.columns), Random.Range(4f, 10f), Random.Range(terrainMin, terrain.rows)), Quaternion.identity);
            chapterSixCreatures.Add(chapterSixCreature);
        }

        for (int i = 0; i < chapterSevenCreaturePopulation; i++) //flock
        {
            chapterSevenCreature = Instantiate(chapterSevenCreature, new Vector3(Random.Range(terrainMin, terrain.columns), Random.Range(4f, 10f), Random.Range(terrainMin, terrain.rows)), Quaternion.identity);
        
        }
        flockBehaviorScript = chapterSevenCreature.GetComponent<flockBehavior>();
        chapterSevenCreatures = flockBehaviorScript.eco.chapterSevenCreatures;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator ShortenList()
    {

        chapterOneCreatures.RemoveAll(item => item == null);
        chapterTwoCreatures.RemoveAll(item => item == null);
        chapterThreeCreatures.RemoveAll(item => item == null);
        chapterSixCreatures.RemoveAll(item => item == null);
        chapterSevenCreatures.RemoveAll(item => item == null);
        yield return new WaitForSeconds(1.0f);

        StartCoroutine(ShortenList());
    }
}
