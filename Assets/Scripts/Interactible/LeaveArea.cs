using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaveArea : MonoBehaviour, IInteractible
{
    [SerializeField] private Transform iPos;
    public bool IsEnding;
    
    public void Interact()
    {
    }

    public Vector3 InteractPos()
    {
        return iPos.position;
    }
}
