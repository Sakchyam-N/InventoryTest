using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class ItemSlot : MonoBehaviour,IPointerClickHandler
{

    public string itemName;
    public int quantity;
    public Sprite itemSprite;
    public bool isFull;


    [SerializeField] private GameObject quantityText;

    [SerializeField] private Image itemImage;

    [SerializeField] public GameObject selected;
    public bool itemSelected;

    private InventoryController inventoryController;

    public int itemLimit = 100;

    

    public int AddItem(string itemName, int quantity, Sprite sprite)
    {
        if (isFull)
        {
            return quantity;
        }

        this.itemName = itemName;
        
        this.itemSprite = sprite;

        itemImage.sprite = sprite;
        this.quantity += quantity;
        if(this.quantity >= itemLimit)
        {

            quantityText.SetActive(true);
            quantityText.GetComponent<TextMeshProUGUI>().text = itemLimit.ToString();
            isFull = true;

            int extraItem = this.quantity - itemLimit;
            this.quantity = itemLimit;
            return extraItem;
        }
        quantityText.SetActive(true);
        quantityText.GetComponent<TextMeshProUGUI>().text = this.quantity.ToString();
        return 0;

    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(eventData.button == PointerEventData.InputButton.Left)
        {
            OnLeftClick();
        }
        else if(eventData.button == PointerEventData.InputButton.Right)
        {
            OnRightClick();
        }
    }

    private void OnRightClick()
    {
        //drop item
        GameObject itemToDrop = new GameObject(itemName);
        Item newItem = itemToDrop.AddComponent<Item>();
        newItem.quantity = 1;
        newItem.sprite = itemSprite;
        newItem.itemName = itemName;

        SpriteRenderer spriteR = itemToDrop.AddComponent<SpriteRenderer>();
        spriteR.sprite = itemSprite;
        spriteR.sortingOrder = 5;
        spriteR.sortingLayerName = "Ground";

        itemToDrop.AddComponent<BoxCollider2D>();

        itemToDrop.transform.position = GameObject.FindWithTag("Player").transform.position + new Vector3(2f, 0, 0);
        itemToDrop.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        this.quantity -= 1;
        quantityText.GetComponent<TextMeshProUGUI>().text = this.quantity.ToString();
        if (this.quantity <= 0)
        {
            EmptySlot();
        }

    }

    private void OnLeftClick()
    {
        if (itemSelected)
        {
            inventoryController.UseItem(itemName);
            this.quantity -= 1;
            quantityText.GetComponent<TextMeshProUGUI>().text = this.quantity.ToString();
            if (this.quantity <= 0)
            {
                EmptySlot();
            }
        }
        else
        {
            inventoryController.DeselectAllSlots();
            selected.SetActive(true);
            itemSelected = true;
        }
       
    }

    private void EmptySlot()
    {
        quantityText.SetActive(false);
        itemImage.sprite = null;

    }

    // Start is called before the first frame update
    void Start()
    {
        inventoryController = GameObject.Find("Canvas").GetComponent<InventoryController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
