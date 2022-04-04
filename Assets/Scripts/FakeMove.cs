using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FakeMove : MonoBehaviour
{
    public Transform pos;
    public Animator anim;
    
    void Start()
    {
        GetComponent<NavMeshAgent>().SetDestination(pos.position);
        anim.SetFloat("Speed", 1);
    }
    
}
