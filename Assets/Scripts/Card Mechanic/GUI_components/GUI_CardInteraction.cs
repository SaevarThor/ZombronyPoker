using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GUI_CardInteraction : MonoBehaviour {

    public bool Selected = false;
    public Material MaterialSelected;
    public Material MaterialNotSelected;
    public Card thisCard;

    [SerializeField] private TMP_Text cardTitle;
    [SerializeField] private TMP_Text cardDesc;
    [SerializeField] private TMP_Text cardHealth;
    [SerializeField] private TMP_Text cardAttack;
    [SerializeField] private TMP_Text cardCost;
    [SerializeField] private Image profilePic;

    private void Start() {
        DeSelect();
    
        cardTitle.text = thisCard.CardName;
        cardDesc.text = thisCard.Description;
        cardHealth.text = thisCard.CurrentHealth.ToString();
        cardAttack.text = thisCard.Damage.ToString();
        profilePic.sprite = thisCard.CardPic;
        //cardCost.text = thisCard.Cost.ToString();
        
        if (thisCard.faction == CardFaction.Enemy)
            this.gameObject.tag = "OpponentCard";

        thisCard.onCardTakeDamage += UpdateCardInfo;
    }

    private void Update() {

    }

    public void Select(){
        this.transform.GetComponent<MeshRenderer>().material = MaterialSelected;
        Selected = true;
    }

    public void DeSelect(){
        this.transform.GetComponent<MeshRenderer>().material = MaterialNotSelected;
        Selected = false;
    }

    public void DestoryCard(){

        thisCard.OnCardDestroyed -= DestoryCard;
        Destroy(this.gameObject);
    }

    public void UpdateCardInfo(){
        Debug.Log("update card info");
        cardTitle.text = thisCard.CardName;
        cardDesc.text = thisCard.Description;
        cardHealth.text = thisCard.CurrentHealth.ToString();
        cardAttack.text = thisCard.Damage.ToString();
    }
}