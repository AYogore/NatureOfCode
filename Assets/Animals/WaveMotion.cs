using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveMotion : MonoBehaviour
{

    public float amplitude, period;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float x = amplitude * Mathf.Cos((2 * Mathf.PI) * Time.time / period);
        this.transform.position = new Vector2(x, 0f);
    }
}
