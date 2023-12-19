using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float m_moveSpeed = 1f;
    [SerializeField] private float m_turnSpeed = 1f;

    private CharacterController m_characterController;

    private void Awake()
    {
        m_characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        var moveInput = Input.GetAxisRaw("Vertical");
        var rotationInput = Input.GetAxisRaw("Horizontal");

        m_characterController.Move(transform.forward * (moveInput * m_moveSpeed * Time.deltaTime));
        transform.Rotate(0f, rotationInput * m_turnSpeed * Time.deltaTime, 0f, Space.Self);
    }
}
