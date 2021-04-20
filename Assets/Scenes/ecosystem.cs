using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ecosystem : MonoBehaviour
{
    //This is the full ecosystem controller

    //Chapter 1 creature
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

    //Chapter 6 creature
    public List<GameObject> chapterSixCreatures = new List<GameObject>();
    public GameObject chapterSixCreature;
    public int chapterSixCreaturePopulation;

    //Chapter 7 creature
    public List<GameObject> chapterSevenCreatures = new List<GameObject>();
    public GameObject chapterSevenCreature;
    public int chapterSevenCreaturePopulation;

    //Chapter 8 creature
    public List<GameObject> chapterEightCreatures = new List<GameObject>();
    public GameObject chapterEightCreature;
    public int chapterEightCreaturePopulation;

    public float terrainMin;
    //Terrain
    public pTerrain terrain;

    void Start()
    {
        for(int i = 0; i < chapterOneCreaturePopulation; i++)
        {
            chapterOneCreature = Instantiate(chapterOneCreature, new Vector3(Random.Range(terrainMin, terrain.columns), Random.Range(4f, 20f), Random.Range(terrainMin, terrain.rows)), Quaternion.identity);
            chapterOneCreatures.Add(chapterOneCreature);
        }

        for (int i = 0; i < chapterSixCreaturePopulation; i++)
        {
            chapterOneCreature = Instantiate(chapterSixCreature, new Vector3(Random.Range(terrainMin, terrain.columns), Random.Range(4f, 10f), Random.Range(terrainMin, terrain.rows)), Quaternion.identity);
            chapterOneCreatures.Add(chapterOneCreature);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
