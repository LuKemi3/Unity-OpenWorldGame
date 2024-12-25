using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BearWalkState : StateMachineBehaviour
{
    float timer;
    public float walkingTime = 10f;

    // Reference to components
    Transform player;
    NavMeshAgent agent;

    // Patrol parameters
    public float detectionAreaRadius = 18f;
    public float walkSpeed = 2f;

    // Waypoints for patrol
    List<Transform> waypointsList = new List<Transform>();

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        agent = animator.GetComponent<NavMeshAgent>();

        // Set initial values
        agent.speed = walkSpeed;
        timer = 0;

        // -- Get all waypoints and Move to the first waypoint -- //
        GameObject waypointsCluster = GameObject.FindGameObjectWithTag("Waypoints");
        foreach (Transform t in waypointsCluster.transform)
        {
            waypointsList.Add(t);
        }

        Vector3 firstPosition = waypointsList[Random.Range(0, waypointsList.Count)].position;
        agent.SetDestination(firstPosition);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            agent.SetDestination(waypointsList[Random.Range(0, waypointsList.Count)].position);
        }

        // -- Transition to Idle State -- //
        timer += Time.deltaTime;
        if (timer > walkingTime)
        {
            animator.SetBool("isWalking", false);
        }

        // -- Transition to Chase State -- //
        float distanceFromPlayer = Vector3.Distance(player.position, animator.transform.position);
        if (distanceFromPlayer < detectionAreaRadius)
        {
            animator.SetBool("isChasing", true);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agent.SetDestination(agent.transform.position);
    }


}
