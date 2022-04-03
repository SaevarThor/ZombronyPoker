using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class ItemPanel : MonoBehaviour
{
    public GameObject UiParent;
    public TMP_Text Text;
    public TMP_Text ButtonText;
    public Image Image;
    public Button LeaveButton;

    public Sprite ItemImage;
    public Sprite CardImage;
    public Sprite NothingImage;
    public Sprite FightImage;

    public string[] ItemFlavorText;
    public string[] CardFlavorText;
    public string[] NothingFlavorText;
    public string[] FightFlavorText;

    public string[] ItemButtonText;
    public string[] CardButtonText;
    public string[] NothingButtonText;
    public string[] FightButtonText;

    public GameObject Enemy; 

    private void Start()
    {
        EncounterManager.Instance.ItemVisual = this;
        LeaveButton.onClick.AddListener(ExitMenu);
    }

    private void ExitMenu()
    {
        UiParent.SetActive(false);
        Time.timeScale = 1;
    }

    public void SetItemPanel()
    {
        UiParent.SetActive(true);
        Text.text = ItemFlavorText[Random.Range(0, ItemFlavorText.Length)];
        ButtonText.text = ItemButtonText[Random.Range(0, ItemButtonText.Length)];
        Image.sprite = ItemImage;
    }
    
    public void SetCardPanel(string charName)
    {
        UiParent.SetActive(true);
        Text.text = CardFlavorText[Random.Range(0, CardFlavorText.Length)] + $"\n {charName} has entered the fight!";
        ButtonText.text = CardButtonText[Random.Range(0, CardButtonText.Length)];
        Image.sprite = CardImage;
    }
    
    public void SetFightPanel()
    {
        UiParent.SetActive(true);
        Text.text = FightFlavorText[Random.Range(0, FightFlavorText.Length)];
        ButtonText.text = FightButtonText[Random.Range(0, FightButtonText.Length)];
        Image.sprite = FightImage;
        GameObject g = Instantiate(GameManager.Instance.ZombiePrefab, Player.Instance.GetRandomPlayerPos(), Quaternion.identity);

        g.GetComponent<Zombie>().AttackPlayer();
    }

    public void SetNothingPanel()
    {
        UiParent.SetActive(true);
        Text.text = NothingFlavorText[Random.Range(0, NothingFlavorText.Length)];
        ButtonText.text = NothingButtonText[Random.Range(0, NothingButtonText.Length)];
        Image.sprite = NothingImage;

    }
}
