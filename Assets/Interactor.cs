using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interactor : MonoBehaviour
{
    public LayerMask interactableLayerMask = 8;
    UnityEvent onInteract;
    Hotbar hotbar;
    public Dictionary<int, GameObject> items = new Dictionary<int ,GameObject>();
    [SerializeField] GameObject hotbarUI;
    void Start()
    {
        hotbar = hotbarUI.GetComponent<Hotbar>();
    }

    void Update()
    {
        RaycastHit hit;

        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, 100f , interactableLayerMask))
        {
            if (hit.collider.GetComponent<Interactable>())
            {
                onInteract = hit.collider.GetComponent<Interactable>().onInteract;
                if (Input.GetMouseButton(0))
                {
                    if (hotbar.activeSlot == 0)
                    {
                        items[0] = hit.collider.gameObject;
                    }
                    else if (hotbar.activeSlot == 1)
                    {
                        items[1] = hit.collider.gameObject;
                    }
                    else if (hotbar.activeSlot == 2)
                    {
                        items[2] = hit.collider.gameObject;
                    }
                    //onInteract.Invoke();
                }
            }
        }
    }
}
