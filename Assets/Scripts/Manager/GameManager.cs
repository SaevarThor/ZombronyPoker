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

    private void Update()
    {
        if (PlayerIsDead || OverWorldUI.Instance == null) return;
        if ((ZombificationMaxValue - .1f) <= OverWorldUI.Instance.GetValue())
        {
            Debug.Log("kill player");
            StartCoroutine(WaitAndLoad(3, "BossIntro"));
            PlayerIsDead = true; 
        }
    }

    public void SetZombification(int amount)
    {
        ZombificationTimer += amount;
    }

    private IEnumerator WaitAndLoad(float timer, string sceneName)
    {
        yield return new WaitForSeconds(timer);

        if (ZombificationTimer >= ZombificationMaxValue)
        {
            SceneLoadingManager.LoadNewScene(sceneName);
        }
        else
            PlayerIsDead = false;

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
        SceneLoadingManager.LoadNewScene("LoseScene");
    }
}
