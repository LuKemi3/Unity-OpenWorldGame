using System.Collections.Generic; // 使用列表
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<string> items = new List<string>(); // 背包物品列表

    // 添加物品到背包
    public void AddItem(string itemName)
    {
        items.Add(itemName);
        Debug.Log($"Added {itemName} to inventory.");
    }

    // 显示背包中的物品
    public void DisplayInventory()
    {
        Debug.Log("Inventory:");
        foreach (string item in items)
        {
            Debug.Log(item);
        }
    }
}
