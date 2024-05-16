using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class hallucinationMOVE : MonoBehaviour
{
    public NavMeshAgent agent;
    public static bool SphereCast;


    public LayerMask layerMask;

    public Transform[] waypoints;
    int waypointIndex;
    Vector3 target;

    public float radius;
    [Range(0, 360)]
    public float angle;

    public GameObject White;

    public LayerMask targetMask;
    public LayerMask obstructionMask;




    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.autoBraking = false;
        White.SetActive(true);
        UpdateDestination();
    }


    void Update()
    {
        UpdateDestination();
        if (Vector3.Distance(transform.position, target) < 1)
        {
            IterateWaypointIndex();

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

    /*public IEnumerator PlayerSeen(float seenDelay)
    {
        yield return new WaitForSeconds(seenDelay);
        if (canSeePlayer)
        {
            Debug.Log("can still see player");
        }
    }*/
}