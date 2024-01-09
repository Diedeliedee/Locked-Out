using UnityEngine;

[System.Serializable]
public class Toolbox
{
    [SerializeField] private GrabHandler m_grabbing = null;
    //private CutHandler m_cutting = null;

    public void CrossReference(IHoverable _hoverable)
    {
        if (Input.GetKeyDown(KeyCode.G) && _hoverable is IGrabbable _grabbable) m_grabbing.Grab(_grabbable); 
        //if (Input.GetKeyDown(KeyCode.C) && _hoverable is ICuttable _cuttable) m_grabbing.Grab(_grabbable);
    }
}
