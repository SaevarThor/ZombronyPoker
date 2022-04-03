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
        if (cardsLeft <= 0){
            //player has lost
            if (OnPlayerLoss != null)
                OnPlayerLoss();
            ActiveBattle = false;
        }
        if (CardsLeftOpponent <= 0){
            //player has won
            if (OnPlayerWin != null)
                OnPlayerWin();
            ActiveBattle = false;
        }
        if (playerHasAttacked){
            playerHasAttacked = false;
            endTurn(CardFaction.player);
        }
        if (!PlayerTurn && !OpponentTurn && !cleanupDone){
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
    // Draw a card into the hand, returns null if no cards are left.
    public Card DrawCard(CardFaction faction){
        if (faction == CardFaction.player){
            foreach (Card card in playerDeck){
                if (card.state == CardState.InDeck && cardsLeft > 0){
                    LastDrawnCard = card;
                    card.setCardState(CardState.OnHand);
                    return card;
                }
            }
            return null;
        } else {
            foreach (Card card in opponentDeck){
                if (card.state == CardState.InDeck){
                    LastDrawnCard = card;
                    card.setCardState(CardState.OnHand);
                    return card;
                }
            }
            return null;
        }
    }

    // maybe implement
    public void ShuffleDeck(){

    }

    public void Attack(Card attacker, Card target){
        if (attacker.Attack(target) != -1){
            if (attacker.faction == CardFaction.player){
                //send signal to gui
                endTurn(CardFaction.player);
            } else {
                //send signal to gui    
            }
        }
    }
    
    // gives a card the OnBoard status
    public int PlayCard(Card card){
        // cost check for the player
        if (card.faction != CardFaction.Enemy && resourcepool < card.Cost){
            // Send signal
            return -1;
        }

        // set the card state
        card.setCardState(CardState.OnBoard);
        LastPlayedCard = card;

        // Send signal
        return 0;
    }

    public void endTurn(CardFaction faction){
        if (faction == CardFaction.player){
            PlayerTurn = false;
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
}
