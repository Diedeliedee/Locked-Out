using UnityEngine;
using UnityEngine.Events;

public class Slot : HighlightHoverable, ISlot
{
    public UnityEvent<Slot> onPlace = null;

    private int m_index = 0;
    private Piece m_insertedPiece = null;

    public bool isCorrect => m_insertedPiece != null && m_insertedPiece.index == m_index;

    public void Register(int _index)
    {
        m_index = _index;
    }

    public bool TryPlace(IGrabbable _item)
    {
        if (m_insertedPiece != null) return false;
        if (_item is not Piece _piece) return false;

        gameObject.SetActive(false);
        m_insertedPiece = _piece;
        _piece.gameObject.SetActive(true);
        _piece.transform.SetPositionAndRotation(transform.position, transform.rotation);
        _piece.onGrab.AddListener(ResetSlot);
        onPlace.Invoke(this);
        return true;
    }

    public void ResetSlot()
    {
        if (m_insertedPiece == null) return;

        m_insertedPiece.onGrab.RemoveListener(ResetSlot);
        m_insertedPiece = null;
        gameObject.SetActive(true);
    }
}
