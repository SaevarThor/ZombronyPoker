using System;
using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    
    [SerializeField] private GameObject table;
    private float combatInitTimer = 2;
    private float dropHeight = 40;

    private void Awake()
    {
        if (Instance != this && Instance != null)
            Destroy(this);
        else
            Instance = this;
    }

    public void RequestCombat(Vector3 playerPos, Vector3 zombiePos)
    {
        float x = (playerPos.x + zombiePos.x) / 2;
        float y = (playerPos.y + zombiePos.y) / 2;
        float z = (playerPos.z + zombiePos.z) / 2;
        Vector3 attackPosition = new Vector3(x, y, z);
        attackPosition.y += dropHeight;
        Instantiate(table, attackPosition, Quaternion.identity);
        
        Player.Instance.SetMovement(false);
    }

    private IEnumerator StartCombat()
    {
        yield return new WaitForSeconds(combatInitTimer);
        //Set camera target and zoom 
        
    }

    public void EndCombat()
    {
        //Kill off zombie and players. 
        Player.Instance.SetMovement(true);
    }
}
