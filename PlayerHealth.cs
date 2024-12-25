using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100; // �������ֵ
    private int currentHealth;

    public Text healthText; // ��ʾ����ֵ������
    public Image healthBar; // ͼ�λ�������ֵ������ɫ���Σ�

    void Start()
    {
        // ��ʼ������ֵ
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
        // ʵ�������߼�
    }
}
