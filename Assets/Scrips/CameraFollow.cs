using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Vector2 Velocity; 
    public float SmoothTime = 0.15f; 

    public Transform Target;
    public float minX;
    public float minY;
    public float maxX;
    public float maxY;

    Menu menu;

    // Start is called before the first frame update
    void Start()
    {
        menu = GetComponent<Menu>();
    }

    // Update is called once per frame
    void Update()
    {
        if (menu.paused == false)
        {
            float posX = Mathf.SmoothDamp(transform.position.x, Target.position.x, ref Velocity.x, SmoothTime);
            float posY = Mathf.SmoothDamp(transform.position.y, Target.position.y, ref Velocity.y, SmoothTime);

            transform.position = new Vector3(posX, posY, transform.position.z);
        }
    }
}
