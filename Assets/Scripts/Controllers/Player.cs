using System.Collections.Generic;
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
    private bool isLeaving; 
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

    public void LeaveLevel(Vector3 pos)
    {
        agent.SetDestination(pos);
        isLeaving = true; 
    }


    public void SetMovement(bool value)
    {
        canMove = value;
        if (!value)
        {
            agent.SetDestination(this.transform.position);
        }
    }

    public Vector3 GetRandomPlayerPos()
    {
        List<Vector3> positions = new List<Vector3>();
        for (int i = 0; i < 6; i++)
        {
            float x = Random.Range(3, 8);
            float z = Random.Range(3, 8); 
            positions.Add(new Vector3(x, this.transform.position.y, z));
        }

        Vector3 pos = positions[0];
        foreach (var position in positions)
        {
            NavMeshHit hit;
            if (NavMesh.SamplePosition(position, out hit, 1.0f, NavMesh.AllAreas)) {
                return hit.position;
            }
        }

        return pos;
    }

    private void Update()
    {
        if (isLeaving)
        {
            if (Vector3.Distance(transform.position, agent.destination) < 1)
            {
                SceneLoadingManager.LoadNewScene("OverWorld");
            }   
        }
        if (!isSearching || !canMove) return;
        if (Vector3.Distance(transform.position, agent.destination) < 1)
        {
            EncounterManager.Instance.Search(searchContainer);
            SetMovement(false);
            isSearching = false;
        }
    }
}
