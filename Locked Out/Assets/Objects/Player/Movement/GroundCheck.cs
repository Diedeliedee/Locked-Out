using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    [SerializeField] private LayerMask m_mask = default;
    [SerializeField] private CharacterController m_collider = null;

    public bool IsOnGround()
    {
        var position = GetSphereOrigin();
        var overlappingColliders = Physics.OverlapSphere(position, m_collider.radius, m_mask, QueryTriggerInteraction.Ignore);

        if (overlappingColliders.Length > 0) return true;
        return false;
    }

    private Vector3 GetSphereOrigin()
    {
        var pos = m_collider.transform.position;

        pos.y += m_collider.radius;
        pos.y -= m_collider.skinWidth * 2;
        return pos;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = IsOnGround() ? Color.green : Color.white;
        Gizmos.DrawWireSphere(GetSphereOrigin(), m_collider.radius);
    }
}