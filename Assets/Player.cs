using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class Player : MonoBehaviour
{
    [SerializeField] Camera kamera;
    [SerializeField] float SPEED = 10f;
    [SerializeField] float ROTATIONSPEED = 300f;
    [SerializeField] Image mask;
    [SerializeField] Manager manager;
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
        if (!PauseMenu.GameIsPause)
        {
            move.x = Input.GetAxis("Horizontal");
            move.z = Input.GetAxis("Vertical");

            float x = Input.GetAxis("Mouse Y");
            float y = Input.GetAxis("Mouse X");

            
            if (characterController.enabled)
            {
                kameraRotation = new Vector3(x, 0f, 0f);
                rotate = new Vector3(0, y, 0);
                transform.Rotate(rotate * Time.deltaTime * ROTATIONSPEED);
                if (kamera.transform.eulerAngles.x > 45f && kamera.transform.eulerAngles.x < 270f)
                {
                    kameraRotation = new Vector3(kamera.transform.eulerAngles.x - 45f, 0, 0);
                }
                kamera.transform.eulerAngles -= kameraRotation;
            }

            Cursor.lockState = CursorLockMode.Locked;
        }


        if (fill > 0.3f && Input.GetKey(KeyCode.LeftShift)) canRun = true;
        else if (fill == 0) canRun = false;

        if (Input.GetKey(KeyCode.S))
        {
            canRun = false;
        }

        if (Input.GetKey(KeyCode.LeftShift) && canRun && move.z > 0f)
        {
            animator.SetBool("isRunning", true);
            SPEED = 10f;
            fill -= Time.deltaTime / 10;
            if (fill < 0f) fill = 0f;
        }
        else
        {
            animator.SetBool("isRunning", false);
            SPEED = 5f;
            fill += Time.deltaTime / 5;
            if (fill > 1f) fill = 1f;
        }
        mask.fillAmount = fill;

        move = transform.TransformDirection(move).normalized;
        characterController.Move(SPEED * Time.deltaTime * move);

        if (!characterController.isGrounded)
            move.y -= 9.8f * Time.deltaTime;
        else
            move.y = 0f;

        if (move.x == 0 && move.z == 0)
        {
            animator.SetBool("isWalking", false);
        }
        else
        {
            animator.SetBool("isWalking", true);
        }

        if (animator.GetBool("isWalking"))
        {
            manager.clip = (AudioClip)AssetDatabase.LoadAssetAtPath("Assets/Audio/steps.wav", typeof(AudioClip));
            manager.source.pitch = 1f;
            if (!manager.source.isPlaying)
            {
                manager.source.PlayOneShot(manager.clip);
            }
        }

        if (!animator.GetBool("isWalking"))
        {
            manager.source.Stop();
        }

        if (animator.GetBool("isRunning"))
        {
            manager.clip = (AudioClip)AssetDatabase.LoadAssetAtPath("Assets/Audio/steps.wav", typeof(AudioClip));
            manager.source.pitch = 2f;
            if (!manager.source.isPlaying)
            {
                manager.source.PlayOneShot(manager.clip);
            }
        }
    }
}
