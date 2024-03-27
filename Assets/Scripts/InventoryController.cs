using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{

    public ItemSlot[] itemSlot;
    public GameObject InventoryPanel;

    public ItemSO[] itemSO;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Inventory") && InventoryPanel.transform.localScale == Vector3.one)
        {
            Time.timeScale = 1;
            InventoryPanel.transform.localScale = Vector3.zero;


        }
        else if (Input.GetButtonDown("Inventory") && InventoryPanel.transform.localScale == Vector3.zero)
        {
            Time.timeScale = 0;
            InventoryPanel.transform.localScale = Vector3.one;

        }

    }

    public void UseItem(string itemName)
    {
        for (int i = 0; i < itemSO.Length; i++)
        {
            if(itemSO[i].itemName == itemName)
            {
                itemSO[i].UseItem();
            }
        }
    }

    public int AddItem(string itemName,int quantity,Sprite sprite)
    {
        for (int i = 0; i < itemSlot.Length; i++)
        {
            if(itemSlot[i].isFull == false && itemSlot[i].itemName == itemName || itemSlot[i].quantity ==0)
            {
                int leftOverItems = itemSlot[i].AddItem(itemName, quantity, sprite);
                if(leftOverItems > 0)
                {
                    leftOverItems = AddItem(itemName,leftOverItems,sprite);
                    
                }
                return leftOverItems;

            }
        }
        return quantity; 
    }

    public void DeselectAllSlots()
    {
        for (int i = 0; i < itemSlot.Length; i++)
        {
            itemSlot[i].selected.SetActive(false);
            itemSlot[i].itemSelected = false;
        }
    }
}
