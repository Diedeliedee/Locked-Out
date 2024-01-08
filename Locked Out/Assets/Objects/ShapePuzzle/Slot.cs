using UnityEngine;
using UnityEngine.Events;

public class Slot : HighlightInteractable
{
    public UnityEvent<Slot> onSelect = null;
    [HideInInspector] public int index = 0;

    private Piece m_insertedPiece = null;

    public bool hasPiece => m_insertedPiece != null;
    public bool correct => hasPiece && m_insertedPiece.index == index;

    public override void OnInteract()
    {
        onSelect.Invoke(this);
    }

    public Piece InsertPiece(Piece _piece)
    {
        var returningPiece = RemovePiece();

        gameObject.SetActive(false);
        m_insertedPiece = _piece;
        _piece.gameObject.SetActive(true);
        _piece.transform.position = transform.position;
        _piece.transform.rotation = transform.rotation;
        return returningPiece;
    }

    public Piece RemovePiece()
    {
        if (m_insertedPiece == null) return null;

        gameObject.SetActive(true);
        var piece = m_insertedPiece;
        return piece;
    }

}
