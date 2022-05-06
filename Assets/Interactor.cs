using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interactor : MonoBehaviour
{
    public LayerMask interactableLayerMask = 8;
    UnityEvent onInteract;
    public List<GameObject> items = new List<GameObject>();
    void Start()
    {
        
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
                    items.Add(hit.collider.gameObject);
                    onInteract.Invoke();
                }
            }
        }
    }
}
