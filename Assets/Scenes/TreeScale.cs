using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeScale : MonoBehaviour
{

    public float scalar = 0.5f;
    public void Made(int index)
    {
        this.transform.localScale *= scalar;
    }
}
