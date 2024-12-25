using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySystem : MonoBehaviour
{

    public static InventorySystem Instance { get; set; }

    public GameObject inventoryScreenUI;
    public List<GameObject> slotList = new List<GameObject>();
    public List<string> itemList = new List<string>();
    private GameObject itemToAdd;
    private GameObject whatSlotToEquip;
    public bool isOpen;
    public bool isFull;
    public GameObject pickupAlert;
    public Text pickupName;
    public Image pickupImage;


    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }


    void Start()
    {
        isOpen = false;
        
        PopulateSlotList();
        Cursor.visible = false;

    }

   

    private void PopulateSlotList()
    {
        foreach(Transform child in inventoryScreenUI.transform)
        {
            if (child.CompareTag("Slot"))
            {
                slotList.Add(child.gameObject);
            }
        }
    }


    void Update()
    {

        if (Input.GetKeyDown(KeyCode.I) && !isOpen)
        {

            Debug.Log("i is pressed");
            inventoryScreenUI.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            isOpen = true;
            Cursor.visible = true;

        }
        else if (Input.GetKeyDown(KeyCode.I) && isOpen)
        {
            inventoryScreenUI.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            isOpen = false;
            Cursor.visible = false;
        }
    }


    public void AddToInventory(string itemName)
    {
     
    
        whatSlotToEquip = FindNextEmptySlot();

        itemToAdd = Instantiate(Resources.Load<GameObject>(itemName), whatSlotToEquip.transform.position, whatSlotToEquip.transform.rotation);
        itemToAdd.transform.SetParent(whatSlotToEquip.transform);

        itemList.Add(itemName);
        TriggerPickPopUp(itemName, itemToAdd.GetComponent<Image>().sprite);


    }

    void TriggerPickPopUp(string itemName, Sprite itemSprite)
    {
        pickupAlert.SetActive(true);
        pickupImage.sprite = itemSprite;
        pickupName.text = itemName;
    }

    public bool CheckIfFull()
    {
        int counter = 0;
        foreach (GameObject slot in slotList)
        {
            if (slot.transform.childCount > 0)
            {
                counter += 1;
            }
            
        }
        if (counter == 14)
        {
            return true;
        }
        else
        {
            return false;
        }

    }


    private GameObject FindNextEmptySlot()
    {
        foreach(GameObject slot in slotList)
        {
            if (slot.transform.childCount == 0)
            {
                return slot;
            }
            
        }
        return new GameObject();
    }
    public void ReCalculateList()
    {
        // Clear the current item list
        itemList.Clear();

        // Iterate through each slot in the slot list
        foreach (GameObject slot in slotList)
        {
            // Check if the slot has any children
            if (slot.transform.childCount > 0)
            {
                Transform child = slot.transform.GetChild(0);

                // Add the name of the child (item name) to the item list
                itemList.Add(child.name);
            }
        }
    }



}