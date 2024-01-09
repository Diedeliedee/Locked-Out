using UnityEngine;
using UnityEngine.Events;

public class Piece : HighlightHoverable
{
    public UnityEvent<Piece> onSelect = null;

    [HideInInspector] public int index = 0;

    /*
    public override void OnInteract()
    {
        onSelect.Invoke(this);
        return true;
    }
    */
}
