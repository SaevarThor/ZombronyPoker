using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class BoardController : MonoBehaviour {
    public static BoardController Instance;
    public int resourcepool;
    
    private DeckController deckController => DeckController.Instance;

    private List<Card> opponentDeck;
    private List<Card> playerDeck;

    private int turn;
    private int cardsLeft = 0;
    
    // required turn action flags
    private bool playerHasAttacked = false;
    private bool opponentHasAttacked = false;
    private bool cleanupDone = false;

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
            if (card.state == CardState.InDeck)
                cardsLeft ++;
        }

        resourcepool = 100;
    }

    private void Update() {
        if (playerHasAttacked && opponentHasAttacked && cleanupDone){
            turn ++;
            //send some signal to the gui
            playerHasAttacked = false;
            opponentHasAttacked = false;
            cleanupDone = false;
        }else if (playerHasAttacked && opponentHasAttacked && !cleanupDone) {
            // do cleanup, remove destroyed cards and such
            cleanupDone = true;
        } else if (playerHasAttacked && !opponentHasAttacked) {
            //pick a random card and attack for now, probably make it smarter in the future
            
        } else if (!playerHasAttacked) {
            //wait for the player to make an attack

        }
    }

    // Actions
    // Draw a card into the hand, returns null if no cards are left.
    public Card DrawCard(CardFaction faction){
        if (faction == CardFaction.player){
            foreach (Card card in playerDeck){
                if (card.state == CardState.InDeck){
                    return card;
                }
            }
            return null;
        } else {
            foreach (Card card in opponentDeck){
                if (card.state == CardState.InDeck){
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
            } else {
                //send signal to gui    
            }
        }
    }
    
    // gives a card the OnBoard status
    public int PlayCard(Card card){
        // cost check
        if (resourcepool < card.Cost){
            // Send signal
            return -1;
        }

        // set the card state
        card.setCardState(CardState.OnBoard);

        // Send signal
        return 0;
    }

}
