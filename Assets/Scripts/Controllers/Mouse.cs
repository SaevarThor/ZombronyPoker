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
            if (_hit.transform.CompareTag("Interactible"))
            {
                Player.Instance.isLeaving = false;
                var interactable = _hit.transform.GetComponent<IInteractible>();
                if (interactable is SearchContainer)
                {
                    Player.Instance.MovePlayer(interactable.InteractPos(), interactable as SearchContainer);
                }
                else if (interactable is LeaveArea)
                {
                    LeaveArea area = (LeaveArea) interactable;

                    if (area.IsEnding)
                    {
                        SceneLoadingManager.LoadNewScene("WinScene");
                        return;
                    }
                    
                    Player.Instance.LeaveLevel(interactable.InteractPos());
                }
                else
                {
                    Player.Instance.MovePlayer(interactable.InteractPos());
                }
            }
            else
            {
                Player.Instance.MovePlayer(_hit.point);
            }
        }
    }
}