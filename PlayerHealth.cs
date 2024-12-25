using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100; // 最大生命值
    private int currentHealth;

    public Text healthText; // 显示生命值的文字
    public Image healthBar; // 图形化的生命值条（红色矩形）

    void Start()
    {
        // 初始化生命值
        currentHealth = maxHealth;
        UpdateHealthUI();
    }

    public void TakeDamage(int damage)
    {
        
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        UpdateHealthUI();
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void UpdateHealthUI()
    {
        if (healthText != null)
        {
            healthText.text = $"Health: {currentHealth}/{maxHealth}";
        }

        if (healthBar != null)
        {
            
            float healthPercentage = (float)currentHealth / maxHealth;
            healthBar.transform.localScale = new Vector3((float)(healthPercentage* 0.8268876f), 0.437f, 1f);
        }
    }

    private void Die()
    {
        Debug.Log("Player has died!");
        // 实现死亡逻辑
    }
}
