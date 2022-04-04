using UnityEngine;
using TMPro;
using Unity.Mathematics;
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
    [SerializeField] private Image selectionGraphic;

    private int oldHealth;

    public GameObject Front;

    public GameObject BloodParticle;

    private void Start() {
        DeSelect();
    
        cardTitle.text = thisCard.CardName;
        cardDesc.text = thisCard.Description;
        cardHealth.text = thisCard.CurrentHealth.ToString();
        cardAttack.text = thisCard.Damage.ToString();
        profilePic.sprite = thisCard.CardPic;
        //cardCost.text = thisCard.Cost.ToString();

        oldHealth = thisCard.CurrentHealth;
        
        if (thisCard.faction == CardFaction.Enemy)
            this.gameObject.tag = "OpponentCard";

        thisCard.onCardTakeDamage += UpdateCardInfo;
    }

    public void Select(){
        //this.transform.GetComponent<MeshRenderer>().material = MaterialSelected;
        selectionGraphic.enabled = true;
        Selected = true;
    }

    public void DeSelect(){
        //this.transform.GetComponent<MeshRenderer>().material = MaterialNotSelected;
        selectionGraphic.enabled = false;
        Selected = false;
    }

    public void HasAttacked()
    {
        Debug.Log($"{thisCard.CardName} has attacked");
        Front.SetActive(false);
        thisCard.HasAttackedThisRound = true;
    }

    public void ResetAttack()
    {
        Front.SetActive(true);
        thisCard.HasAttackedThisRound = false;
    }

    public void DestoryCard(){

        thisCard.OnCardDestroyed -= DestoryCard;
        Destroy(this.gameObject);
    }

    public void UpdateCardInfo(){
        Debug.Log("update card info");
        if (oldHealth != thisCard.CurrentHealth)
        {
            if (BloodParticle != null && this != null)
            {
                GameObject g = Instantiate(BloodParticle, this.transform.position, quaternion.identity);
                Destroy(g, 1f);
            }
        }

        cardTitle.text = thisCard.CardName;
        cardDesc.text = thisCard.Description;
        cardHealth.text = thisCard.CurrentHealth.ToString();
        cardAttack.text = thisCard.Damage.ToString();
    }
}