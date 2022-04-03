using UnityEngine;
using TMPro;

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

    private void Start() {
        DeSelect();
    
        cardTitle.text = thisCard.CardName;
        cardDesc.text = thisCard.Description;
        cardHealth.text = thisCard.Health.ToString();
        cardAttack.text = thisCard.Damage.ToString();
        //cardCost.text = thisCard.Cost.ToString();
        
        if (thisCard.faction == CardFaction.Enemy)
            this.gameObject.tag = "OpponentCard";
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
}