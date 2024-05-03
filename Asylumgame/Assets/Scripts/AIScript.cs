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

    public Transform[] waypoints;
    int waypointIndex;
    Vector3 target;

    public float radius;
    [Range(0, 360)]
    public float angle;

    public GameObject playerRef;
    public GameObject distractRef;
    public GameObject Red;
    public GameObject White;

    public LayerMask targetMask;
    public LayerMask obstructionMask;
    public bool canSeePlayer;
    public bool isDistracted;
    public bool playerSeenForLongTime;
    float seenDelay = 4.0f;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.autoBraking = false;
        playerRef = GameObject.FindGameObjectWithTag("Player");
        distractRef = GameObject.FindGameObjectWithTag("Distraction");
        StartCoroutine(FOVRoutine());
        Red.SetActive(false);
        White.SetActive(true);
        UpdateDestination();
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
    bool needsDelay;

    private void OnTriggerEnter(Collider dcollision)
    {
        if (dcollision.gameObject.tag == "Distraction")
        {
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
            StartCoroutine(Delay(4));
        }

        if(canSeePlayer)
        {
            
            needsDelay = true;
            agent.SetDestination(player.transform.position);
            Red.SetActive(true);
            White.SetActive(false);
            StartCoroutine(PlayerSeen(0.1f));
        }
        else if (needsDelay)
        {
            needsDelay = false;
            Red.SetActive(false);
            White.SetActive(true);
            StartCoroutine(Delay(4));
        }

        if(Vector3.Distance(transform.position, target) < 1)
        { 
           IterateWaypointIndex();
           UpdateDestination(); 
        }


    }

    void UpdateDestination()
    {
        target = waypoints[waypointIndex].position;
        agent.SetDestination(target);
    }

    void IterateWaypointIndex()
    {
        waypointIndex++;
        if (waypointIndex == waypoints.Length)
        {
            waypointIndex = 0;
        }
    }

    void Attack()
    {
        //player dies
    }
    
    public IEnumerator Delay(float delay)
    {
        yield return new WaitForSeconds(delay);
        UpdateDestination();
    }

    public IEnumerator PlayerSeen(float seenDelay)
    {
        yield return new WaitForSeconds(seenDelay);
        if (canSeePlayer)
        {
            Debug.Log("can still see player");
        }
    }
}