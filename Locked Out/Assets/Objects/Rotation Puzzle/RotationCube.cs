using System.Collections.Generic;
using UnityEngine;

public class RotationCube : HighlightHoverable, IGrabbable
{
    private List<Joycon> joycons;

    public float[] stick;
    public Vector3 gyro = default;
    public Vector3 accel = default;
    public Quaternion orientation = default;
    public int jc_ind = 0;

    public System.Action<Quaternion> onRotated = null;

    private bool m_grabbed = false;

    private void Update()
    {
        if (!m_grabbed) return;

        var rotation = GetOrientation();

        transform.localRotation = rotation;
        onRotated?.Invoke(rotation);
    }

    public void Grab(Transform _origin)
    {
        m_grabbed = true;
    }

    public void Release()
    {
        m_grabbed = false;
    }


    private void Awake()
    {
        //  Get the public Joycon array attached to the JoyconManager in scene
        joycons = JoyconManager.Instance.j;
        if (joycons.Count < jc_ind + 1)
        {
            Debug.LogWarning("No joycons found! JESSEEE NO JOYCONS FOUUUNDD!!11");
        }
    }

    // Update is called once per frame
    public Quaternion GetOrientation()
    {
        // make sure the Joycon only gets checked if attached
        if (joycons.Count <= 0) return Quaternion.identity;

        var j = joycons[jc_ind];

        stick = j.GetStick();
        gyro = j.GetGyro();
        accel = j.GetAccel();
        orientation = j.GetVector();

        var newOrientation = new Quaternion
        {
            w = orientation.w,
            x = -orientation.y,
            y = -orientation.z,
            z = orientation.x
        };

        return newOrientation;
    }
}
