using System.Collections.Generic; // ʹ���б�
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<string> items = new List<string>(); // ������Ʒ�б�

    // �����Ʒ������
    public void AddItem(string itemName)
    {
        items.Add(itemName);
        Debug.Log($"Added {itemName} to inventory.");
    }

    // ��ʾ�����е���Ʒ
    public void DisplayInventory()
    {
        Debug.Log("Inventory:");
        foreach (string item in items)
        {
            Debug.Log(item);
        }
    }
}
