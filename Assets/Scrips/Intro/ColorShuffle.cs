using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class ColorShuffle : MonoBehaviour
{
    [Range(0f, 100f)]
    public float ShuffleSpeed = 1f;

    private Image ImageComponent;

    void Start()
    {
        ImageComponent = GetComponent<Image>();
        StartCoroutine(ColorShuffling());
    }

    private IEnumerator ColorShuffling()
    {
        Color color = ImageComponent.color;
        float element = 0;
        while (true)
        {
            // Increase red
            element = ImageComponent.color.r;
            while (element < 1)
            {
                element += Time.deltaTime * ShuffleSpeed;
                color.r = element;
                ImageComponent.color = color;
                yield return null;
            }
            // Decrease blue
            element = ImageComponent.color.b;
            while (element > 0)
            {
                element -= Time.deltaTime * ShuffleSpeed;
                color.b = element;
                ImageComponent.color = color;
                yield return null;
            }
            // Increase green
            element = ImageComponent.color.g;
            while (element < 1)
            {
                element += Time.deltaTime * ShuffleSpeed;
                color.g = element;
                ImageComponent.color = color;
                yield return null;
            }
            // Decrease red
            element = ImageComponent.color.r;
            while (element > 0)
            {
                element -= Time.deltaTime * ShuffleSpeed;
                color.r = element;
                ImageComponent.color = color;
                yield return null;
            }
            // Increase blue
            element = ImageComponent.color.b;
            while (element < 1)
            {
                element += Time.deltaTime * ShuffleSpeed;
                color.b = element;
                ImageComponent.color = color;
                yield return null;
            }
            // Decrease green
            element = ImageComponent.color.g;
            while (element > 0)
            {
                element -= Time.deltaTime * ShuffleSpeed;
                color.g = element;
                ImageComponent.color = color;
                yield return null;
            }
        }
    }
}
