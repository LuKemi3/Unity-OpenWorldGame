using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AnimalState : MonoBehaviour
{
    // ��������
    public float health = 100f;
    public float stamina = 100f;
    public float alertLevel = 0f;

    // ��֪��Χ
    public float detectionRadius = 10f;
    public float safeDistance = 15f;

    // ����
    private Transform playerTransform;
    private AnimalController controller;

    void Start()
    {
        // ��ȡ�������
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        controller = GetComponent<AnimalController>();

        // ��ʼ��״̬
        InvokeRepeating("UpdateStamina", 1f, 1f);
    }

    void Update()
    {
        if (playerTransform != null)
        {
            // ��������ҵľ���
            float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);

            // ���¾���ȼ�
            UpdateAlertLevel(distanceToPlayer);
        }
    }

    void UpdateAlertLevel(float distanceToPlayer)
    {
        if (distanceToPlayer < detectionRadius)
        {
            // ���Խ��,����ȼ�Խ��
            alertLevel = 1 - (distanceToPlayer / detectionRadius);
            alertLevel = Mathf.Clamp(alertLevel, 0f, 1f);
        }
        else
        {
            // �����̽�ⷶΧ��,����ȼ�����
            alertLevel *= 0.95f;
        }
    }

    void UpdateStamina()
    {
        // �ָ�����
        if (stamina < 100f)
        {
            stamina += 5f;
            stamina = Mathf.Clamp(stamina, 0f, 100f);
        }
    }

    // ��������
    public void ConsumeStamina(float amount)
    {
        stamina -= amount;
        stamina = Mathf.Clamp(stamina, 0f, 100f);
    }
}