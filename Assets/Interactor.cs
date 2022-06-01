using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEditor;
using UnityEngine.SceneManagement;

public class Interactor : MonoBehaviour
{
    public LayerMask interactableLayerMask = 8;
    UnityEvent onInteract;
    Hotbar hotbar;
    public GameObject[] items = { };
    //public Dictionary<int, GameObject> items = new Dictionary<int ,GameObject>(3);
    [SerializeField] GameObject hotbarUI;
    [SerializeField] GameObject player;
    [SerializeField] GameObject models3d;
    [SerializeField] GameObject hand;
    [SerializeField] Jason jason;
    [SerializeField] AudioSource source;
    [SerializeField] AudioClip clip;
    GameObject lastCollider;
    Animator animator;

    void Start()
    {
        hotbar = hotbarUI.GetComponent<Hotbar>();
        animator = GetComponent<Animator>();

    }

    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, 50f, interactableLayerMask))
        {
            if (hit.collider.GetComponent<Interactable>())
            {
                onInteract = hit.collider.GetComponent<Interactable>().onInteract;

                try
                {
                    if (hit.collider.gameObject.transform.GetChild(0).gameObject.name == "Canvas")
                    {
                    hit.collider.gameObject.transform.GetChild(0).gameObject.SetActive(true);
                    lastCollider = hit.collider.gameObject;
                    hit.collider.gameObject.transform.GetChild(0).gameObject.transform.LookAt(player.transform.position);
                    hit.collider.gameObject.transform.GetChild(0).gameObject.transform.eulerAngles = new Vector3(0f, hit.collider.gameObject.transform.GetChild(0).gameObject.transform.eulerAngles.y, 0f);
                    }

                    items[hotbar.activeSlot].transform.GetChild(0).gameObject.SetActive(false);
                }
                catch { }

                if (Input.GetMouseButtonDown(0))
                {

                    if (hit.collider.gameObject.name == "Pick-Up old" && items[hotbar.activeSlot].tag == "Jerry Can")
                    {
                        hit.collider.gameObject.SetActive(false);
                        items[hotbar.activeSlot].SetActive(false);
                        SceneManager.LoadScene("Menu");
                    }
                    if (hit.collider.gameObject.tag == "Jerry Can")
                    {
                        items[hotbar.activeSlot].transform.SetParent(models3d.transform, true);
                        items[hotbar.activeSlot].transform.position = hit.collider.gameObject.transform.position;
                        items[hotbar.activeSlot].transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                        hit.transform.SetParent(hand.transform, true);
                        hit.collider.gameObject.transform.localRotation = Quaternion.Euler(180f, 0f, 0f);
                        hit.collider.gameObject.transform.localPosition = new Vector3(0f, 1.22f, 0.29f);
                    }
                    else if (hit.collider.gameObject.tag == "Spray")
                    {
                        items[hotbar.activeSlot].transform.SetParent(models3d.transform, true);
                        items[hotbar.activeSlot].transform.position = hit.collider.gameObject.transform.position;
                        items[hotbar.activeSlot].transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                        hit.transform.SetParent(hand.transform, true);
                        hit.collider.gameObject.transform.localPosition = new Vector3(0, 1.12f, 0.039f);
                        hit.collider.gameObject.transform.localRotation = Quaternion.Euler(180f, 0f, 0f);
                    }
                    else if (hit.collider.gameObject.tag == "Bat")
                    {
                        items[hotbar.activeSlot].transform.SetParent(models3d.transform, true);
                        items[hotbar.activeSlot].transform.position = hit.collider.gameObject.transform.position;
                        items[hotbar.activeSlot].transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                        hit.transform.SetParent(hand.transform, true);
                        hit.transform.localPosition = new Vector3(0f, 0.24f, 0.1f);
                        hit.transform.localRotation = Quaternion.Euler(0f, 102f, 0f);
                    }

                    if (hotbar.activeSlot == 0 && hit.collider.gameObject.tag != "Car" && hit.collider.gameObject.name != "Jason")
                    {
                        items[0] = hit.collider.gameObject;
                    }
                    else if (hotbar.activeSlot == 1 && hit.collider.gameObject.tag != "Car" && hit.collider.gameObject.name != "Jason")
                    {
                        items[1] = hit.collider.gameObject;
                    }
                    else if (hotbar.activeSlot == 2 && hit.collider.gameObject.tag != "Car" && hit.collider.gameObject.name != "Jason")
                    {
                        items[2] = hit.collider.gameObject;
                    }
                    
                    if (items[hotbar.activeSlot].tag == "Spray" && hit.collider.gameObject.name == "Jason" && hit.distance < 5f)
                    {
                        Destroy(items[hotbar.activeSlot]);
                        items[hotbar.activeSlot] = GameObject.Find(string.Format("GameObject ({0})", hotbar.activeSlot));
                        GameObject.Find(string.Format("GameObject ({0})", hotbar.activeSlot)).transform.parent = null;
                        jason.Stun = true;
                        clip = (AudioClip)AssetDatabase.LoadAssetAtPath("Assets/Audio/spray.mp3", typeof(AudioClip));
                        source.PlayOneShot(clip);
                    }
                    else if (items[hotbar.activeSlot].tag == "Bat" && hit.collider.gameObject.name == "Jason" && hit.distance < 5f)
                    {
                        Destroy(items[hotbar.activeSlot]);
                        items[hotbar.activeSlot] = GameObject.Find(string.Format("GameObject ({0})", hotbar.activeSlot));
                        GameObject.Find(string.Format("GameObject ({0})", hotbar.activeSlot)).transform.parent = null;
                        jason.Stun = true;
                        clip = (AudioClip)AssetDatabase.LoadAssetAtPath("Assets/Audio/bat.mp3", typeof(AudioClip));
                        source.PlayOneShot(clip);
                    }
                }
            }
        }
        else
        {
            try
            {
                lastCollider.transform.GetChild(0).gameObject.SetActive(false);
            }
            catch{ }
        }
        try
        {
            if (hotbar.activeSlot == 0)
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
        catch { }
        
    }
}
