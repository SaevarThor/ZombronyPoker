using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private string firstSceneName;
    [SerializeField] private GameObject lordManager;

    private void Start()
    {
        if (GameManager.Instance == null)
            Instantiate(lordManager);
    }

    public void StartGame()
    {
        SceneLoadingManager.LoadNewScene(firstSceneName);
    }
}
