using UnityEngine;

public class RotationComparison : Puzzle
{
    [SerializeField] private Transform targetObject;
    [SerializeField] private float rotationTolerance = 10f;

    void Update()
    {
        if (CompareRotation(transform.rotation, targetObject.rotation, rotationTolerance))
        {
            print("Rotations are similar!");
            m_onSolved.Invoke();
            // Uhh destroy script instance or something idk
            Destroy(this);
        }
    }

    bool CompareRotation(Quaternion a, Quaternion b, float tolerance)
    {
        var angle = Quaternion.Angle(a, b);

        return angle <= tolerance;
    }
}