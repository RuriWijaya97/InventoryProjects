using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class ItemSlot : MonoBehaviour, IPointerClickHandler
{
    private InventoryManager inventoryManager;

    //Item Data
    [Header ("Item Data")]
    public string itemName;
    public int quantity;
    public Sprite itemSprite;
    public Material itemMaterial;
    public Mesh itemMesh;
    public bool isFull;
    [TextArea]
    public string itemDescription;
    public Sprite emptySprite;
    [SerializeField] private int maxNumberOfItems;
    public GameObject interactText;


    //Item Slot
    [Header ("Item Slot")]
    [SerializeField] private TMP_Text quantityText;
    [SerializeField] private GameObject qtyTextObject;
    [SerializeField] private Image itemImage;
    public GameObject selectedShader;
    public bool thisItemSelected;
    
    //Item Description
    [Header ("Item Description Slot")]
    public Image itemDescriptionImage;
    public TMP_Text itemDescriptionNameText;
    public TMP_Text itemDescriptionText;


    void Start()
    {
        inventoryManager = GameObject.Find("HUD").GetComponent<InventoryManager>();
    }
    public int AddItem(string itemName, int quantity, Sprite itemSprite, string itemDescription, GameObject interactText, Mesh itemMesh, Material itemMaterial)
    {
        // Check if the slot is already full
        if (isFull)
        
            return quantity;
        
        
        //Update Name
        this.itemName = itemName;
        
        //Update Image
        this.itemSprite = itemSprite;
        itemImage.sprite = itemSprite;

        //Update Description
        this.itemDescription = itemDescription;
        
        //Update Text
        this.interactText = interactText;

        //Update Mesh
        this.itemMesh = itemMesh;

        //Update Material
        this.itemMaterial = itemMaterial;
        
        //Update Quantity
        this.quantity += quantity;
        if (this.quantity >= maxNumberOfItems)
        {
            quantityText.text = maxNumberOfItems.ToString();
            qtyTextObject.SetActive(true);
            isFull = true;
        
        
            //Return the Leftovers
            int extraItems = this.quantity - maxNumberOfItems;
            this.quantity = maxNumberOfItems;
            return extraItems;
        }
        
        //Update Quantity Text
        quantityText.text = this.quantity.ToString();
        qtyTextObject.SetActive(true);

        return 0;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
       if (eventData.button == PointerEventData.InputButton.Left)
       {
            OnLeftClick();        
       }
       if (eventData.button == PointerEventData.InputButton.Right)
       {
            OnRightClick();        
       }
    }

    public void OnLeftClick() 
    {
        if (thisItemSelected)
        {
            bool usable = inventoryManager.UseItem(itemName);
            if (usable)
            {
                this.quantity -= 1;
                quantityText.text = this.quantity.ToString();
                if (this.quantity <= 0)
                {
                    EmptySlot();
                }
                Debug.Log("Use");
            } 
        }
        else
        {
            inventoryManager.deselectAllSlots();
            selectedShader.SetActive(true);
            thisItemSelected = true;
            itemDescriptionNameText.text = itemName;
            itemDescriptionText.text = itemDescription;
            itemDescriptionImage.sprite = itemSprite;
        
            if (itemDescriptionImage.sprite == null)
            {
                itemDescriptionImage.sprite = emptySprite;
            }
            if (this.quantity <= 0)
            {
                EmptySlot();
            }
        }
    }
    void EmptySlot()
    {
        qtyTextObject.SetActive(false);
        itemImage.sprite = emptySprite;
        itemDescriptionNameText.text = "";
        itemDescriptionText.text = "";
        itemDescriptionImage.sprite = emptySprite;
        selectedShader.SetActive(false);
        thisItemSelected = false;
    }

    public void OnRightClick() 
    {
        if (quantity > 0)
        {
            GameObject itemToDrop = new GameObject(itemName);
            Item newItem = itemToDrop.AddComponent <Item>();
            newItem.quantity = 1;
            newItem.itemName = itemName;
            newItem.sprite = itemSprite;
            newItem.material = itemMaterial;
            newItem.mesh = itemMesh;
            newItem.itemDescription = itemDescription;
            newItem.interactText = interactText;

            MeshFilter meshFilter = itemToDrop.AddComponent<MeshFilter>();
            meshFilter.mesh = itemMesh;
            MeshRenderer meshRenderer = itemToDrop.AddComponent<MeshRenderer>();
            meshRenderer.material = itemMaterial;
            meshRenderer.sortingOrder = 5;
            meshRenderer.sortingLayerName = "Ground";

            itemToDrop.AddComponent<BoxCollider>();
            Rigidbody rb = itemToDrop.AddComponent<Rigidbody>();
            rb.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;

            itemToDrop.transform.position = GameObject.FindWithTag("Player").transform.position + new Vector3(1, 0, 0);
            itemToDrop.transform.localScale = new Vector3(.5f, .5f, .5f);

            this.quantity -= 1;
            quantityText.text = this.quantity.ToString();
            if (this.quantity <= 0)
            {
                EmptySlot();
            }
            
        }
        
        
    }

}
