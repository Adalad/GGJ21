using UnityEngine;

public class BoatCatCollector : MonoBehaviour
{
    private bool CatGrabbed;
    private CatFollow FollowingCat;

    private void Update()
    {
        if (Input.GetButton("Fire1") && CatGrabbed)
        {
            CatGrabbed = false;
            FollowingCat.ReleaseGrab();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Cat"))
        {
            CatGrabbed = true;
            FollowingCat = collision.GetComponent<CatFollow>();
            FollowingCat.GetGrabbed(gameObject.transform);
        }
    }
}
