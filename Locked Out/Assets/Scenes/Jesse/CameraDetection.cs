using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Light))]
public class CameraDetection : MonoBehaviour
{

    [SerializeField] private LayerMask m_mask = default;
    [SerializeField] private int m_accuracy = 10;
    [SerializeField] private bool m_drawRays = false;

    private Light m_light = null;

    private void Awake()
    {
        m_light = GetComponent<Light>();
    }

    private void Update()
    {
        PlayerDetected();
    }

    private bool PlayerDetected()
    {
        var originalRotation = transform.localRotation;
        var offset = m_light.spotAngle / 2f;

        for (int i = 0; i < m_accuracy; i++)
        {
            var randomOffset = RandomOffset() * offset;

            //  Apply random offset to euler angles of camera.
            transform.Rotate(randomOffset.x, randomOffset.y, 0f, Space.Self);

            //  Cast a ray, and continue to the next iteration if neither anything has been hit, or whatever's been hit isn't a player.
            if (!Physics.Raycast(transform.position, transform.forward, out RaycastHit _hit, m_light.range, m_mask))
            {
                if (m_drawRays) Debug.DrawRay(transform.position, transform.forward * m_light.range);
                transform.localRotation = originalRotation;
                continue;
            }

            //  Draw the hit ray (optional), and reset rotation.
            if (m_drawRays) Debug.DrawLine(transform.position, _hit.point);
            transform.localRotation = originalRotation;

            //  Return true if the player has been found.
            if (_hit.transform.TryGetComponent(out PlayerMovement _player)) return true;
        }
        return false;
    }

    private Vector2 RandomOffset()
    {
        var t = 2 * Mathf.PI * Random.Range(0f, 1f);
        var u = Random.Range(0f, 1f) * Random.Range(0f, 1f);
        var r = u > 1 ? 1 : u;

        return new Vector2(r * Mathf.Cos(t), r * Mathf.Sin(t));
    }
}
