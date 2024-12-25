using UnityEngine;
using UnityEngine.AI;

public class BearAttackState : StateMachineBehaviour
{
    Transform player;
    NavMeshAgent agent;

    public float stopAttackingDistance = 2.5f;
    public float attackRate = 1f; // ����Ƶ��
    private float attackTimer;
    private int damageToInflict = 10; // ÿ�ι������˺�ֵ

    private PlayerHealth playerHealth;

    public AudioClip attackSound; // ������Ч
    private AudioSource audioSource;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        agent = animator.GetComponent<NavMeshAgent>();

        if (player != null)
        {
            playerHealth = player.GetComponent<PlayerHealth>();
        }

        // ��ȡ��Ч��������һ���µ�
        audioSource = animator.GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = animator.gameObject.AddComponent<AudioSource>();
        }

        attackTimer = 0f;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        LookAtPlayer();

        float distanceFromPlayer = Vector3.Distance(player.position, animator.transform.position);
        if (distanceFromPlayer > stopAttackingDistance)
        {
            animator.SetBool("isAttacking", false);
            return;
        }

        // attack
        attackTimer += Time.deltaTime;
        if (attackTimer >= attackRate)
        {
            AttackPlayer();
            attackTimer = 0f;
        }
    }

    private void LookAtPlayer()
    {
        Vector3 direction = player.position - agent.transform.position;
        agent.transform.rotation = Quaternion.LookRotation(direction);

        var yRotation = agent.transform.eulerAngles.y;
        agent.transform.rotation = Quaternion.Euler(0, yRotation, 0);
    }

    private void AttackPlayer()
    {
        if (playerHealth != null)
        {
            playerHealth.TakeDamage(damageToInflict);

            // ���Ź�����Ч
            if (attackSound != null && audioSource != null)
            {
                audioSource.PlayOneShot(attackSound);
            }

            Debug.Log("Bear attacked the player!");
        }
    }
}
