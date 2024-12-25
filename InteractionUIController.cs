using UnityEngine;
using UnityEngine.UI; // ���ʹ�� TextMeshPro���滻Ϊ using TMPro;

public class InteractionUIController : MonoBehaviour
{
    public Text interactionText; // ���ʹ�� TextMeshPro������ TMP_Text �滻
    private static InteractionUIController instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    // ��ʾ��ʾ
    public static void ShowInteraction(string message)
    {
        if (instance != null)
        {
            instance.interactionText.text = message;
            instance.interactionText.gameObject.SetActive(true);
        }
    }

    // ������ʾ
    public static void HideInteraction()
    {
        if (instance != null)
        {
            instance.interactionText.gameObject.SetActive(false);
        }
    }
}
