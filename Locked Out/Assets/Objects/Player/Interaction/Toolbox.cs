using UnityEngine;

[System.Serializable]
public class Toolbox
{
    [SerializeField] private GrabHandler m_grabbing = null;
    //private CutHandler m_cutting = null;

    public void Tick(float _deltaTime)
    {
        m_grabbing.Tick(_deltaTime);
    }

    public void CrossReference(IHoverable _hoverable)
    {
        // Quick and dirty method of discerning which type of interaction is needed for which object we're interacting with.

        //  If it is a standard interactable, just interact with it.
        if (_hoverable is IInteractable _interactable)
        {
            _interactable.Interact();
            return;
        }

        //  Try placing an item we're holding in the slot if the hoverable is a slot.
        if (_hoverable is ISlot _slot)
        {
            m_grabbing.TryInsert(_slot);
            return;
        }

        //  Try picking up the object if it's a grabbable.
        if (_hoverable is IGrabbable _grabbable)
        {
            m_grabbing.TryGrab(_grabbable);
            return;
        }

        //  Run the cut behavior if it's cuttable.
        if (_hoverable is ICuttable _cuttable)
        {
            _cuttable.Cut();
            return;
        }
    }

    /// <summary>
    /// Function called when no hoverable is detected, but the interaction button is still pressed.
    /// </summary>
    public void TrySecondaryActions()
    {
        m_grabbing.TryRelease();
    }
}
