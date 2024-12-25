using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TrashSlot : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject trashAlertUI;
    private Text textToModify;
    public Sprite trash_closed;
    public Sprite trash_opened;
    private Image imageComponent;
    Button YesBTN, NoBTN;

    GameObject draggedItem
    {
        get { return DragDrop.itemBeingDragged; }
    }

    GameObject itemToBeDeleted;
    public string itemName
    {
        get
        {
            string name = itemToBeDeleted.name;
            string toRemove = "(Clone)";
            string result = name.Replace(toRemove, "");
            return result;
        }
    }

    void Start()
    {
        // 修改这里：直接获取当前GameObject的Image组件
        imageComponent = GetComponent<Image>();

        textToModify = trashAlertUI.transform.Find("Text").GetComponent<Text>();

        // 修改这里：改为r和r (1)按钮的路径
        YesBTN = trashAlertUI.transform.Find("r").GetComponent<Button>();
        YesBTN.onClick.AddListener(delegate { DeleteItem(); });

        NoBTN = trashAlertUI.transform.Find("r (1)").GetComponent<Button>();
        NoBTN.onClick.AddListener(delegate { CancelDeletion(); });
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (draggedItem.GetComponent<InventoryItem>().isTrashable == true)
        {
            itemToBeDeleted = draggedItem.gameObject;
            StartCoroutine(notifyBeforeDeletion());
        }
    }

    IEnumerator notifyBeforeDeletion()
    {
        trashAlertUI.SetActive(true);
        textToModify.text = "Throw away this " + itemName + "?";
        yield return new WaitForSeconds(1f);
    }

    private void CancelDeletion()
    {
        imageComponent.sprite = trash_closed;
        trashAlertUI.SetActive(false);
    }

    private void DeleteItem()
    {
        imageComponent.sprite = trash_closed;
        DestroyImmediate(itemToBeDeleted.gameObject);
        InventorySystem.Instance.ReCalculateList();
        //CraftingSystem.Instance.RefreshNeededItems();
        trashAlertUI.SetActive(false);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (draggedItem != null && draggedItem.GetComponent<InventoryItem>().isTrashable == true)
        {
            imageComponent.sprite = trash_opened;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (draggedItem != null && draggedItem.GetComponent<InventoryItem>().isTrashable == true)
        {
            imageComponent.sprite = trash_closed;
        }
    }
}