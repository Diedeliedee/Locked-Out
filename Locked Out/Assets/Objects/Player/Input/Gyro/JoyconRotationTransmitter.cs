using Joeri.Tools.Movement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoyconRotationTransmitter : IGyroTransmitter
{
    private List<Joycon> m_joycons;

    public bool Setup()
    {
        //  Check if the joyconmanager is in the scene.
        if (JoyconManager.Instance == null)
        {
            Debug.LogError("No Joycon Manager found in scene.. JESSEEEEEEEEE!111");
            return false;
        }

        //  Check if any m_joycons have been found.
        if (JoyconManager.Instance.j == null || JoyconManager.Instance.j.Count <= 0)
        {
            Debug.LogWarning("No joycons found! JESSEEE NO JOYCONS FOUUUNDD!!11");
            return false;
        }

        m_joycons = JoyconManager.Instance.j;

        //  If the joycon is still null somehow, give up.
        if (m_joycons == null || m_joycons.Count <= 0)
        {
            Debug.LogWarning("Joycon is somehow still not found.  :(.");
        }

        return true;
    }

    public Quaternion GetOrientation()
    {
        //  Make sure the Joycon only gets checked if attached.
        if (m_joycons == null || m_joycons.Count <= 0)
        {
            Debug.LogWarning("Joycon lost connection!");
            return Quaternion.identity;
        }

        var orientation = m_joycons[0].GetVector();

        Quaternion newOrientation = new Quaternion();

        newOrientation.w = orientation.w;
        newOrientation.x = -orientation.y;
        newOrientation.y = -orientation.z;
        newOrientation.z = orientation.x;

        return newOrientation;
    }
}
