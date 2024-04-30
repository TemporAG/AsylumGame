using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class AiScript : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform player;
    public static bool SphereCast;

    public float range; 

    public Transform centrePoint;

    public LayerMask layerMask;

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

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.autoBraking = false;
        playerRef = GameObject.FindGameObjectWithTag("Player");
        distractRef = GameObject.FindGameObjectWithTag("Distraction");
        StartCoroutine(FOVRoutine());
        Red.SetActive(false);
        White.SetActive(true);
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
            Red.SetActive(true);
            White.SetActive(false);
        }
        else
        {
            Red.SetActive(false);
            White.SetActive(true);
        }

        if (agent.remainingDistance <= agent.stoppingDistance) //done with path
        {
            Vector3 point;
            if (RandomPoint(centrePoint.position, range, out point)) //pass in our centre point and radius of area
            {
                Debug.DrawRay(point, Vector3.up, Color.blue, 1.0f); //so you can see with gizmos
                agent.SetDestination(point);
            }
        }

        bool RandomPoint(Vector3 center, float range, out Vector3 result)
        {

            Vector3 randomPoint = center + Random.insideUnitSphere * range; //random point in a sphere 
            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas)) //documentation: https://docs.unity3d.com/ScriptReference/AI.NavMesh.SamplePosition.html
            {
                //the 1.0f is the max distance from the random point to a point on the navmesh, might want to increase if range is big
                //or add a for loop like in the documentation
                result = hit.position;
                return true;
            }

            result = Vector3.zero;
            return false;
        }
    }

    void Attack()
    {
        //player dies
    }
}