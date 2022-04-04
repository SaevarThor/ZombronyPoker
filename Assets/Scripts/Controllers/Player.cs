using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Player : MonoBehaviour
{
    public static Player Instance;
    
    [SerializeField] private float speed;
    [SerializeField] private float rotationSpeed;

    private NavMeshAgent agent;
    public bool canMove = true;
    private bool isSearching;
    public bool isLeaving;
    public bool isWin;
    private SearchContainer searchContainer;

    public bool IsFighting;
    public Animator Anim; 

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
        Time.timeScale = 1;
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

    public void LeaveLevel(Vector3 pos, bool _isWin = false)
    {
        agent.SetDestination(pos);
        isLeaving = true;
        isWin = _isWin;
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
        Anim.SetFloat("Speed", agent.velocity.magnitude);
        if (isLeaving && canMove)
        {
            if (Vector3.Distance(transform.position, agent.destination) < 1)
            {
                if (!isWin)
                {
                    SceneLoadingManager.LoadNewScene("OverWorld");
                }
                else
                {
                    SceneLoadingManager.LoadNewScene("WinScene");
                }
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
