using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int MapPosition = 0;
    public int ZombificationTimer = 0;
    public int ZombificationMaxValue = 20;
    public bool PlayerIsDead = false;

    public GameObject ZombiePrefab;

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(this);
        else
            Instance = this;
    }

    void Start()
    {
        DontDestroyOnLoad(gameObject);       
    }

    public void SetZombification(int amount)
    {
        ZombificationTimer += amount;

        if (ZombificationMaxValue < ZombificationTimer)
        {
            PlayerIsDead = true; 
            StartCoroutine(WaitAndLoad(3, "BossFight"));
        }
    }

    private IEnumerator WaitAndLoad(float timer, string sceneName)
    {
        yield return new WaitForSeconds(timer); 
        
        SceneLoadingManager.LoadNewScene(sceneName);
    }

    public void LoseGame()
    {
        StartCoroutine(WaitAndLoose());
    }

    public void WinGame(string scene){
        StartCoroutine(WaitAndLoad(3f,scene));
    }

    private IEnumerator WaitAndLoose()
    {
        yield return new WaitForSeconds(3f);
        Destroy(this.gameObject);
        SceneLoadingManager.LoadNewScene("LoseScene");
    }
}
