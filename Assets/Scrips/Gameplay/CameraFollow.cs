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
            float posX = Mathf.SmoothDamp(transform.position.x, Target.position.x, ref Velocity.x, SmoothTime);
            float posY = Mathf.SmoothDamp(transform.position.y, Target.position.y, ref Velocity.y, SmoothTime);

            transform.position = new Vector3(posX, posY, transform.position.z);
    }
}
