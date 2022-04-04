using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class VfxManager : MonoBehaviour
{
    public static VfxManager Instance;
    
    public GameObject SkullPefab;
    public GameObject FirePrefab;


    private void Awake()
    {
        if (Instance != this && Instance != null)
            Destroy(Instance);
        else
            Instance = this;
    }

    public void SpawnSkull(Vector3 pos, Quaternion rot)
    {
        Instantiate(SkullPefab, pos, rot);
        GameObject g = Instantiate(FirePrefab, pos, rot);
        
        Destroy(g, 1);
    }
}
