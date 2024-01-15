using UnityEngine;

public class RotationPuzzle : Puzzle
{
    [SerializeField] private float rotationTolerance = 10f;
    [Space]
    [SerializeField] private RotationCube m_rotationCube = null;
    [SerializeField] private Transform m_guide = null;

    private void Start()
    {
        m_rotationCube.onRotated += OnCubeRotated;
    }

    private void OnCubeRotated(Quaternion _rotation)
    {
        if (!CompareRotation(_rotation, m_guide.localRotation, rotationTolerance)) return;

        m_onSolved.Invoke();
        print("SOLVED LE PUZZLE");
        m_rotationCube.transform.localRotation = m_guide.localRotation;
        m_rotationCube.onRotated -= OnCubeRotated;
    }

    bool CompareRotation(Quaternion a, Quaternion b, float tolerance)
    {
        var angle = Quaternion.Angle(a, b);

        return angle <= tolerance;
    }
}
