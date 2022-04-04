using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouse : MonoBehaviour
{
    public Texture2D CursorHover;
    [SerializeField] private Camera playerCamera;
    
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (Player.Instance.canMove)
                SendPosition();
        }

        checkHover();
    }

    private void checkHover()
    {
        RaycastHit _hit;
        Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out _hit))
        {
            if (_hit.transform.CompareTag("Interactible"))
                Cursor.SetCursor(CursorHover, Vector2.zero, CursorMode.Auto);
            else 
                Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
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