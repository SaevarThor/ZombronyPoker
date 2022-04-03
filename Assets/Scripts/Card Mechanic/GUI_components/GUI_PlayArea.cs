using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUI_PlayArea : MonoBehaviour
{

    private List<Transform> cardInPlayArea = new List<Transform>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDisable() {
        CleanPlayArea();
        //clean up listeners
        foreach (Transform card in cardInPlayArea) {
            card.GetComponent<GUI_CardInteraction>().thisCard.OnCardDestroyed -= CleanPlayArea;
        }
    }

    public void PlaceInPlayArea(Transform card){

        if (card.GetComponent<GUI_CardInteraction>().thisCard.state == CardState.OnHand){

            Card cardData = card.GetComponent<GUI_CardInteraction>().thisCard;
            GUI_CardInteraction cardGUI = card.GetComponent<GUI_CardInteraction>();
            cardInPlayArea.Add(card);
            spaceCards();

            // This link should be removed when the play area disables
            cardData.OnCardDestroyed += CleanPlayArea;
            cardData.setCardState(CardState.OnBoard);

        }
        else {
            Debug.Log(string.Format("Placing failed for {0}", card.GetComponent<GUI_CardInteraction>().thisCard.CardName));
        }
    }

    public void CleanPlayArea(){
        List<Transform> remainingCards = new List<Transform>();
        foreach (Transform trans in cardInPlayArea){
            GUI_CardInteraction card = trans.GetComponent<GUI_CardInteraction>();
            if (card.thisCard.state == CardState.Destroyed){
                card.DestoryCard();

            } else {
                remainingCards.Add(trans);
            }
        }
        cardInPlayArea = remainingCards;
    }

    private void spaceCards(){
        float spacing = 1.25f;
        float offset = 0;
        foreach (Transform trans in cardInPlayArea){
            trans.position = this.transform.position;
            trans.rotation = this.transform.rotation;
            Vector3 newPos = new Vector3(trans.position.x -3.5f + offset, this.transform.position.y, this.transform.position.z);
            trans.position = newPos;
            offset += spacing;
        }
    }
}
