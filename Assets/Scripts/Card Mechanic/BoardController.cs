using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class BoardController : MonoBehaviour {
    public static BoardController Instance;
    
    private DeckController deckController => DeckController.Instance;

    private List<Card> opponentDeck = new List<Card>();
    private List<Card> playerDeck = new List<Card>();

    private int turn;
    private void Awake() {
            if (Instance != null && Instance != this){
                Destroy(this);
            } else {
                Instance = this;
            }
    }

    private void Start() {
        // copy inn the player deck
        playerDeck = new List<Card>(deckController.PlayerDeck.ToList());
        opponentDeck = new List<Card>(deckController.OpponentDeck.ToList());
    
    }

    // Actions
    public void DrawCard(Card cardToDraw){

    }

    public void Attack(){

    }

    public void PlayCard(){

    }
}