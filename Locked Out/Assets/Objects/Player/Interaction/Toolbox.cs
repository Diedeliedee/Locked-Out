using UnityEngine;

[System.Serializable]
public class Toolbox
{
    [SerializeField] private GrabHandler m_grabbing = null;
    //private CutHandler m_cutting = null;

    public void CrossReference(IHoverable _hoverable)
    {
        if (Input.GetKeyDown(KeyCode.G) && _hoverable is IGrabbable _grabbable) m_grabbing.TryGrab(_grabbable); 
        //if (Input.GetKeyDown(KeyCode.C) && _hoverable is ICuttable _cuttable) m_grabbing.TryGrab(_grabbable);
    }

    public void TrySecondaryActions()
    {
        if (Input.GetKeyDown(KeyCode.G)) m_grabbing.TryRelease();
    }
}
