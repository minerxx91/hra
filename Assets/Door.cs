using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Door : MonoBehaviour
{
    public LayerMask interactableLayerMask = 8;
    Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, 50f, interactableLayerMask))
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (hit.collider.gameObject.tag == "Door")
                {
                    if (animator.GetBool("isClosed"))
                    {
                        animator.SetBool("isOpen", true);
                        animator.SetBool("isClosed", false);
                    }
                    else
                    {
                        animator.SetBool("isClosed", true);
                        animator.SetBool("isOpen", false);
                    }
                }
            }
        }
    }
}
