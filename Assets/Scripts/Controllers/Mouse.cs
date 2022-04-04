using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Mouse : MonoBehaviour
{
    public Texture2D CursorHover;
    [SerializeField] private Camera playerCamera;

    public GameObject highlight;
    private GameObject curhigh;
    
    private float yOffset = 2;
    
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
            {
                Vector3 t = _hit.transform.position;
                t.y += yOffset;
                if (curhigh == null)
                    curhigh = Instantiate(highlight, t, highlight.transform.rotation);
            }
            else
            {
                if (curhigh != null)
                    Destroy(curhigh);
            }
        }
        else
        {
            if (curhigh != null)
                Destroy(curhigh);
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
                        SceneLoadingManager.LoadNewScene("WinScene Good");
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