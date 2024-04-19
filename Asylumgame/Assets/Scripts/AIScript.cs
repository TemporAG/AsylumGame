using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class AiScript : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform player;
    public static bool SphereCast;

    public LayerMask layerMask;

    public float radius;
    [Range(0, 360)]
    public float angle;

    public GameObject playerRef;
    public GameObject distractRef;

    public LayerMask targetMask;
    public LayerMask obstructionMask;

    public bool canSeePlayer;
    public bool isDistracted;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        playerRef = GameObject.FindGameObjectWithTag("Player");
        distractRef = GameObject.FindGameObjectWithTag("Distraction");
        StartCoroutine(FOVRoutine());
    }

    private IEnumerator FOVRoutine()
    {
        WaitForSeconds wait = new WaitForSeconds(0.2f);

        while (true)
        {
            yield return wait;
            FieldOfViewCheck();
        }
    }

    private void FieldOfViewCheck()
    {
        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, radius, targetMask);

        if (rangeChecks.Length != 0)
        {
            Transform target = rangeChecks[0].transform;
            Vector3 directionToTarget = (target.position - transform.position).normalized;

            if (Vector3.Angle(transform.forward, directionToTarget) < angle / 2)
            {
                float distanceToTarget = Vector3.Distance(transform.position, target.position);

                if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstructionMask))
                    canSeePlayer = true;
                else
                    canSeePlayer = false;
            }
            else
                canSeePlayer = false;
        }
        else if (canSeePlayer)
            canSeePlayer = false;
    }

    private void OnTriggerEnter(Collider dcollision)
    {
        if (dcollision.gameObject.tag == "Distraction")
        {
            //agent.SetDestination(dcollision.transform.parent.position);
            agent.SetDestination(dcollision.transform.position);
            isDistracted = true;
        }
    }
    void Update()
    {
        if(isDistracted)
        {
            canSeePlayer = false;
        }

        if (isDistracted && agent.velocity == Vector3.zero)
        {
            isDistracted = false;
        }

        if(canSeePlayer)
        {
           agent.SetDestination(player.transform.position);
        }
    }

    void Attack()
    {
        //player dies
    }
}