using UnityEngine;

public class RotationComparison : MonoBehaviour
{
    [SerializeField] private Transform targetObject;
    [SerializeField] private float rotationTolerance = 10f;

    void Update()
    {
        if (CompareRotation(transform.rotation, targetObject.rotation, rotationTolerance))
        {
            print("Rotations are similar!");
            // Test
            Destroy(gameObject); 
            // Logic here
        }
    }

    bool CompareRotation(Quaternion a, Quaternion b, float tolerance)
    {
        var angle = Quaternion.Angle(a, b);

        return angle <= tolerance;
    }
}