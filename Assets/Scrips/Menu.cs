using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    public bool paused = false;

    public float percentaje = 1; 

    Vector3 DestinationCameraMenu;

    // Start is called before the first frame update
    void Start()
    {
        DestinationCameraMenu = new Vector3(0, 0, -10);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("f") && paused == false)
        {
            paused = true;

            Debug.Log(paused);

            StartCoroutine(MenuSmooth());
        }

        if (transform.position != DestinationCameraMenu && paused == true)
        {
            transform.position = Vector3.MoveTowards(this.transform.position, DestinationCameraMenu, 1f);

            if(Input.GetKeyDown("f") && paused == false)
            {
                paused = false;
            }
        }
    }

    IEnumerator MenuSmooth()
    {
        while (Camera.main.orthographicSize < 65)
        {
            Camera.main.orthographicSize += (0.01f * percentaje); //Esto intenta hacer el cambio de camara de forma smooth, no funciona al parecer

            percentaje = Camera.main.orthographicSize * 65 / 100;

            yield return null;
        }
    }
}
