using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatFollow : MonoBehaviour
{
    public Transform Drakar;

    public float Velocity = 0.02f;

    public float SinFrecuency;
    public float SinMagnitude; 

    Vector3 DrakarSin;

    public float Distance = 1;

    public bool Grabbed = false; 

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Grabbed)
        {
            if (Vector3.Distance(transform.position, Drakar.position) > Distance)
            {
                DrakarSin = transform.position + (Drakar.position - transform.position).normalized * Time.deltaTime * Velocity;

                transform.position += transform.up * Mathf.Sin(Time.time * SinFrecuency) * SinMagnitude;

                transform.position = DrakarSin;
            }
        }
       
    }
}
