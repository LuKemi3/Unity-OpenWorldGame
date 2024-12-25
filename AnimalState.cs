using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AnimalState : MonoBehaviour
{
    // 基础属性
    public float health = 100f;
    public float stamina = 100f;
    public float alertLevel = 0f;

    // 感知范围
    public float detectionRadius = 10f;
    public float safeDistance = 15f;

    // 引用
    private Transform playerTransform;
    private AnimalController controller;

    void Start()
    {
        // 获取玩家引用
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        controller = GetComponent<AnimalController>();

        // 初始化状态
        InvokeRepeating("UpdateStamina", 1f, 1f);
    }

    void Update()
    {
        if (playerTransform != null)
        {
            // 更新与玩家的距离
            float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);

            // 更新警戒等级
            UpdateAlertLevel(distanceToPlayer);
        }
    }

    void UpdateAlertLevel(float distanceToPlayer)
    {
        if (distanceToPlayer < detectionRadius)
        {
            // 玩家越近,警戒等级越高
            alertLevel = 1 - (distanceToPlayer / detectionRadius);
            alertLevel = Mathf.Clamp(alertLevel, 0f, 1f);
        }
        else
        {
            // 玩家在探测范围外,警戒等级降低
            alertLevel *= 0.95f;
        }
    }

    void UpdateStamina()
    {
        // 恢复耐力
        if (stamina < 100f)
        {
            stamina += 5f;
            stamina = Mathf.Clamp(stamina, 0f, 100f);
        }
    }

    // 消耗耐力
    public void ConsumeStamina(float amount)
    {
        stamina -= amount;
        stamina = Mathf.Clamp(stamina, 0f, 100f);
    }
}