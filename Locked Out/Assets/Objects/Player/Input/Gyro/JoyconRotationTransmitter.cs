using UnityEngine;

public class JoyconRotationTransmitter : IGyroTransmitter
{
    private Joycon m_joycon = null;

    public bool Setup()
    {
        //  Check if the joyconmanager is in the scene.
        if (JoyconManager.Instance == null)
        {
            Debug.LogError("No Joycon Manager found in scene.. JESSEEEEEEEEE!111");
            return false;
        }

        //  Check if any joycons have been found.
        if (JoyconManager.Instance.j.Count <= 0)
        {
            Debug.LogWarning("No joycons found! JESSEEE NO JOYCONS FOUUUNDD!!11");
            return false;
        }

        //  Cache the first joycon in the array.
        m_joycon = JoyconManager.Instance.j[0];

        //  If the joycon is still null somehow, give up.
        if (m_joycon == null)
        {
            Debug.LogWarning("Joycon is somehow still not found.  :(.");
        }

        return true;
    }

    public Quaternion GetOrientation()
    {
        //  Make sure the Joycon only gets checked if attached.
        if (m_joycon == null)
        {
            Debug.LogWarning("Joycon lost connection!");
            return Quaternion.identity;
        }

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
