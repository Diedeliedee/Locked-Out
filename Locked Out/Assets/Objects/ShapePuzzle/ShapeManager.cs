using UnityEngine;

public class ShapeManager : Puzzle
{
    [SerializeField] private Piece[] m_pieces;
    [SerializeField] private Slot[] m_slots;

    private void Awake()
    {
        if (m_pieces == null || m_pieces.Length == 0)
        {
            Debug.LogWarning("Corresponding pieces not assigned in the inspector!!!", gameObject);
            return;
        }

        //  Register the indexes of the pieces and slots.
        for (int i = 0; i < m_pieces.Length; i++)
        {
            m_pieces[i].index = i;
        }
        for (int i = 0; i < m_slots.Length; i++)
        {
            m_slots[i].Register(i);
            m_slots[i].onPlace.AddListener(OnPieceInserted);
        }
    }

    private void OnPieceInserted(Slot _slot)
    {
        //  Check every slot. If every slot has the isCorrect piece, the puzzle is solved.
        for (int i = 0; i < m_slots.Length; i++) if (!m_slots[i].isCorrect) return;
        m_onSolved.Invoke();
    }
}
