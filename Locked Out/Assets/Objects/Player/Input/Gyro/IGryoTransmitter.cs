using UnityEngine;

public interface IGyroTransmitter
{
    public bool Setup();

    public Quaternion GetOrientation();
}