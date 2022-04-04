using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardCamera : MonoBehaviour
{
    private bool attacking;
    [SerializeField] private Animator anim;
    
    public void SetAttacking(bool value, float delay = 0)
    {
        attacking = value;
        anim.SetBool("Attack", value);
    }
    
    


}
