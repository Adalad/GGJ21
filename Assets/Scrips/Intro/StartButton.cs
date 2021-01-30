using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour
{
    [Range(1f, 2f)]
    public float MaxScale = 1.1f;
    [Range(0f, 1f)]
    public float MinScale = 0.9f;
    [Range(0.0f, 0.5f)]
    public float ScaleDelta = 0.05f;

    private void Start()
    {
        StartCoroutine(Zooming());
    }

    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            SceneManager.LoadScene("CinematicScene");
        }
    }

    private IEnumerator Zooming()
    {
        float scale = 1f;
        while (true)
        {
            while (scale < MaxScale)
            {
                scale += ScaleDelta;
                transform.localScale = Vector3.one * scale;
                yield return new WaitForSeconds(0.05f);
            }

            while (MinScale < scale)
            {
                scale -= ScaleDelta;
                transform.localScale = Vector3.one * scale;
                yield return new WaitForSeconds(0.05f);
            }
        }

    }
}
