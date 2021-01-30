using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    GameObject Camera; 
    float CameraSpeed;

    public bool paused = false;

     

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("f") && paused == false)
        {
            OpenMap();
            paused = true; 
        }

        if (Input.GetKeyDown("f") && paused == true)
        {            
            paused = false;
        }
    }

    void OpenMap()
    {
        Debug.Log("f"); 
        Vector3.MoveTowards(transform.position, new Vector3(0, 0, transform.position.z), CameraSpeed); 
    }
}
