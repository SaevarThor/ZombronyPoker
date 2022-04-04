using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
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

    [SerializeField] private Camera fightCamera;
    private CardCamera cardCamera;

    private List<GUI_CardInteraction> placedCards = new List<GUI_CardInteraction>();

    public bool InAttackState = false;

    private void Start() {
        BoardController.Instance.OnTurnEnd += turnEnd;
        cardCamera = fightCamera.GetComponent<CardCamera>();
    }
        
    private void OnDisable() {
        BoardController.Instance.OnTurnEnd -= turnEnd;
    }
    void Update()
    {   
        if (BoardController.Instance.ActiveBattle && BoardController.Instance.PlayerTurn){
            if (Input.GetMouseButtonDown(0)){
                //raycast after cards? 
                Ray ray = fightCamera.ScreenPointToRay (Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, Mathf.Infinity, LayerMask)){
                    Debug.Log(hit.transform.name);

                    // selection of card
                    if (selectedCard != null && hit.transform.CompareTag("OpponentCard")){
                        //attack the enemy card
                        InAttackState = true;
                        selectedCard.HasAttacked();
                        BoardController.Instance.Attack(selectedCard.thisCard, hit.transform.GetComponent<GUI_CardInteraction>().thisCard);

                        BoardController.Instance.TutText.text = BoardController.Instance.Tut_Attack;
                        
                        CardSoundController.Instance.CardHit.Play();
                        selectedCard.DeSelect();
                        selectedCard = null;
                        CheckAttackState();
                    }
                    if (hit.transform.CompareTag("Card") && hit.transform != selectedCard){
                        Transform target = hit.transform;
                        // Deselect the previous card if there is one
                        deSelectCard();
                        selectedCard = target.GetComponent<GUI_CardInteraction>();
                        if (InAttackState &&(selectedCard != null && selectedCard.thisCard.HasAttackedThisRound)) return;
                        selectedCard.Select();

                        if (hit.transform.GetComponent<GUI_CardInteraction>().thisCard.state == CardState.OnBoard)
                        {
                            cardCamera.SetAttacking(true);
                            if (BoardController.Instance.TutText != null)
                                BoardController.Instance.TutText.text = BoardController.Instance.Tut_Attack;
                        }
                        else if (!InAttackState)
                        {
                            if (BoardController.Instance.TutText != null)
                                BoardController.Instance.TutText.text = BoardController.Instance.Tut_PlaceCard;
                            cardCamera.SetAttacking(false);
                        }
                    } 
                    // unselection of cards DOES NOT WORK ATM
                    else if (hit.transform.CompareTag("Card") && hit.transform == selectedCard){
                        // Deselect the previous card if there is one
                        Debug.Log("Deselect");
                        deSelectCard();
                        selectedCard = null;
                        
                        if (!InAttackState)
                            cardCamera.SetAttacking(false, .5f);
                    } 
                    // Interact with player PlayArea
                    else if (hit.transform.CompareTag("PlayerPlayArea")&& selectedCard != null && selectedCard.thisCard.state == CardState.OnHand)
                    { 
                        hit.transform.GetComponent<IClickable>().Click(selectedCard);
                       handInteraction.RemoveFromHand(selectedCard.transform);
                       CardSoundController.Instance.PlaceCard.Play();
                       placedCards.Add(selectedCard);
                       deSelectCard();
                       selectedCard = null;

                    } 
                    // Interaction with opponent playArea
                    else if (hit.transform.CompareTag("OpponentPlayArea")) {

                    }

                } else {
                    // If we click off a card, deselect it
                    deSelectCard();
                    if (!InAttackState)
                        cardCamera.SetAttacking(false,.5f);
                }
            }
        }
    }

    private void CheckAttackState()
    {
        GUI_CardInteraction[] activeCards =
            placedCards.Where(x => x != null && x.thisCard.state == CardState.OnBoard).ToArray();

        var cardy = activeCards.FirstOrDefault(x => !x.thisCard.HasAttackedThisRound);

        bool hasTarget = BoardController.Instance.Enemy.myCardsOnBoard.FirstOrDefault(x => x != null && x.state != CardState.Destroyed) != default;
        
        Debug.Log($"hasTarget = {hasTarget}");

        if (cardy == default || !hasTarget)
        {
            InAttackState = false;
            BoardController.Instance.endPlayerTurn();
            cardCamera.SetAttacking(false, .5f);
            Debug.Log($"Resetting {placedCards.Count} cards");
            foreach (var card in placedCards)
            {
                if (card != null)
                    card.ResetAttack();
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
