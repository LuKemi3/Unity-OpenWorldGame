using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class AnimalController : MonoBehaviour
{
    // 组件引用
    private NavMeshAgent agent;
    private AnimalState state;

    // 移动参数
    public float walkSpeed = 2f;
    public float runSpeed = 5f;

    // 躲避相关
    public float minEscapeDistance = 5f;
    public float maxEscapeDistance = 15f;

    void Start()
    {
        // 获取组件
        agent = GetComponent<NavMeshAgent>();
        state = GetComponent<AnimalState>();

        // 设置初始速度
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

            // 消耗耐力
            state.ConsumeStamina(Time.deltaTime * 10f);
        }
        else
        {
            // 耐力不足时步行
            WalkAway();
        }
    }

    void WalkAway()
    {
        // 设置步行速度
        agent.speed = walkSpeed;

        // 获取远离方向
        Vector3 directionToPlayer = transform.position - GameObject.FindGameObjectWithTag("Player").transform.position;
        Vector3 walkPoint = transform.position + directionToPlayer.normalized * minEscapeDistance;

        // 移动到步行点
        agent.SetDestination(walkPoint);

        // 消耗少量耐力
        state.ConsumeStamina(Time.deltaTime * 2f);
    }
}