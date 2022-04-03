using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GUI_Orientation : int{
    faceUp = 0,
    faceDown = 1
}

public class GUI_CardBattle : MonoBehaviour
{
    public LayerMask LayerMask;
    private Card thisCard;
    private GUI_Orientation orientation = GUI_Orientation.faceDown;
    
    public GUI_HandInteractions handInteraction;
    public GUI_CardInteraction selectedCard;
    public GUI_CardInteraction selectedEnemyCard; 

    private void OnEnable() {
        BoardController.Instance.OnTurnEnd += turnEnd;
    }
        
    private void OnDisable() {
        BoardController.Instance.OnTurnEnd -= turnEnd;
    }
    void Update()
    {   
        if (BoardController.Instance.ActiveBattle && BoardController.Instance.PlayerTurn){
            if (Input.GetMouseButtonDown(0)){
                //raycast after cards? 
                Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, Mathf.Infinity, LayerMask)){
                    // selection of card
                    if (selectedCard != null && hit.transform.CompareTag("OpponentCard")){
                        //attack the enemy card
                        BoardController.Instance.Attack(selectedCard.thisCard, hit.transform.GetComponent<GUI_CardInteraction>().thisCard);
                    }
                    if (hit.transform.CompareTag("Card") && hit.transform != selectedCard){

                        Transform target = hit.transform;
                        // Deselect the previous card if there is one
                        deSelectCard();

                        selectedCard = target.GetComponent<GUI_CardInteraction>();
                        selectedCard.Select();
                    } 
                    // unselection of cards DOES NOT WORK ATM
                    else if (hit.transform.CompareTag("Card") && hit.transform == selectedCard){
                        // Deselect the previous card if there is one
                        deSelectCard();
                    } 
                    // Interact with player PlayArea
                    else if (hit.transform.CompareTag("PlayerPlayArea")){
                        if (selectedCard != null){
                            GUI_PlayArea playArea = hit.transform.GetComponent<GUI_PlayArea>();
                            // Place a card in the play area
                            if (playArea != null){
                                playArea.PlaceInPlayArea(selectedCard.transform);
                                handInteraction.RemoveFromHand(selectedCard.transform); 
                                deSelectCard();
                            }
                        }
                    } 
                    // Interaction with opponent playArea
                    else if (hit.transform.CompareTag("OpponentPlayArea")) {

                    }

                } else {
                    // If we click off a card, deselect it
                    deSelectCard();
                }
            }
        }
    }

    public void flipCard(){
       if (orientation == GUI_Orientation.faceDown) {
           orientation = GUI_Orientation.faceUp;
       } else {
           orientation = GUI_Orientation.faceDown;
       }
    }

    private void deSelectCard(){
        if (selectedCard != null){
            selectedCard.DeSelect();
            selectedCard = null;
        }
    }

    // stuff that needs to happen when a turn ends
    private void turnEnd(){
        deSelectCard();
    }
}
