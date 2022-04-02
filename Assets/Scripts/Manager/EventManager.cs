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

    private UnityEvent[] unityEvents;

    private void Awake()
    {
        if (Instance != this && Instance != null)
            Destroy(this);
        else
            Instance = this; 
    }

    public void RequestEvent(Sprite newImage, string newText)
    {
      
    }
}
