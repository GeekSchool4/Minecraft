using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public NavMeshAgent agent;
    public float range; 
    public GameObject player;
    public int playerPositionRange;
    public Transform centrePoint; 
    

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

    }


    void Update()
    {   
        if (Vector3.Distance(transform.position, player.transform.position) <= playerPositionRange)
        {
            agent.SetDestination(player.transform.position);
        }
        else
        {
            if (agent.remainingDistance <= agent.stoppingDistance) 
            {
                Vector3 point;
                if (RandomPoint(centrePoint.position, range, out point)) 
                {
                    Debug.DrawRay(point, Vector3.up, Color.blue, 1.0f); 
                    agent.SetDestination(point);
                }
            }
        }


    }
    bool RandomPoint(Vector3 center, float range, out Vector3 result)
    {

        Vector3 randomPoint = center + Random.insideUnitSphere * range; 
        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas)) 
        {
            result = hit.position;
            return true;
        }

        result = Vector3.zero;
        return false;
    }
}
