using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoTrailer : MonoBehaviour
{
    public GameObject barco;

    public float speed; 

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.right * Time.deltaTime; 
    }
}
