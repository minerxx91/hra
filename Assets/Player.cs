using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] Camera kamera;
    [SerializeField] float SPEED = 10f;
    [SerializeField] float ROTATIONSPEED = 300f;
    [SerializeField] Image mask;
    float fill = 1;
    bool canRun;
    Vector3 move;
    Vector3 kameraRotation;
    Vector3 rotate;
    Animator animator;
    CharacterController characterController;
    void Start()
    {
        animator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        move.x = Input.GetAxis("Horizontal");
        move.z = Input.GetAxis("Vertical");

        float x = Input.GetAxis("Mouse Y");
        float y = Input.GetAxis("Mouse X");

        kameraRotation = new Vector3(x, 0, 0);
        rotate = new Vector3(0, y, 0);
        transform.Rotate(rotate * Time.deltaTime * ROTATIONSPEED);
        if (kamera.transform.eulerAngles.x > 45f && kamera.transform.eulerAngles.x < 270f)
        {
            kameraRotation = new Vector3(kamera.transform.eulerAngles.x - 45f, 0, 0);
        }

        kamera.transform.eulerAngles -= kameraRotation;

        if(fill > 0.3f && Input.GetKeyDown(KeyCode.LeftShift)) canRun = true;
        else if(fill == 0) canRun = false;

        if (Input.GetKey(KeyCode.LeftShift) && canRun)
        {
            animator.SetBool("isRunning", true);
            SPEED = 10f;
            fill -= Time.deltaTime/10;
            if (fill < 0f) fill = 0f;
        }
        else
        {
            animator.SetBool("isRunning", false);
            SPEED = 5f;
            fill += Time.deltaTime/5;
            if (fill > 1f) fill = 1f;
        }
        mask.fillAmount = fill;

        move = transform.TransformDirection(move);
        characterController.Move(SPEED * Time.deltaTime * move);
        
        if (transform.position.x > 499f)
        {
            transform.position = new Vector3(499f, transform.position.y, transform.position.z);
        }
        else if(transform.position.x < -499f)
        {
            transform.position = new Vector3(-499f, transform.position.y, transform.position.z);
        }
        if (transform.position.z < 1f)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, 1f);
        }
        else if (transform.position.z > 999f)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, 999f);
        }

        if (!characterController.isGrounded)
            move.y -= 9.8f * Time.deltaTime;
        else
            move.y = 0f;

        if (move.x == 0 && move.z == 0)
        {
            animator.SetBool("isWalking", false);
        }
        else animator.SetBool("isWalking", true);

    }
}
