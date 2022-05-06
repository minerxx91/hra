using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hotbar : MonoBehaviour
{
    [SerializeField] GameObject kamera;
    Interactor interactor;
    int activeSlot = 0;
    [SerializeField] GameObject slot1;
    [SerializeField] GameObject slot2;
    [SerializeField] GameObject slot3;
    [SerializeField] Texture2D active;
    [SerializeField] Texture2D unactive;
    [SerializeField] Texture2D spray;
    [SerializeField] GameObject item1;
    [SerializeField] GameObject item2;
    [SerializeField] GameObject item3;
    void Start()
    {
        interactor = kamera.GetComponent<Interactor>();
    }

    void Update()
    {
        if (Input.mouseScrollDelta.y == 1f)
        {
            if (activeSlot == 2)
            {
                activeSlot = 0;
            }
            else activeSlot++;
        }
        else if (Input.mouseScrollDelta.y == -1f)
        {
            if (activeSlot == 0)
            {
                activeSlot = 2;
            }
            else activeSlot--;
        }

        if (activeSlot == 0)
        {
            slot1.GetComponent<RawImage>().texture = active;
            slot2.GetComponent<RawImage>().texture = unactive;
            slot3.GetComponent<RawImage>().texture = unactive;
            if (interactor.items.Count > 0)
            {
                item1.GetComponent<RawImage>().texture = Resources.Load<Texture2D>(interactor.items[activeSlot].name);
            }
        }
        else if (activeSlot == 1)
        {
            slot2.GetComponent<RawImage>().texture = active;
            slot1.GetComponent<RawImage>().texture = unactive;
            slot3.GetComponent<RawImage>().texture = unactive;
        }
        else
        {
            slot3.GetComponent<RawImage>().texture = active;
            slot1.GetComponent<RawImage>().texture = unactive;
            slot2.GetComponent<RawImage>().texture = unactive;
        }
    }
}
