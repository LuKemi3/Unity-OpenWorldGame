using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class AnimalController : MonoBehaviour
{
    // �������
    private NavMeshAgent agent;
    private AnimalState state;

    // �ƶ�����
    public float walkSpeed = 2f;
    public float runSpeed = 5f;

    // ������
    public float minEscapeDistance = 5f;
    public float maxEscapeDistance = 15f;

    void Start()
    {
        // ��ȡ���
        agent = GetComponent<NavMeshAgent>();
        state = GetComponent<AnimalState>();

        // ���ó�ʼ�ٶ�
        agent.speed = walkSpeed;
    }

    void Update()
    {
        // Determine behavior according to the level of alert
        if (state.alertLevel > 0.8f)
        {
            RunAway();
        }
        else if (state.alertLevel > 0.3f)
        {
            WalkAway();
        }
    }

    void RunAway()
    {
        if (state.stamina > 20f)
        {
            // Set running speed
            agent.speed = runSpeed;

            // Get escape directions
            Vector3 directionToPlayer = transform.position - GameObject.FindGameObjectWithTag("Player").transform.position;
            Vector3 escapePoint = transform.position + directionToPlayer.normalized * Random.Range(minEscapeDistance, maxEscapeDistance);

            // Move to escape point
            agent.SetDestination(escapePoint);

            // ��������
            state.ConsumeStamina(Time.deltaTime * 10f);
        }
        else
        {
            // ��������ʱ����
            WalkAway();
        }
    }

    void WalkAway()
    {
        // ���ò����ٶ�
        agent.speed = walkSpeed;

        // ��ȡԶ�뷽��
        Vector3 directionToPlayer = transform.position - GameObject.FindGameObjectWithTag("Player").transform.position;
        Vector3 walkPoint = transform.position + directionToPlayer.normalized * minEscapeDistance;

        // �ƶ������е�
        agent.SetDestination(walkPoint);

        // ������������
        state.ConsumeStamina(Time.deltaTime * 2f);
    }
}