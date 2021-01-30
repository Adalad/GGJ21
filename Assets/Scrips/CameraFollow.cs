using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public float FollowSpeed = 2;
    public Transform Target;
    public float minX;
    public float minY;
    public float maxX;
    public float maxY;

    private 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    //void Update()
    //{
    //    transform.position = Vector3.SmoothDamp(Target.transform.position.x,Target.transform.position.y, transform.position.z);
    //}
}
