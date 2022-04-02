using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class EventManager : MonoBehaviour
{
    public static EventManager Instance;
    
    [SerializeField] private GameObject uiPanel;
    [SerializeField] private Image image;
    [SerializeField] private TMP_Text text;
    [SerializeField] private Button acceptButton;
    [SerializeField] private Button declineButton;

    private void Awake()
    {
        if (Instance != this && Instance != null)
            Destroy(this);
        else
            Instance = this; 
    }

    public void RequestEvent(Sprite newImage, string newText, IRequest request)
    {
        Player.Instance.SetMovement(false);
        image.sprite = newImage;
        text.text = newText;
        acceptButton.onClick.AddListener(request.AcceptRequest);
        acceptButton.onClick.AddListener(Decline);
        declineButton.onClick.AddListener(Decline);
        uiPanel.SetActive(true); 
    }
    
    private void Decline()
    {
        Player.Instance.SetMovement(true);
        uiPanel.SetActive(false);
    }
}
