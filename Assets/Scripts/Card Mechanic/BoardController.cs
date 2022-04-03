using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class BoardController : MonoBehaviour {

    //Delegates
    public delegate void OnTurnEndDelegate();
    public OnTurnEndDelegate OnTurnEnd;

    public delegate void OnPlayerWinDelegate();
    public OnPlayerWinDelegate OnPlayerWin;

    public delegate void OnPlayerLossDelegate();
    public OnPlayerLossDelegate OnPlayerLoss;

    public static BoardController Instance;
    public int resourcepool;
    public bool ActiveBattle = false;
    public bool PlayerTurn = false;
    public bool OpponentTurn = false;
    public Card LastDrawnCard;
    public Card LastPlayedCard;

    private DeckController deckController => DeckController.Instance;

    private List<Card> opponentDeck;
    private List<Card> playerDeck;

    public int turn;
    public int cardsLeft = 0;
    public int CardsLeftOpponent = 0;
    
    // required turn action flags
    public bool playerHasAttacked = false;
    public bool opponentHasAttacked = false;
    public bool cleanupDone = false;

    // flags

    public bool hasDrawnCard = false;
    public bool hasPlayedCard = false;

    private void Awake() {
            if (Instance != null && Instance != this){
                Destroy(this);
            } else {
                Instance = this;
            }
    }

    private void Start() {
        playerDeck = deckController.PlayerDeck;
        opponentDeck = deckController.OpponentDeck;

        foreach (Card card in playerDeck){
            if (card.state == CardState.InDeck){
                cardsLeft ++;
            }
        }
        foreach (Card card in opponentDeck){
            if (card.state == CardState.InDeck){
                CardsLeftOpponent ++;
            }
        }

        resourcepool = 100;
    }

    private void Update() {

        if (ActiveBattle) {

            // Check for win condition
            if (cardsLeft <= 0){
                //player has lost
                if (OnPlayerLoss != null)
                    OnPlayerLoss();
                cleanUp();
                ActiveBattle = false;
            }
            if (CardsLeftOpponent <= 0){
                //player has won
                if (OnPlayerWin != null)
                    OnPlayerWin();
                cleanUp();
                ActiveBattle = false;
            }

            // Ends the turn after one attack
            /*if (playerHasAttacked){
                playerHasAttacked = false;
                endTurn(CardFaction.player);
            } */

            if (!PlayerTurn && !OpponentTurn && !cleanupDone){

                ResetForNextTurn();
                cleanupDone = true;
            } else if (!PlayerTurn && !OpponentTurn && cleanupDone){
                turn ++;
                // send some signal to the gui
                OnTurnEnd();

                playerHasAttacked = false;
                opponentHasAttacked = false;
                cleanupDone = false;
                PlayerTurn = true;
                OpponentTurn = false;
            }
        }
    }

    // Actions
    // Draw a card into the hand, returns null if no cards are left or if player has already drawn a card this round.
    public Card DrawCard(CardFaction faction){
        if (faction == CardFaction.player && !hasDrawnCard ){
            foreach (Card card in playerDeck){
                if (card.state == CardState.InDeck && cardsLeft > 0){
                    LastDrawnCard = card;
                    card.setCardState(CardState.OnHand);
                    hasDrawnCard = true;
                    return card;
                }
            }
            return null;
        } else if(faction == CardFaction.Enemy){
            foreach (Card card in opponentDeck){
                if (card.state == CardState.InDeck){
                    LastDrawnCard = card;
                    card.setCardState(CardState.OnHand);
                    return card;
                }
            }
            return null;
        } else {
            return null;
        }

    }

    // maybe implement
    public void ShuffleDeck(){

    }

    // The attack happens in the firs IF statement I keep forgeting 
    public void Attack(Card attacker, Card target){

        //we can only attack or play a card to the table
        if (!hasPlayedCard){
            if (attacker.Attack(target) != -1){
                // send signal attack ok
            } else {
                // send signal attack failed
            }
        }
    }
    
    // gives a card the OnBoard status
    public int PlayCard(Card card){
        // cost check for the player and check if we have played a card this round or if we have attacked this round
        if ((card.faction != CardFaction.Enemy && resourcepool < card.Cost) || hasPlayedCard || playerHasAttacked){
            // Send signal (Card play denied)
            return -1;
        }

        // set the card state
        card.setCardState(CardState.OnBoard);
        LastPlayedCard = card;
        hasPlayedCard = true;

        // Send signal (card play allowed)
        return 0;
    }

    public void endTurn(CardFaction faction){
        if (faction == CardFaction.player){
            PlayerTurn = false;
            hasPlayedCard = false;
            hasDrawnCard = false;
            OpponentTurn = true;
        } else{
            Debug.Log("opponent ended turn");
            OpponentTurn = false;
        }
    }

    public void endPlayerTurn(){
        endTurn(CardFaction.player);
    }

    public void DecrementCardsLeft(CardFaction faction){
        if (faction == CardFaction.player){
            cardsLeft--;
        }
        else{
            CardsLeftOpponent --;
        }
    }

    public void StartBattle(){
        ActiveBattle = true;
    }

    private void ResetForNextTurn(){
        //Make sure the cards on the board are ready for the next round
        foreach (Card card in playerDeck ){
            if (card.state == CardState.OnBoard){
                card.resetState();
            }
        }

        //Make sure the cards on the board are ready for the next round
        foreach (Card card in opponentDeck ){
            if (card.state == CardState.OnBoard){
                card.resetState();
            }
        }

        hasDrawnCard = false;
        hasPlayedCard = false;
        playerHasAttacked = false;

    }

    // Cleans up stuff at the end of a fight
    private void cleanUp(){

        List<Card> cardsToRemove = new List<Card>();
        //Make sure the cards on the board are ready for the next round
        foreach (Card card in playerDeck ){
            if (card.state == CardState.Destroyed){
                cardsToRemove.Add(card);
            }
            if (card.state == CardState.OnBoard){
                card.resetState();
            }
        }

        foreach (Card card in cardsToRemove){
            deckController.removeCardFromDeck(card);
        }

        //Make sure the cards on the board are ready for the next round
        foreach (Card card in opponentDeck ){
            if (card.state == CardState.OnBoard){
                card.resetState();
            }
        }

        hasDrawnCard = false;
        hasPlayedCard = false;
        playerHasAttacked = false;
    }
}
