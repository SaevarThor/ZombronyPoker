using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapPosition : MonoBehaviour
{
    public Vector3 Pos;
    public bool HasEncounter = true;
    public string EncounterSceneName;
    public int Distance = 7;
    
    private void Start()
    {
        Pos = transform.position;
    }
}
