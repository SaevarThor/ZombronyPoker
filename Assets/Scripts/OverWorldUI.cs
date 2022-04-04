using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OverWorldUI : MonoBehaviour
{
    public static OverWorldUI Instance;
    
    public Slider ZombSlider;
    private bool _move;
    private float newValue;

    [SerializeField] private TMP_Text syringeAmount;
    [SerializeField] private Button syringeButton;
    private int syringes;

    public GameObject Half;
    public GameObject ALmost;

    public AudioSource heal;

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
        syringes = InventoryManager.Instance.GetSyringes();
        syringeAmount.text = syringes.ToString();

        syringeButton.interactable = syringes != 0;
        syringeButton.onClick.AddListener(UseSyringe);
    }
    

    public void MoveSlider(int newAmount)
    {
        _move = true;
        newValue = ZombSlider.value + newAmount;
    }

    private void UseSyringe()
    {
        if (syringes > 0)
        {
            InventoryManager.Instance.UseSyringe();
            syringes--;
            syringeAmount.text = syringes.ToString();
            newValue -= InventoryManager.Instance.SyringeHeal;
            ZombSlider.value = GameManager.Instance.ZombificationTimer;
            heal.Play();
        }

        syringeButton.interactable = syringes != 0;
    }

    private void Update()
    {
        if (!_move) return;
        
        if (ZombSlider.value < newValue)
            ZombSlider.value += (Time.deltaTime * 3);

        if (ZombSlider.value > 10)
        {
            Half.SetActive(true);
        } else 
            Half.SetActive(false);

        if (ZombSlider.value > 16)
        {
            ALmost.SetActive(true);
        } else 
            ALmost.SetActive(false);
    }
}
