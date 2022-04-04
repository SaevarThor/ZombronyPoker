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
    private EncounterManager manager;
    private zombieState state = zombieState.Patroling;
    private NavMeshAgent agent;
    private Vector3 curTarget;
    private bool canMove = true;
    private Transform playerTrans;

    public Animator anim;

    private void Awake()
    {
        agent = this.GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        manager = EncounterManager.Instance;
        patrolId = 0;

        SetNextPatrolPoint();
    }

    private void SetNextPatrolPoint()
    {
        if (patrolPoints.Length == 0 || patrolPoints[0] == null) return;
        
        if ((patrolId + 1) == patrolPoints.Length) patrolId = 0;
        else patrolId++;

        curTarget = patrolPoints[patrolId].position;
        agent.SetDestination(curTarget);
    }

    public void AttackPlayer()
    {
        playerTrans = Player.Instance.transform;
        state = zombieState.Attacking;
    }

    private void NextAction()
    {
    }

    private void Update()
    {
        anim.SetFloat("Speed", agent.velocity.magnitude);
        if (!canMove) return; 
        if (state == zombieState.Patroling)
        {
            if (Vector3.Distance(transform.position, curTarget) < 1)
            {
                SetNextPatrolPoint();
            }
        }
        
        if (state == zombieState.Attacking && !Player.Instance.IsFighting)
        {
            agent.SetDestination(playerTrans.position);
            if (Vector3.Distance(transform.position, playerTrans.position) < 4)
            {
                Debug.Log($"Zombie In Range manager= {manager.name}");
                Vector3 zombiePos = transform.position;
                agent.SetDestination(zombiePos);
                manager.RequestCombat(playerTrans.position, zombiePos, this);
                SetMove(false);
            }
        }
    }

    private void SetMove(bool value)
    {
        canMove = value;
    }

    public void Die()
    {
        //Play anim and die
        
        //Destroy(this.gameObject, 2f);
        anim.SetBool("Die", true);
        Destroy(this);
    }

    public void Celebrate()
    {
        anim.SetBool("Dance", true);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            state = zombieState.Attacking;
            playerTrans = other.transform;
        }
    }
}
