using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Zombie : MonoBehaviour
{
    private enum zombieState
    {
        Patroling, 
        Attacking,
    }
    
    [SerializeField] private Transform[] patrolPoints;

    private int patrolId;
    private GameManager manager;
    private zombieState state = zombieState.Patroling;
    private NavMeshAgent agent;
    private Vector3 curTarget;

    private void Awake()
    {
        agent = this.GetComponent<NavMeshAgent>();
        manager = GameManager.Instance;

        patrolId = 0;

        SetNextPatrolPoint();
    }

    private void SetNextPatrolPoint()
    {
        if (patrolId == patrolPoints.Length) patrolId = 0;
        else patrolId++;

        curTarget = patrolPoints[patrolId].position;
        agent.SetDestination(curTarget);
    }

    private void Update()
    {
        if (state == zombieState.Patroling)
        {
            if (Vector3.Distance(transform.position, curTarget) < 1)
            {
                SetNextPatrolPoint();
            }
        }
    }
}
