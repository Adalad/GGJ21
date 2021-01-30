using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Vector2 Velocity; 
    public float SmoothTime = 0.15f;
    public Transform Target;

    private void FixedUpdate()
    {
        transform.position = new Vector3(Target.transform.position.x, Target.transform.position.y, -10) ; 
    }
}
