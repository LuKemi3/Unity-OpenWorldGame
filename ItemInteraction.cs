using UnityEngine;

public class ItemInteraction : MonoBehaviour
{
    public string itemName = "Default Item"; // 物品名称
    public float interactDistance = 3f; // 交互距离
    public AudioClip pickupSound; // 拾取音效

    private Transform player; // 玩家
    private AudioSource audioSource; // 音频组件
    private bool isPlayerNearby = false;

    void Start()
    {
        // 找到玩家
        player = GameObject.FindWithTag("Player").transform;

        // 确保有 AudioSource 组件
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
        audioSource.playOnAwake = false; // 禁用自动播放
    }

    public string GetitemName()
    {
        return itemName;
    }

    void Update()
    {
        // 检测玩家是否在交互范围内
        if (Vector3.Distance(player.position, transform.position) <= interactDistance)
        {
            if (!isPlayerNearby)
            {
                isPlayerNearby = true;
                InteractionUIController.ShowInteraction($"Press 'E' to pick up {itemName}");
            }

            // 按下按键拾取物品
            if (Input.GetKeyDown(KeyCode.E))
            {
                
                PickUpItem();
            }
        }
        else
        {
            if (isPlayerNearby)
            {
                isPlayerNearby = false;
                InteractionUIController.HideInteraction();
            }
        }
    }

    

    void PickUpItem()
    {
        Debug.Log($"Picked up {itemName}");

        // 播放拾取音效
        if (pickupSound != null)
        {
            audioSource.PlayOneShot(pickupSound);
        }
        if (!InventorySystem.Instance.CheckIfFull())
        {
            InventorySystem.Instance.AddToInventory(itemName);
            InteractionUIController.HideInteraction();
            Destroy(gameObject,0.1f);
        }
        else
        {
            Debug.Log("Inventory is full");
        }

    }

   
}
