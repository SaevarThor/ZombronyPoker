using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OverWorldUI : MonoBehaviour
{
    public static OverWorldUI Instance;
    
    public Slider ZombSlider;
    private bool _move;
    private float newValue;


    private void Awake()
    {
        if (Instance != this && Instance != null)
            Destroy(this);
        else
            Instance = this;
    }

    private void Start()
    {
        ZombSlider.maxValue = GameManager.Instance.ZombificationMaxValue;
        ZombSlider.value = GameManager.Instance.ZombificationTimer;
    }

    public void MoveSlider(int newAmount)
    {
        _move = true;
        newValue = ZombSlider.value + newAmount;
    }

    private void Update()
    {
        if (!_move) return;
        
        if (ZombSlider.value < newValue)
            ZombSlider.value += (Time.deltaTime * 3);
    }
}
