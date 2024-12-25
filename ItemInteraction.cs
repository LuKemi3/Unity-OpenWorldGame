using UnityEngine;

public class ItemInteraction : MonoBehaviour
{
    public string itemName = "Default Item"; // ��Ʒ����
    public float interactDistance = 3f; // ��������
    public AudioClip pickupSound; // ʰȡ��Ч

    private Transform player; // ���
    private AudioSource audioSource; // ��Ƶ���
    private bool isPlayerNearby = false;

    void Start()
    {
        // �ҵ����
        player = GameObject.FindWithTag("Player").transform;

        // ȷ���� AudioSource ���
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
        audioSource.playOnAwake = false; // �����Զ�����
    }

    public string GetitemName()
    {
        return itemName;
    }

    void Update()
    {
        // �������Ƿ��ڽ�����Χ��
        if (Vector3.Distance(player.position, transform.position) <= interactDistance)
        {
            if (!isPlayerNearby)
            {
                isPlayerNearby = true;
                InteractionUIController.ShowInteraction($"Press 'E' to pick up {itemName}");
            }

            // ���°���ʰȡ��Ʒ
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

        // ����ʰȡ��Ч
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
