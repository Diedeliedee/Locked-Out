using UnityEngine;
using UnityEngine.Events;

public class Piece : MonoBehaviour
{
    public UnityEvent<Piece> onSelect = null;

    [HideInInspector] public int index = 0;

    private void OnMouseDown()
    {
        onSelect.Invoke(this);
    }
}
