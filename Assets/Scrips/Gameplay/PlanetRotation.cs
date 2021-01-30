using UnityEngine;

public class PlanetRotation : MonoBehaviour
{
    // Rotation speed
    [Range(-1000.0f, 1000.0f)]
    public float RotationSpeed = 150.0f;

    void Update()
    {
        transform.Rotate(0, 0, RotationSpeed * Time.deltaTime);
    }
}
