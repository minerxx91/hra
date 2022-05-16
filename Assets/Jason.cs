using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jason : MonoBehaviour
{
    CharacterController characterController;
    [SerializeField] float SPEED = 10f;
    Vector3 move;
    Vector2 target;
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        target = new Vector2(Random.Range(0f, 1000f), Random.Range(0f, 1000f));
    }

    void Update()
    {
        move = new Vector3(Random.Range(0f,1f), 0f, Random.Range(0f, 1f));

        if (!characterController.isGrounded)
        {
            move.y -= 9.8f;
        }

        characterController.Move(SPEED * Time.deltaTime * move);
    }
}
