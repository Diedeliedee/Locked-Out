using UnityEngine;

public class ShapeManager : Puzzle
{
    [SerializeField] private Piece[] m_pieces;
    [SerializeField] private Slot[] m_slots;

    private Piece m_holdingPiece = null;

    private void Awake()
    {
        //  Register corresponding select events.
        for (int i = 0; i < m_pieces.Length; i++)
        {
            m_pieces[i].index = i;
            m_pieces[i].onSelect.AddListener(OnPieceSelected);
        }
        for (int i = 0; i < m_slots.Length; i++)
        {
            m_pieces[i].index = i;
            m_slots[i].onSelect.AddListener(OnSlotSelected);
        }
    }

    private void OnPieceSelected(Piece _piece)
    {
        //  Deactivate the piece, and grab it.
        _piece.transform.gameObject.SetActive(false);
        _piece.onSelect.RemoveListener(OnPieceSelected);
        m_holdingPiece = _piece;
    }

    private void OnSlotSelected(Slot _slot)
    {
        //  We don't need to do anything if we don't hold a piece, or the selected slot already has one.
        if (m_holdingPiece == null || _slot.hasPiece) return;

        //  Insert the holding piece.
        m_holdingPiece = _slot.InsertPiece(m_holdingPiece);

        //  Check every slot. If every slot has the correct piece, the puzzle is solved.
        for (int i = 0; i < m_slots.Length; i++) if (!m_slots[i].correct) return;
        m_onSolved.Invoke();
    }
}
