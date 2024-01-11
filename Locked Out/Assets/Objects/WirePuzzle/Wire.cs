using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Wire : HighlightHoverable, ICuttable
{
    [Tooltip("The color of this wire. Techical and important in determining whether the puzzle is solved.")]
    public WireColor color;
    [Tooltip("Event called when the wire is cut. The wire manager automatically subscribes to this, so anything added here is purely cosmetic.")]
    public UnityEvent<Wire> onCut = null;

    public void Cut()
    {
        onCut?.Invoke(this);
    }
}

//  Enum for comparing or registering wire colors. Can be altered later down development.
public enum WireColor
{
    Red,
    Green,
    Blue,
    Yellow,
}
