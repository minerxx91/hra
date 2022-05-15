using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interactor : MonoBehaviour
{
    public LayerMask interactableLayerMask = 8;
    UnityEvent onInteract;
    Hotbar hotbar;
    public GameObject[] items = {};
    //public Dictionary<int, GameObject> items = new Dictionary<int ,GameObject>(3);
    [SerializeField] GameObject hotbarUI;
    void Start()
    {
        hotbar = hotbarUI.GetComponent<Hotbar>();
    }

    void Update()
    {
        RaycastHit hit;

        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, 50f , interactableLayerMask))
        {
            if (hit.collider.GetComponent<Interactable>())
            {
                onInteract = hit.collider.GetComponent<Interactable>().onInteract;
                if (Input.GetMouseButton(0))
                {
                    try
                    {
                        if (hit.collider.gameObject.name == "Pick-Up old" && items[hotbar.activeSlot].tag == "Jerry Can")
                        {
                            hit.collider.gameObject.SetActive(false);
                            onInteract.Invoke();
                        }
                    }
                    catch (Exception) { };
                    if(hit.collider.gameObject.tag == "Jerry Can")
                    {
                        onInteract.Invoke();
                        hit.collider.gameObject.transform.localPosition = new Vector3(0f, 1.22f, 0.29f);
                        hit.collider.gameObject.transform.localRotation = Quaternion.Euler(180f, 0f, 0f);
                    }
                    else if (hit.collider.gameObject.tag == "Spray")
                    {
                        onInteract.Invoke();
                        hit.collider.gameObject.transform.localPosition = new Vector3(0f, -3.356f, -10.303f);
                        hit.collider.gameObject.transform.localRotation = Quaternion.Euler(180f, 0f, 0f);
                    }
                    else if (hit.collider.gameObject.tag == "Bat")
                    {
                        onInteract.Invoke();
                        hit.transform.parent.localPosition = new Vector3(0.318f, 0.256f, -0.019f);
                        hit.transform.parent.localRotation = Quaternion.Euler(0f, -130f, 0f);
                    }
                    if (hotbar.activeSlot == 0 && hit.collider.gameObject.tag != "Car")
                    {
                        items[0] = hit.collider.gameObject;
                    }
                    else if (hotbar.activeSlot == 1 && hit.collider.gameObject.tag != "Car")
                    {
                        items[1] = hit.collider.gameObject;
                    }
                    else if (hotbar.activeSlot == 2 && hit.collider.gameObject.tag != "Car")
                    {
                        items[2] = hit.collider.gameObject;
                    }
                }
            }
        }
        if(hotbar.activeSlot == 0)
        {
            items[0].gameObject.SetActive(true);
            items[1].gameObject.SetActive(false);
            items[2].gameObject.SetActive(false);
        }
        else if (hotbar.activeSlot == 1)
        {
            items[0].gameObject.SetActive(false);
            items[1].gameObject.SetActive(true);
            items[2].gameObject.SetActive(false);
        }
        else if (hotbar.activeSlot == 2)
        {
            items[0].gameObject.SetActive(false);
            items[1].gameObject.SetActive(false);
            items[2].gameObject.SetActive(true);
        }
    }
}
