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

    public void ShuffleDeck(){

    }

    public void Attack(Card attacker, Card target){
        if (attacker.Attack(target) != -1){
            // on success trigger some event to tell the gui
        }
    }

    public int PlayCard(Card card){
        // cost check
        if (resourcepool < card.Cost){
            return -1;
        }

        // set the card state
        card.setCardState(CardState.OnBoard);

        // trigger some kind of event so the board can update
        return 0;
    }

}
