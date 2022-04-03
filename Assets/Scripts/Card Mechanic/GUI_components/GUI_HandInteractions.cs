using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class GUI_HandInteractions : MonoBehaviour {

    public GameObject card_prefab;

    private List<Transform> cardOnHand = new List<Transform>();
    private void Start()
    {
        StartCoroutine(WaitAndDraw());
    }

    private IEnumerator WaitAndDraw()
    {
        yield return new WaitForSeconds(.2f);

        Card[] cards = DeckController.Instance.PlayerDeck.ToArray();
        Debug.Log(cards.Length);
        foreach (var card in cards)
        {
            card.setCardState(CardState.OnHand);
            cardOnHand.Add(InstansiateNewCard(card).transform);
        }
        
        spaceCards();
    }

    public GameObject InstansiateNewCard(Card card){
        //instansiate gameobject
        GameObject newCard = null;
        if (card_prefab != null){
            newCard = Instantiate(card_prefab, transform.position, transform.rotation);
            newCard.transform.parent = this.transform;
            newCard.GetComponent<GUI_CardInteraction>().thisCard = card;
        }

        //attach card class

        //generate visuals for card

        //show gameobject
        return newCard;
    }

    public void RemoveFromHand(Transform card){
        if (cardOnHand.Contains(card))
            cardOnHand.Remove(card);
    }
    
    private void spaceCards(){
        float spacing = 0.125f;
        float offset = 0;
        foreach (Transform trans in cardOnHand){
            trans.position = this.transform.position;
            Vector3 newPos = new Vector3(trans.position.x + offset, this.transform.position.y, this.transform.position.z);
            trans.position = newPos;
            offset += spacing;
        }
    }
}