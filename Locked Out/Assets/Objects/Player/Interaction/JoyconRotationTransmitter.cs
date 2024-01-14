using UnityEngine;

public class JoyconRotationTransmitter
{
    private Joycon m_joycon = null;

    public void Setup()
    {
        if (JoyconManager.Instance == null)
        {
            Debug.LogError("No Joycon Manager found in scene.. JESSEEEEEEEEE!111");
            return;
        }

        //  Check if any joycons have been found, and add them to the list.
        m_joycon = JoyconManager.Instance.j[0];
        if (m_joycon == null)
        {
            Debug.LogWarning("No joycons found! JESSEEE NO JOYCONS FOUUUNDD!!11");
        }
    }

    public Quaternion GetOrientation()
    {
        //  Make sure the Joycon only gets checked if attached.
        if (m_joycon == null) return Quaternion.identity;

        //  Get the joycon orientation.
        var orientation = m_joycon.GetVector();

        //  Modify it to match Unity's dimensions.
        return new Quaternion
        {
            w = orientation.w,
            x = -orientation.y,
            y = -orientation.z,
            z = orientation.x
        };
    }
}
