using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Door : MonoBehaviour
{
    public LayerMask interactableLayerMask = 8;
    [SerializeField] GameObject[] pivots;
    [SerializeField] List<Animator> animators = new List<Animator>();
    void Start()
    {
        for (int i = 0; i < pivots.Length; i++)
        {
            animators.Add(pivots[i].GetComponent<Animator>());
            animators[i].enabled = false;
        }
    }

    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, 50f, interactableLayerMask))
        {
            if (Input.GetMouseButtonDown(0))
            {
                for (int i = 0; i < pivots.Length; i++)
                {
                    if (hit.collider.gameObject.transform.parent.name == pivots[i].name)
                    {
                        animators[i].enabled = true;
                        if (animators[i].GetBool("isClosed"))
                        {
                            animators[i].SetBool("isOpen", true);
                            animators[i].SetBool("isClosed", false);
                        }
                        else
                        {
                            animators[i].SetBool("isClosed", true);
                            animators[i].SetBool("isOpen", false);
                        }
                    }
                }
            }
        }
    }
}
