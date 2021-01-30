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
    [Range(0.0f, 1000)]
    public float LiftForce = 5.0f;
    // Anchor distance
    [Range(0.0f, 100)]
    public float AnchorDistance = 5.0f;

    private Rigidbody2D RigidBodyComponent;
    private SpriteRenderer SpriteComponent;
    private Vector2 LookDirection;
    private float LookAngle;

    // Planets
    private Transform[] PlanetTransforms;
    private Transform AnchoredPlanetTransform;
    private Transform LastPlanetTransform;

    void Start()
    {
        RigidBodyComponent = GetComponent<Rigidbody2D>();
        SpriteComponent = GetComponent<SpriteRenderer>();
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
            int direction = (AnchoredPlanetTransform.GetComponent<PlanetRotation>().RotationSpeed > 0) ? -1 : 1;
            LastPlanetTransform = AnchoredPlanetTransform;
            AnchoredPlanetTransform = null;
            RigidBodyComponent.isKinematic = false;
            transform.parent = null;
            RigidBodyComponent.AddForce(transform.right.normalized * LiftForce * direction);

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

                    if (dist < MaxGravityDistance)
                    {
                        // Gravity
                        Vector3 v = PlanetTransforms[i].position - transform.position;
                        RigidBodyComponent.AddForce(v.normalized * (1.0f - dist / MaxGravityDistance) * MaxGravity);

                        // Rotating to the planet
                        LookDirection = PlanetTransforms[i].position - transform.position;
                        LookAngle = 90 + Mathf.Atan2(LookDirection.y, LookDirection.x) * Mathf.Rad2Deg;
                        float lerpRatio = 1 - ((dist - AnchorDistance) / (MaxGravityDistance - AnchorDistance));
                        Vector3 newRotation = transform.eulerAngles;
                        newRotation.z = LookAngle * lerpRatio;
                        transform.eulerAngles = newRotation;

                        if (dist <= AnchorDistance)
                        {
                            AnchoredPlanetTransform = PlanetTransforms[i];
                            if (AnchoredPlanetTransform.GetComponent<PlanetRotation>().RotationSpeed > 0)
                            {
                                SpriteComponent.flipX = true;
                            }
                            else
                            {
                                SpriteComponent.flipX = false;
                            }

                            RigidBodyComponent.isKinematic = true;
                            RigidBodyComponent.velocity = Vector2.zero;
                            transform.parent = AnchoredPlanetTransform;
                            break;
                        }
                    }
                }
            }

            yield return null;
        }
    }
}