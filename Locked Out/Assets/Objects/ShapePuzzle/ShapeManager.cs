using UnityEngine;
using UnityEngine.Events;

public class ShapeManager : Puzzle
{
    [SerializeField] private UnityEvent m_onGuessedWrong = null;
    [Space]
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
        //  Check every slot. If every
        for (int i = 0; i < m_slots.Length; i++)
        {
            //  If not every slot has a piece, return.
            if (!m_slots[i].hasPiece) return;

            //  If all slots are filled, but it is incorrect, call the mistake event.
            if (!m_slots[i].isCorrect)
            {
                m_onGuessedWrong.Invoke();
                return;
            }
        }

        //  Otherwise, the puzzle is solved.
        m_onSolved.Invoke();
        for (int i = 0; i < m_slots.Length; i++) m_slots[i].onPlace.RemoveListener(OnPieceInserted);
    }
}
