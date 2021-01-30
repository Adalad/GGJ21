using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(SpriteRenderer))]
public class BoatCatCollector : MonoBehaviour
{
    [Range(0f, 100f)]
    public float ShuffleSpeed = 1f;
    public bool Anchored;

    private SpriteRenderer SpriteRendererComponent;
    private bool CatGrabbed;
    private CatFollow FollowingCat;
    private Coroutine ShuffleRoutine;

    private void Start()
    {
        SpriteRendererComponent = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (Input.GetButton("Fire1") && CatGrabbed && !Anchored)
        {
            if (EventSystem.current.IsPointerOverGameObject())
            {
                return;
            }

            CatGrabbed = false;
            FollowingCat.ReleaseGrab();
            StopCoroutine(ShuffleRoutine);
            SpriteRendererComponent.color = Color.white;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Cat"))
        {
            CatGrabbed = true;
            FollowingCat = collision.GetComponent<CatFollow>();
            if (FollowingCat.GetGrabbed(gameObject.transform))
            {
                ShuffleRoutine = StartCoroutine(ColorShuffling());
            }
        }
    }

    private IEnumerator ColorShuffling()
    {
        Color color = SpriteRendererComponent.color;
        float element = 0;
        while (true)
        {
            // Increase red
            element = SpriteRendererComponent.color.r;
            while (element < 1)
            {
                element += Time.deltaTime * ShuffleSpeed;
                color.r = element;
                SpriteRendererComponent.color = color;
                yield return null;
            }
            // Decrease blue
            element = SpriteRendererComponent.color.b;
            while (element > 0)
            {
                element -= Time.deltaTime * ShuffleSpeed;
                color.b = element;
                SpriteRendererComponent.color = color;
                yield return null;
            }
            // Increase green
            element = SpriteRendererComponent.color.g;
            while (element < 1)
            {
                element += Time.deltaTime * ShuffleSpeed;
                color.g = element;
                SpriteRendererComponent.color = color;
                yield return null;
            }
            // Decrease red
            element = SpriteRendererComponent.color.r;
            while (element > 0)
            {
                element -= Time.deltaTime * ShuffleSpeed;
                color.r = element;
                SpriteRendererComponent.color = color;
                yield return null;
            }
            // Increase blue
            element = SpriteRendererComponent.color.b;
            while (element < 1)
            {
                element += Time.deltaTime * ShuffleSpeed;
                color.b = element;
                SpriteRendererComponent.color = color;
                yield return null;
            }
            // Decrease green
            element = SpriteRendererComponent.color.g;
            while (element > 0)
            {
                element -= Time.deltaTime * ShuffleSpeed;
                color.g = element;
                SpriteRendererComponent.color = color;
                yield return null;
            }
        }
    }
}
