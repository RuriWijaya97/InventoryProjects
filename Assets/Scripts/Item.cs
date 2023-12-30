using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public GameObject interactText;
    [Header ("Item Informations")]
    public string itemName;
    public int quantity;
    public Sprite sprite;
    public Material material;
    public Mesh mesh;
    [TextArea]
    public string itemDescription;
    
    [Header ("Configurations")]
    private InventoryManager inventoryManager;
    // Start is called before the first frame update
    void Start()
    {
        inventoryManager = GameObject.Find("HUD").GetComponent<InventoryManager>();
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            interactText.SetActive(true);
        }
    }

    void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            GetIem();
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            interactText.SetActive(false);
        }
    }

    void GetIem()
    {
        // interactText.SetActive(true);
        if (Input.GetButtonDown("Interact"))
        {
            interactText.SetActive(false);
            int leftOverItems = inventoryManager.AddItem(itemName, quantity, sprite, itemDescription, interactText, mesh, material);
            if (leftOverItems <= 0)
            {
                Destroy(gameObject);
            }
            else
            {
                quantity = leftOverItems;
            } 
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
