using System;
using UnityEngine;
using UnityEngine.AI;

public class Player : MonoBehaviour
{
    public static Player Instance;
    
    [SerializeField] private float speed;
    [SerializeField] private float rotationSpeed;

    private NavMeshAgent agent;
    private bool canMove = true;
    private bool isSearching;
    private SearchContainer searchContainer;

    private void Awake()
    {
        if (Instance != this && Instance != null)
            Destroy(this);
        else
            Instance = this;
        
        agent = this.GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        agent.speed = speed;
        agent.angularSpeed = rotationSpeed;
    }

    public void MovePlayer(Vector3 pos, SearchContainer container = null)
    {
        if (!canMove) return;
        agent.SetDestination(pos);
        searchContainer = container;
        isSearching = container;
    }


    public void SetMovement(bool value)
    {
        canMove = value;
        if (!value)
        {
            agent.SetDestination(this.transform.position);
        }
    }

    private void Update()
    {
        if (!isSearching || !canMove) return;

        if (Vector3.Distance(transform.position, agent.destination) < 1)
        {
            GameManager.Instance.Search(searchContainer);
            SetMovement(false);
        }
    }
}
