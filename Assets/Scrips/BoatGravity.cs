using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class BoatGravity : MonoBehaviour
{
    [Header("Gravity")]
    // Distance where gravity works
    [Range(0.0f, 1000.0f)]
    public float MaxGravityDistance = 150.0f;
    // Gravity force
    [Range(0.0f, 100)]
    public float MaxGravity = 5.0f;
    // Lift force
    [Range(0.0f, 100)]
    public float LiftForce = 5.0f;
    // Anchor distance
    [Range(0.0f, 100)]
    public float AnchorDistance = 5.0f;

    private Rigidbody2D RigidBodyComponent;
    private Vector2 LookDirection;
    private float LookAngle;

    // Planets
    private Transform[] PlanetTransforms;
    private Transform AnchoredPlanetTransform;
    private Transform LastPlanetTransform;

    void Start()
    {
        RigidBodyComponent = GetComponent<Rigidbody2D>();
        GameObject[] planetGOs = GameObject.FindGameObjectsWithTag("Planet");
        PlanetTransforms = new Transform[planetGOs.Length];
        for (int i = 0; i < planetGOs.Length; ++i)
        {
            PlanetTransforms[i] = planetGOs[i].transform;
        }

        StartCoroutine(Gravitate());
    }

    private void LiftUp()
    {
        if (AnchoredPlanetTransform != null)
        {
            LastPlanetTransform = AnchoredPlanetTransform;
            AnchoredPlanetTransform = null;
            RigidBodyComponent.isKinematic = false;
            transform.parent = null;
            RigidBodyComponent.AddForce(transform.up.normalized * LiftForce);

            StartCoroutine(Gravitate());
        }
    }

    private IEnumerator Gravitate()
    {
        while (true)
        {
            for (int i = 0; i < PlanetTransforms.Length; ++i)
            {
                if (PlanetTransforms[i] != LastPlanetTransform)
                {
                    // Distance to the planet
                    float dist = Vector3.Distance(PlanetTransforms[i].position, transform.position);

                    // Gravity
                    Vector3 v = PlanetTransforms[i].position - transform.position;
                    RigidBodyComponent.AddForce(v.normalized * (1.0f - dist / MaxGravityDistance) * MaxGravity);

                    // Rotating to the planet
                    LookDirection = PlanetTransforms[i].position - transform.position;
                    LookAngle = Mathf.Atan2(LookDirection.y, LookDirection.x) * Mathf.Rad2Deg;

                    transform.rotation = Quaternion.Euler(0f, 0f, LookAngle);

                    if (dist <= AnchorDistance)
                    {
                        AnchoredPlanetTransform = PlanetTransforms[i];
                        RigidBodyComponent.isKinematic = true;
                        RigidBodyComponent.velocity = Vector2.zero;
                        transform.parent = AnchoredPlanetTransform;
                        break;
                    }
                }
            }

            yield return null;
        }
    }
}