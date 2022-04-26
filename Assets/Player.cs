using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] Camera kamera;
    [SerializeField] float SPEED = 10f;
    Vector3 move;
    CharacterController characterController;
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        move.x = Input.GetAxis("Horizontal");
        move.z = Input.GetAxis("Vertical");

        if ( !characterController.isGrounded)
            move.y -= 9.8f * Time.deltaTime;
        else
            move.y = 0f;

        move = transform.TransformDirection(move);
        characterController.Move(move * Time.deltaTime * SPEED);
    }
}
