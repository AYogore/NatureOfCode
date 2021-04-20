﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class predatorAttract : MonoBehaviour
{

    public float gravity = 657f;
    public float mass;
    Vector3 location;
    public GameObject predator;
    public string predatorTag = "";
    public bool alive = true;

    ecosystem eco;
    List<GameObject> ch1Creatures;


    // Start is called before the first frame update
    void Start()
    {
        eco = GameObject.Find("EcosystemController").GetComponent<ecosystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if (alive)
        {
            GameObject[] predators = GameObject.FindGameObjectsWithTag(predatorTag);
            if (predators.Length > 0)
            {
                foreach (GameObject predator in predators)
                {
                    location = this.gameObject.transform.position;
                    Vector3 desired = this.transform.position - predator.transform.position;

                    predator.transform.GetComponent<Rigidbody>().AddForce(desired, ForceMode.Acceleration);
                    predator.transform.GetComponent<Rigidbody>().AddForce(attract(predator), ForceMode.Acceleration);

                    float dist = Vector3.Distance(predator.transform.position, location);
                    //4f
                    if (dist <= 4f)
                    {
                        alive = false;
                        if (predatorTag == "c1predetor")
                        {
                            eco.chapterOneCreatures.Remove(this.gameObject);
                            
                        }
                        else if (predatorTag == "c2predetor")
                        {
                            eco.chapterTwoCreatures.Remove(this.gameObject);
                        }
                        else if (predatorTag == "")
                        {
                            eco.chapterThreeCreatures.Remove(this.gameObject);
                        }
                        else if (predatorTag == "")
                        {
                            eco.chapterSixCreatures.Remove(this.gameObject);
                        }
                        else if (predatorTag == "")
                        {
                            eco.chapterSevenCreatures.Remove(this.gameObject);
                        }
                        else if (predatorTag == "")
                        {
                            eco.chapterEightCreatures.Remove(this.gameObject);
                        }
                        Destroy(this.gameObject);
                    }
                }
            }
        }
        else
        {
            //Dead, do nothing.
        }
    }

    public Vector3 attract(GameObject predator)
    {
        Vector3 difference = location - predator.transform.position;
        float dist = difference.magnitude;
        Vector3 gravityDirection = difference.normalized;
        float g = gravity * (mass * predator.GetComponent<Rigidbody>().mass) / (dist * dist);
        Vector3 gravityVector = gravityDirection * g;

        return gravityVector;
    }
}
