using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouse : MonoBehaviour
{
    [SerializeField] private Camera playerCamera;
    
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            SendPosition();
        }
    }


    private void SendPosition()
    {
        RaycastHit _hit;
        Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out _hit))
        {
            Player.Instance.MovePlayer(_hit.point);
        }
    }
}
