using System.Collections;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class CatFollow : MonoBehaviour
{
    public float Velocity = 1f;
    public float Distance = 1;

    private CircleCollider2D ColliderComponent;
    private bool Anchored;
    private Transform Boat;
    private Vector3 NewPosition;
    private Coroutine FollowRoutine;

    private void Start()
    {
        ColliderComponent = GetComponent<CircleCollider2D>();
    }

    public bool GetGrabbed(Transform boat)
    {
        if (Anchored)
        {
            return false;
        }

        ColliderComponent.enabled = false;
        Boat = boat;
        FollowRoutine = StartCoroutine(FollowBoat());
        return true;
    }

    public void ReleaseGrab()
    {
        Boat = null;
        StopCoroutine(FollowRoutine);
        if (CatLocationchecker.CheckLocation(transform.position))
        {
            transform.GetChild(0).gameObject.SetActive(true);
            Anchored = true;
        }
        else
        {
            ColliderComponent.enabled = true;
        }
    }

    private IEnumerator FollowBoat()
    {
        while (true)
        {
            if (Vector3.Distance(transform.position, Boat.position) > Distance)
            {
                NewPosition = Boat.position + (Boat.position - transform.position).normalized;
                NewPosition = Vector3.Lerp(transform.position, NewPosition, Time.deltaTime * Velocity);
                transform.position = NewPosition;
            }

            yield return null;
        }
    }
}
