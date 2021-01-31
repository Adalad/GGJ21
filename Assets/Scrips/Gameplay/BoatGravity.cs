using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Rigidbody2D), typeof(BoatGravity))]
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
    public AudioClip PlanetClip;
    public Camera Camera;
    [Range(0.0f, 100)]
    public float ZoomOutSpeed = 5.0f;
    [Range(0.0f, 100)]
    public float ZoomOutSize = 7.5f;
    [Range(0.0f, 100)]
    public float ZoomInSpeed = 5.0f;
    [Range(0.0f, 100)]
    public float ZoomInSize = 4.0f;

    public bool IsAnchored
    {
        get
        {
            return (AnchoredPlanetTransform != null);
        }
    }

    private AudioSource AudioSourceComponent;
    private BoatCatCollector CollectorComponent;
    private Rigidbody2D RigidBodyComponent;
    private Vector2 LookDirection;
    private float LookAngle;
    private Vector3 OriginalScale;
    private bool GravityExtra;

    // Planets
    private Transform[] PlanetTransforms;
    private Transform AnchoredPlanetTransform;
    private Transform LastPlanetTransform;

    private void Start()
    {
        OriginalScale = transform.GetChild(0).localScale;
        AudioSourceComponent = GetComponent<AudioSource>();
        CollectorComponent = GetComponent<BoatCatCollector>();
        RigidBodyComponent = GetComponent<Rigidbody2D>();
        GameObject[] planetGOs = GameObject.FindGameObjectsWithTag("Planet");
        PlanetTransforms = new Transform[planetGOs.Length];
        for (int i = 0; i < planetGOs.Length; ++i)
        {
            PlanetTransforms[i] = planetGOs[i].transform;
        }

        StartCoroutine(Gravitate());
    }

    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            if (EventSystem.current.IsPointerOverGameObject())
            {
                return;
            }


            if ((AnchoredPlanetTransform == null) && (!CollectorComponent.IsCarrying))
            {
                GravityExtra = true;
            }
            
            LiftUp();
        }

        if (Input.GetButtonUp("Fire1"))
        {
            if ((AnchoredPlanetTransform == null) && (!CollectorComponent.IsCarrying))
            {
                GravityExtra = false;
            }
        }
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
            AudioSourceComponent.clip = PlanetClip;
            AudioSourceComponent.Play();

            StartCoroutine(ZoomOut());
            StartCoroutine(Gravitate());
        }
    }

    private IEnumerator Gravitate()
    {
        while (true)
        {
            for (int i = 0; i < PlanetTransforms.Length; ++i)
            {
                // Distance to the planet
                float dist = Vector3.Distance(PlanetTransforms[i].position, transform.position);
                if (PlanetTransforms[i] != LastPlanetTransform)
                {

                    if (dist < MaxGravityDistance)
                    {
                        // Gravity
                        Vector3 v = PlanetTransforms[i].position - transform.position;
                        if (GravityExtra)
                        {
                            RigidBodyComponent.AddForce(v.normalized * (1.0f - dist / MaxGravityDistance) * MaxGravity * 2);
                        }
                        else
                        {
                            RigidBodyComponent.AddForce(v.normalized * (1.0f - dist / MaxGravityDistance) * MaxGravity);
                        }

                        // Rotating to the planet
                        LookDirection = PlanetTransforms[i].position - transform.position;
                        LookAngle = 90 + Mathf.Atan2(LookDirection.y, LookDirection.x) * Mathf.Rad2Deg;
                        float lerpRatio = 1 - ((dist - AnchorDistance) / (MaxGravityDistance - AnchorDistance));
                        Vector3 newRotation = transform.eulerAngles;
                        newRotation.z = LookAngle * lerpRatio;
                        transform.eulerAngles = newRotation;

                        if (dist <= AnchorDistance)
                        {
                            GravityExtra = false;
                            AnchoredPlanetTransform = PlanetTransforms[i];
                            CollectorComponent.Anchored = true;
                            if (AnchoredPlanetTransform.GetComponent<PlanetRotation>().RotationSpeed > 0)
                            {
                                transform.GetChild(0).localScale = new Vector3(-OriginalScale.x, OriginalScale.y, 1);
                            }
                            else
                            {
                                transform.GetChild(0).localScale = new Vector3(OriginalScale.x, OriginalScale.y, 1);
                            }

                            RigidBodyComponent.isKinematic = true;
                            RigidBodyComponent.velocity = Vector2.zero;
                            transform.parent = AnchoredPlanetTransform;
                            StartCoroutine(ZoomIn());
                            break;
                        }
                    }
                }
                else
                {
                    if (dist > MaxGravityDistance)
                    {
                        LastPlanetTransform = null;
                        CollectorComponent.Anchored = false;
                    }
                }
            }

            yield return null;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Finish"))
        {
            RigidBodyComponent.velocity = -RigidBodyComponent.velocity;
        }
    }

    private IEnumerator ZoomOut()
    {
        while (Camera.orthographicSize < ZoomOutSize)
        {
            Camera.orthographicSize += ZoomOutSpeed * Time.deltaTime;
            yield return null;
        }
    }

    private IEnumerator ZoomIn()
    {
        while (Camera.orthographicSize > ZoomInSize)
        {
            Camera.orthographicSize -= ZoomInSpeed * Time.deltaTime;
            yield return null;
        }
    }
}