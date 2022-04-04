using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFightManager : MonoBehaviour {
    
    private void Start() {
        if (BoardController.Instance != null){
            BoardController.Instance.OnPlayerWin += PlayerWin;
            BoardController.Instance.OnPlayerLoss += PlayerLoss;
        }
    }

    private void OnDisable() {
        if (BoardController.Instance != null){
            BoardController.Instance.OnPlayerWin -= PlayerWin;
            BoardController.Instance.OnPlayerLoss -= PlayerLoss;
        }
    }
    
    private void PlayerWin(){
        //show win screen
        Debug.Log("Player won! show win screen");
        GameManager.Instance.WinGame("LoseScene Bad");
    }

    private void PlayerLoss(){
        //show loss screen
        Debug.Log("Player lost! show loss screen");
        GameManager.Instance.LoseGame();
        
    }


    private IEnumerator WaitAndLoad(float timer, string sceneName)
    {
        yield return new WaitForSeconds(timer); 
        
        SceneLoadingManager.LoadNewScene(sceneName);
    }
}