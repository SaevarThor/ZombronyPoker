using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;
    
    public int Syringes;
    public int SyringeHeal = 4;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
            Instance = this; 
    }

    public void AddSyringe(int amount) => Syringes += amount;

    public int GetSyringes()
    {
        return Syringes; 
    }

    public void UseSyringe()
    {
        GameManager.Instance.SetZombification(-SyringeHeal);
        Syringes--; 
    }
}
