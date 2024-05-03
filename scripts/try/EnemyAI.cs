using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    private UnityEngine.AI.NavMeshAgent navMeshAgent;

    private void Start()
    {
        navMeshAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
    }

    public void SetDestination(Transform destination)
    {
        if (navMeshAgent != null)
        {
            navMeshAgent.SetDestination(destination.position);
        }
    }
}
