using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Wire : MonoBehaviour
{
    [Tooltip("The color of this wire. Techical and important in determining whether the puzzle is solved.")]
    public Color color;
    [Tooltip("The cosmetic color of this wire. Determines which color the screen turns into.")]
    public UnityEngine.Color cosmeticColor;
    [Tooltip("Event called when the wire is cut. The wire manager automatically subscribes to this, so anything added here is purely cosmetic.")]
    public UnityEvent<Wire> onCut = null;

    private void OnMouseDown()
    {
        onCut?.Invoke(this);
    }

    //  Enum for comparing or registering wire colors. Can be altered later down development.
    public enum Color
    {
        Red,
        Green,
        Pink,
        Blue,
        Yellow
    }
}
