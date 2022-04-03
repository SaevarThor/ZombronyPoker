using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WinText : MonoBehaviour
{
    public TMP_Text WinTextText;
    
    void Start()
    {
        WinTextText.text = $"You saved {DeckController.Instance.PlayerDeck.Count} Survivors!!";
    }
}
