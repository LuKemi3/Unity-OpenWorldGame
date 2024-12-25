using UnityEngine;
using UnityEngine.UI; // 如果使用 TextMeshPro，替换为 using TMPro;

public class InteractionUIController : MonoBehaviour
{
    public Text interactionText; // 如果使用 TextMeshPro，请用 TMP_Text 替换
    private static InteractionUIController instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    // 显示提示
    public static void ShowInteraction(string message)
    {
        if (instance != null)
        {
            instance.interactionText.text = message;
            instance.interactionText.gameObject.SetActive(true);
        }
    }

    // 隐藏提示
    public static void HideInteraction()
    {
        if (instance != null)
        {
            instance.interactionText.gameObject.SetActive(false);
        }
    }
}
