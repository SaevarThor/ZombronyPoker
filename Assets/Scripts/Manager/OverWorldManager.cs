using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OverWorldManager : MonoBehaviour
{
    [SerializeField] private Transform playerEmblem;
    [SerializeField] private MapPosition[] positions;

    private int curIndex;
    private bool move;

    private Vector3 startPos;
    
    private void Start()
    {
        curIndex = GameManager.Instance.MapPosition;
        InitializeEmblem(curIndex);
    }

    private void InitializeEmblem(int mapPointId)
    {
        playerEmblem.position = positions[mapPointId].transform.position;
    }

    public void TravelToNext()
    {
        if (move) return;
        
        curIndex++;
        startPos = playerEmblem.position;
        GameManager.Instance.SetZombification(positions[curIndex].Distance);
        OverWorldUI.Instance.MoveSlider(positions[curIndex].Distance);
        move = true; 
    }

    private void Update()
    {
        if (!move || GameManager.Instance.PlayerIsDead) return;
        
        playerEmblem.position = Vector3.MoveTowards(playerEmblem.position, positions[curIndex].Pos, 0.001f);

        if (Vector3.Distance(playerEmblem.position, positions[curIndex].Pos) < .05f)
        {
            move = false;
            if (!positions[curIndex].HasEncounter) TravelToNext();
    
            if (String.IsNullOrEmpty(positions[curIndex].EncounterSceneName)) return;
            
            GameManager.Instance.MapPosition = curIndex;
            SceneLoadingManager.LoadNewScene(positions[curIndex].EncounterSceneName);
        }
    }
}
