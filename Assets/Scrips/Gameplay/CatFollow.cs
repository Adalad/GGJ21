using System.Collections;
using UnityEngine;

public class CatFollow : MonoBehaviour
{
    public float Velocity = 1f;
    public float Distance = 1;

    private Transform Boat;
    private Vector3 NewPosition;
    private Coroutine FollowRoutine;

    public void GetGrabbed(Transform boat)
    {
        Boat = boat;
        FollowRoutine = StartCoroutine(FollowBoat());
    }

    public void ReleaseGrab()
    {
        Boat = null;
        StopCoroutine(FollowRoutine);
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
