using System.Collections;
using UnityEngine;

public class EncounterManager : MonoBehaviour
{
    public static EncounterManager Instance;
    
    [SerializeField] private GameObject table;
    [SerializeField] private Camera camera;
    private float combatInitTimer = 3;
    private float dropHeight = 40;

    private Zombie fightingZombie;
    private Table fightingTable;
    private Vector3 attackPosition;
    private bool zoomCamera;

    private void Awake()
    {
        if (Instance != this && Instance != null)
            Destroy(this);
        else
            Instance = this;
    }

    public void RequestCombat(Vector3 playerPos, Vector3 zombiePos, Zombie zombie)
    {
        float x = (playerPos.x + zombiePos.x) / 2;
        float y = (playerPos.y + zombiePos.y) / 2;
        float z = (playerPos.z + zombiePos.z) / 2;
        attackPosition = new Vector3(x, y, z);
        Vector3 tableSpawn = attackPosition;
        tableSpawn.y += dropHeight;
        GameObject g = Instantiate(table, tableSpawn, Quaternion.identity);
        fightingTable = g.GetComponent<Table>(); 
        Player.Instance.SetMovement(false);

        StartCoroutine(StartCombat());
    }

    private IEnumerator StartCombat()
    {
        yield return new WaitForSeconds(combatInitTimer);
        //Set camera target and zoom 
        //camera.transform.position = Vector3.Lerp(camera.StartPos, fightingTable.CameraPosition.position, 1); 
        zoomCamera = true;
        yield return new WaitForSeconds(5);
        zoomCamera = false;
        //INIT COMBAT
    }

    private void Update()
    {
        if (zoomCamera)
        {
            Transform cameraTrans = camera.transform;
            cameraTrans.rotation = Quaternion.Slerp(cameraTrans.rotation, Quaternion.LookRotation(attackPosition - cameraTrans.position), 1*Time.deltaTime);
            camera.orthographicSize -= (Time.deltaTime * 3);
        }
    }

    public void EndCombat(bool win)
    {
        if (win)
        {
            //Kill off zombie and players. 
            Player.Instance.SetMovement(true);
            fightingZombie.Die();
        }
        else
        {
            fightingZombie.Celebrate();
            //Kill Players
            //End game
        }
    }

    public void Search(SearchContainer container)
    {
        Debug.Log("Searching...");
        EventManager.Instance.RequestEvent(container.UiPicture, container.ContainerText, container);
    }
}
